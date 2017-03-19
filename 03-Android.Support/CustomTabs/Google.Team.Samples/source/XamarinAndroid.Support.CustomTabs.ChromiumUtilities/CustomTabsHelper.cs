using System;
using System.Collections.Generic;

using Android.Content;
using Android.Net;
using Android.Text;
using Android.Content.PM;
using Android.Util;

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

namespace XamarinAnroid.Support.CustomTabs.ChromiumUtilities
{

	/// <summary>
	/// Helper class for Custom Tabs.
	/// </summary>
	public partial class CustomTabsHelper
	{
		private const string TAG = "CustomTabsHelper";

        /*
		internal const string STABLE_PACKAGE = "com.android.chrome";
		internal const string BETA_PACKAGE = "com.chrome.beta";
		internal const string DEV_PACKAGE = "com.chrome.dev";
		internal const string LOCAL_PACKAGE = "com.google.android.apps.chrome";
		*/
        public static Dictionary<string, string> PackagesSupportingCustomTabs 
        {
            get;
            set;
        } =
            new Dictionary<string, string>()
            {
                {"STABLE_PACKAGE", "com.android.chrome"},
				{"BETA_PACKAGE", "com.chrome.beta"},
				{"DEV_PACKAGE", "com.chrome.dev"},
				{"LOCAL_PACKAGE", "com.google.android.apps.chrome"},
			};

		private const string EXTRA_CUSTOM_TABS_KEEP_ALIVE = "android.support.customtabs.extra.KEEP_ALIVE";
		private const string ACTION_CUSTOM_TABS_CONNECTION = "android.support.customtabs.action.CustomTabsService";

		private static string sPackageNameToUse;

		private CustomTabsHelper()
		{
		}

		public static void AddKeepAliveExtra(Context context, Intent intent)
		{
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getCanonicalName method:
			Intent keepAliveIntent = (new Intent()).SetClassName(context.PackageName, typeof(KeepAliveService).FullName);
			intent.PutExtra(EXTRA_CUSTOM_TABS_KEEP_ALIVE, keepAliveIntent);
		}

        public static Func<Context, string, string> GetPackageNameToUse 
        {
            get;
            set;
        } = GetPackageNameToUseImplementation;

		/// <summary>
		/// Goes through all apps that handle VIEW intents and have a warmup service. Picks
		/// the one chosen by the user if there is one, otherwise makes a best effort to return a
		/// valid package name.
		/// 
		/// This is <strong>not</strong> threadsafe.
		/// </summary>
		/// <param name="context"> <seealso cref="Context"/> to use for accessing <seealso cref="PackageManager"/>. </param>
		/// <returns> The package name recommended to use for connecting to custom tabs related components. </returns>
		protected static string GetPackageNameToUseImplementation(Context context, string url = "http://www.example.com")
		{
			if (sPackageNameToUse != null)
			{
				return sPackageNameToUse;
			}

			PackageManager pm = context.PackageManager;
			// Get default VIEW intent handler.
			Intent activityIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));
			ResolveInfo defaultViewHandlerInfo = pm.ResolveActivity(activityIntent, 0);
			string defaultViewHandlerPackageName = null;
			if (defaultViewHandlerInfo != null)
			{
				defaultViewHandlerPackageName = defaultViewHandlerInfo.ActivityInfo.PackageName;
			}

			// Get all apps that can handle VIEW intents.
			IList<ResolveInfo> resolvedActivityList = pm.QueryIntentActivities(activityIntent, 0);
			IList<string> packagesSupportingCustomTabs = new List<string>();
			foreach (ResolveInfo info in resolvedActivityList)
			{
				Intent serviceIntent = new Intent();
                // Android.Support.CustomTabs.action.CustomTabsService
				serviceIntent.SetAction(ACTION_CUSTOM_TABS_CONNECTION);
				serviceIntent.SetPackage(info.ActivityInfo.PackageName);
				if (pm.ResolveService(serviceIntent, 0) != null)
				{
				packagesSupportingCustomTabs.Add(info.ActivityInfo.PackageName);
				}
			}

			// Now packagesSupportingCustomTabs contains all apps that can handle both VIEW intents
			// and service calls.
			if (packagesSupportingCustomTabs.Count == 0)
			{
				sPackageNameToUse = null;
			}
			else if (packagesSupportingCustomTabs.Count == 1)
			{
				sPackageNameToUse = packagesSupportingCustomTabs[0];
			}
			else if 
                (
                    !TextUtils.IsEmpty(defaultViewHandlerPackageName) 
                    && 
                    !HasSpecializedHandlerIntents(context, activityIntent) 
                    && 
                    packagesSupportingCustomTabs.Contains(defaultViewHandlerPackageName)
                )
			{
				sPackageNameToUse = defaultViewHandlerPackageName;
			}
			else if (packagesSupportingCustomTabs.Contains(PackagesSupportingCustomTabs["STABLE_PACKAGE"]))
			{
				sPackageNameToUse = PackagesSupportingCustomTabs["STABLE_PACKAGE"];
			}
			else if (packagesSupportingCustomTabs.Contains(PackagesSupportingCustomTabs["BETA_PACKAGE"]))
			{
				sPackageNameToUse = PackagesSupportingCustomTabs["BETA_PACKAGE"];
			}
			else if (packagesSupportingCustomTabs.Contains(PackagesSupportingCustomTabs["DEV_PACKAGE"]))
			{
				sPackageNameToUse = PackagesSupportingCustomTabs["DEV_PACKAGE"];
			}
			else if (packagesSupportingCustomTabs.Contains(PackagesSupportingCustomTabs["LOCAL_PACKAGE"]))
			{
				sPackageNameToUse = PackagesSupportingCustomTabs["LOCAL_PACKAGE"];
			}

			return sPackageNameToUse;
		}

		/// <summary>
		/// Used to check whether there is a specialized handler for a given intent. </summary>
		/// <param name="intent"> The intent to check with. </param>
		/// <returns> Whether there is a specialized handler for the given intent. </returns>
        private static bool HasSpecializedHandlerIntents(Context context, Intent intent)
		{
			try
			{
				PackageManager pm = context.PackageManager;
				IList<ResolveInfo> handlers = pm.QueryIntentActivities(intent, Android.Content.PM.PackageInfoFlags.ResolvedFilter);
				if (handlers == null || handlers.Count == 0)
				{
					return false;
				}
				foreach (ResolveInfo resolveInfo in handlers)
				{
					IntentFilter filter = resolveInfo.Filter;
					if (filter == null)
					{
						continue;
					}
					if (filter.CountDataAuthorities() == 0 || filter.CountDataPaths() == 0)
					{
						continue;
					}
					if (resolveInfo.ActivityInfo == null)
					{
						continue;
					}
					return true;
				}
			}
			catch (Exception e)
			{
				Log.Error(TAG, "Runtime exception while getting specialized handlers");
			}
			return false;
		}

		/// <returns> All possible chrome package names that provide custom tabs feature. </returns>
		public static string[] Packages
		{
			get
			{
				return new string[]
                {
                    "", 
                    PackagesSupportingCustomTabs["STABLE_PACKAGE"], 
                    PackagesSupportingCustomTabs["BETA_PACKAGE"],
                    PackagesSupportingCustomTabs["DEV_PACKAGE"],
                    PackagesSupportingCustomTabs["LOCAL_PACKAGE"],
                };
			}
		}
	}

}