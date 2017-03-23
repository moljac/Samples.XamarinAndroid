using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.OS;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Net;
using Android.Text;
using Android.Graphics;

using Android.Support.CustomTabs;
using Android.Support.CustomTabs.Chromium.SharedUtilities;

// Copyright 2015 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace ApplicationCustomTabsTest
{
    /// <summary>
    /// Example client activity for using Chrome Custom Tabs.
    /// </summary>
    [
        Activity
            (
                Label = "ApplicationCustomTabsTest",
                MainLauncher = true,
                Icon = "@drawable/ic_launcher"
        )
    ]
    public partial class MainActivity : Activity, View.IOnClickListener, IServiceConnectionCallback
    {
        private const string TAG = "CustomTabsClientExample";
        private const string TOOLBAR_COLOR = "#ef6c00";

        private EditText mEditText;
        private Button mConnectButton;
        private Button mWarmupButton;
        private Button mMayLaunchButton;
        private Button mLaunchButton;
        private MediaPlayer mMediaPlayer;

        private CustomTabsSession custom_tabs_session;
        private CustomTabsClient custom_tabs_client;
        private CustomTabsServiceConnection custom_tabs_service_connection;
        private string mPackageNameToBind;




        List<KeyValuePair<string, string>> packagesSupportingCustomTabs = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.main);
            mEditText = FindViewById<EditText>(Resource.Id.edit);
            mConnectButton = FindViewById<Button>(Resource.Id.connect_button);
            mWarmupButton = FindViewById<Button>(Resource.Id.warmup_button);
            mMayLaunchButton = FindViewById<Button>(Resource.Id.may_launch_button);
            mLaunchButton = FindViewById<Button>(Resource.Id.launch_button);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
            mEditText.RequestFocus();
            mConnectButton.SetOnClickListener(this);
            mWarmupButton.SetOnClickListener(this);
            mMayLaunchButton.SetOnClickListener(this);
            mLaunchButton.SetOnClickListener(this);
            mMediaPlayer = MediaPlayer.Create(this, Resource.Raw.amazing_grace);

            packagesSupportingCustomTabs =
                    new List<KeyValuePair<string, string>>()
                    //PackageManagerHelper.GetPackageNameToUse.PackagesSupportingCustomTabs.ToList()
                    ;

            Intent activityIntent = new Intent
                                            (
                                                // 
                                                Intent.ActionView,
                                                Android.Net.Uri.Parse("https://holisticware.net")
                                            );

            PackageManager pm = PackageManager;
            IList<ResolveInfo> resolvedActivityList = null;
            resolvedActivityList = pm.QueryIntentActivities
                                            (
                                                activityIntent,
                                                Android.Content.PM.PackageInfoFlags.MatchAll
                                            );
            foreach (ResolveInfo info in resolvedActivityList)
            {
                Intent serviceIntent = new Intent();
                serviceIntent.SetAction("android.support.customtabs.action.CustomTabsService");
                serviceIntent.SetPackage(info.ActivityInfo.PackageName);
                ResolveInfo ri = pm.ResolveService(serviceIntent, 0);
                if (ri != null)
                {
                    string k = info.LoadLabel(pm).ToString();
                    string v = info.ActivityInfo.PackageName;
                    packagesSupportingCustomTabs.Add(new KeyValuePair<string, string>(k, v));
                }
            }

            SetupSpinnerAdapterJavaStyle(spinner);

            mLogImportance.Run();

            return;
        }




        protected override void OnDestroy()
        {
            UnbindCustomTabsService();
            base.OnDestroy();
        }

        private CustomTabsSession Session
        {
            get
            {
                if (custom_tabs_client == null)
                {
                    custom_tabs_session = null;
                }
                else if (custom_tabs_session == null)
                {
                    custom_tabs_session = custom_tabs_client.NewSession(new NavigationCallback());
                    SessionHelper.SetCurrentSession(custom_tabs_session);
                }
                return custom_tabs_session;
            }
        }

        private void BindCustomTabsService()
        {
            if (custom_tabs_client != null)
            {
                return;
            }
            if (TextUtils.IsEmpty(mPackageNameToBind))
            {
                mPackageNameToBind = PackageManagerHelper.GetPackageNameToUseDefaultUri(this);
                if (mPackageNameToBind == null)
                {
                    return;
                }
            }

            custom_tabs_service_connection = new ServiceConnection(this);
            bool ok = CustomTabsClient.BindCustomTabsService(this, mPackageNameToBind, custom_tabs_service_connection);
            if (ok)
            {
                mConnectButton.Enabled = false;
            }
            else
            {
                custom_tabs_service_connection = null;
            }
        }

        private void UnbindCustomTabsService()
        {
            if (custom_tabs_service_connection == null)
            {
                return;
            }
            UnbindService(custom_tabs_service_connection);
            custom_tabs_client = null;
            custom_tabs_session = null;
        }


        public void OnServiceConnected(CustomTabsClient client)
        {
            custom_tabs_client = client;
            mConnectButton.Enabled = false;
            mWarmupButton.Enabled = true;
            mMayLaunchButton.Enabled = true;
            mLaunchButton.Enabled = true;
        }

        public void OnServiceDisconnected()
        {
            mConnectButton.Enabled = true;
            mWarmupButton.Enabled = false;
            mMayLaunchButton.Enabled = false;
            mLaunchButton.Enabled = false;
            custom_tabs_client = null;
        }
    }

}