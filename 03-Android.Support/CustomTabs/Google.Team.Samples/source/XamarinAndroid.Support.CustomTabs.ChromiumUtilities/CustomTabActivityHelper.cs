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

using System.Collections.Generic;

using Android.App;
using Android.Net;
using Android.OS;
using Android.Support.CustomTabs;

namespace XamarinAnroid.Support.CustomTabs.ChromiumUtilities
{
	/// <summary>
	/// This is a helper class to manage the connection to the Custom Tabs Service.
	/// </summary>
	public class CustomTabActivityHelper : Java.Lang.Object, IServiceConnectionCallback
	{
		private CustomTabsSession mCustomTabsSession;
		private CustomTabsClient mClient;
		private CustomTabsServiceConnection mConnection;
		private IConnectionCallback mConnectionCallback;

		/// <summary>
		/// Opens the URL on a Custom Tab if possible. Otherwise fallsback to opening it on a WebView.
		/// </summary>
		/// <param name="activity"> The host activity. </param>
		/// <param name="customTabsIntent"> a CustomTabsIntent to be used if Custom Tabs is available. </param>
		/// <param name="uri"> the Uri to be opened. </param>
		/// <param name="fallback"> a CustomTabFallback to be used if Custom Tabs is not available. </param>
        public static void OpenCustomTab(Activity activity, CustomTabsIntent customTabsIntent, Uri uri, ICustomTabFallback fallback)
		{
			string packageName = CustomTabsHelper.GetPackageNameToUse(activity, uri.Path);

			//If we cant find a package name, it means theres no browser that supports
			//Chrome Custom Tabs installed. So, we fallback to the webview
			if (packageName == null)
			{
				if (fallback != null)
				{
					fallback.OpenUri(activity, uri);
				}
			}
			else
			{
				customTabsIntent.Intent.SetPackage(packageName);
				customTabsIntent.LaunchUrl(activity, uri);
			}

            return;
		}

		/// <summary>
		/// Unbinds the Activity from the Custom Tabs Service. </summary>
		/// <param name="activity"> the activity that is connected to the service. </param>
        public virtual void UnbindCustomTabsService(Activity activity)
		{
			if (mConnection == null)
			{
				return;
			}
			activity.UnbindService(mConnection);
			mClient = null;
			mCustomTabsSession = null;
			mConnection = null;
		}

		/// <summary>
		/// Creates or retrieves an exiting CustomTabsSession.
		/// </summary>
		/// <returns> a CustomTabsSession. </returns>
		public virtual CustomTabsSession Session
		{
			get
			{
				if (mClient == null)
				{
					mCustomTabsSession = null;
				}
				else if (mCustomTabsSession == null)
				{
                    /*
                    public CustomTabsSession NewSession(OnNavigationEventDelegate onNavigationEventHandler);
                    public virtual CustomTabsSession NewSession(CustomTabsCallback callback);
                    public CustomTabsSession NewSession
                                                (
                                                    OnNavigationEventDelegate onNavigationEventHandler, 
                                                    ExtraCallbackDelegate extraCallbackHandler
                                                );
                    */
                    mCustomTabsSession = mClient.NewSession    
                                                    (   
                                                        //null                                                        
                                                        null,
                                                        null
                                                    );
				}
				return mCustomTabsSession;
			}
		}

		/// <summary>
		/// Register a Callback to be called when connected or disconnected from the Custom Tabs Service. </summary>
		/// <param name="connectionCallback"> </param>
		public virtual void setConnectionCallback(IConnectionCallback connectionCallback)
		{
			this.mConnectionCallback = connectionCallback;
		}

		/// <summary>
		/// Binds the Activity to the Custom Tabs Service. </summary>
		/// <param name="activity"> the activity to be binded to the service. </param>
        public virtual void BindCustomTabsService(Activity activity)
		{
			if (mClient != null)
			{
				return;
			}

			string packageName = CustomTabsHelper.GetPackageNameToUse(activity, "http://xamarin.com");
			if (packageName == null)
			{
				return;
			}

			mConnection = new ServiceConnection(this);
			CustomTabsClient.BindCustomTabsService(activity, packageName, mConnection);
		}

		/// <seealso cref= <seealso cref="CustomTabsSession#mayLaunchUrl(Uri, Bundle, List)"/>. </seealso>
		/// <returns> true if call to mayLaunchUrl was accepted. </returns>
        public virtual bool MayLaunchUrl(Uri uri, Bundle extras, IList<Bundle> otherLikelyBundles)
		{
			if (mClient == null)
			{
				return false;
			}

			CustomTabsSession session = Session;
			if (session == null)
			{
				return false;
			}

			return session.MayLaunchUrl(uri, extras, otherLikelyBundles);
		}

		public virtual void OnServiceConnected(CustomTabsClient client)
		{
			mClient = client;
			mClient.Warmup(0L);
			if (mConnectionCallback != null)
			{
				mConnectionCallback.OnCustomTabsConnected();
			}
		}

		public virtual void OnServiceDisconnected()
		{
			mClient = null;
			mCustomTabsSession = null;
			if (mConnectionCallback != null)
			{
				mConnectionCallback.OnCustomTabsDisconnected();
			}
		}

	}

}