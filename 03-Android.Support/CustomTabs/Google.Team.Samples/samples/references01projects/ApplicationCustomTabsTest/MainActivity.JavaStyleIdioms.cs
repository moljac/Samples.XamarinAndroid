using System.Collections.Generic;
using System.Text;

using Android.App;
using Android.OS;
using Android.Widget;
using Android.Util;
using Android.Views;
using Android.Text;
using Android.Graphics;
using Android.Support.CustomTabs;

namespace ApplicationCustomTabsTest
{
    public partial class MainActivity
    {
		//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
		//ORIGINAL LINE: final Android.Widget.ArrayAdapter<Android.Util.Pair<String, String>> adapter = new Android.Widget.ArrayAdapter<Android.Util.Pair<String, String>>(this, 0, packagesSupportingCustomTabs)
		ArrayAdapter<KeyValuePair<string, string>> adapter = null;

		private void SetupSpinnerAdapterJavaStyle(Spinner spinner)
		{
			adapter = new ArrayAdapterAnonymousInnerClassHelper(this, packagesSupportingCustomTabs);
			spinner.Adapter = adapter;
			spinner.OnItemSelectedListener = new OnItemSelectedListenerAnonymousInnerClassHelper(this, adapter);
		}

		private class ArrayAdapterAnonymousInnerClassHelper : ArrayAdapter<KeyValuePair<string, string>>
		{
			private readonly MainActivity outerInstance;

			public ArrayAdapterAnonymousInnerClassHelper(MainActivity outerInstance, List<KeyValuePair<string, string>> packagesSupportingCustomTabs)
					: base(outerInstance, 0, packagesSupportingCustomTabs)
			{
				this.outerInstance = outerInstance;
			}

			public override View GetView(int position, View convertView, ViewGroup parent)
			{
                View view = convertView;
				if (view == null)
				{
					view = LayoutInflater.From(outerInstance).Inflate(Android.Resource.Layout.SimpleListItem2, parent, false);
				}
				KeyValuePair<string, string> data = GetItem(position);
				(view.FindViewById<TextView>(Android.Resource.Id.Text1)).Text = data.Key;//.First;
				(view.FindViewById<TextView>(Android.Resource.Id.Text2)).Text = data.Value;//.Second;
				return view;
			}

			public override View GetDropDownView(int position, View convertView, ViewGroup parent)
			{
				return GetView(position, convertView, parent);
			}
		}

		private class OnItemSelectedListenerAnonymousInnerClassHelper : Java.Lang.Object, AdapterView.IOnItemSelectedListener
		{
			private readonly MainActivity outerInstance;

			private ArrayAdapter<KeyValuePair<string, string>> adapter;

			public OnItemSelectedListenerAnonymousInnerClassHelper(MainActivity outerInstance, ArrayAdapter<KeyValuePair<string, string>> adapter)
			{
				this.outerInstance = outerInstance;
				this.adapter = adapter;
			}

			public void OnItemSelected(AdapterView parent, View view, int position, long id)
			{
				KeyValuePair<string, string> item = adapter.GetItem(position);
				if (TextUtils.IsEmpty(item.Value/*.Second*/))
				{
					OnNothingSelected(parent);
					return;
				}
				outerInstance.mPackageNameToBind = item.Value/*.second*/;
			}

			public void OnNothingSelected(AdapterView parent)
			{
				outerInstance.mPackageNameToBind = null;
			}
		}
		public void OnClick(View v)
		{
			string url = mEditText.Text.ToString();
			int viewId = v.Id;

			if (viewId == Resource.Id.connect_button)
			{
				BindCustomTabsService();
			}
			else if (viewId == Resource.Id.warmup_button)
			{
				bool success = false;
				if (custom_tabs_client != null)
				{
					success = custom_tabs_client.Warmup(0);
				}
				if (!success)
				{
					mWarmupButton.Enabled = false;
				}
			}
			else if (viewId == Resource.Id.may_launch_button)
			{
				CustomTabsSession session = Session;
				bool success = false;
				if (custom_tabs_client != null)
				{
					success = session.MayLaunchUrl(Android.Net.Uri.Parse(url), null, null);
				}
				if (!success)
				{
					mMayLaunchButton.Enabled = false;
				}
			}
			else if (viewId == Resource.Id.launch_button)
			{
				CustomTabsIntent.Builder builder = new CustomTabsIntent.Builder(Session);
				builder.SetToolbarColor(Color.ParseColor(TOOLBAR_COLOR)).SetShowTitle(true);
				PrepareMenuItems(builder);
				PrepareActionButton(builder);
				PrepareBottomBar(builder);
				builder.SetStartAnimations(this, Resource.Animation.slide_in_right, Resource.Animation.slide_out_left);
				builder.SetExitAnimations(this, Resource.Animation.slide_in_left, Resource.Animation.slide_out_right);
				builder.SetCloseButtonIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.ic_arrow_back));


				CustomTabsIntent customTabsIntent = builder.Build();
				// derived class used - thus FQNS
				Android.Support.CustomTabs.Chromium.SharedUtilities.
						CustomTabsHelper.AddKeepAliveExtra(this, customTabsIntent.Intent);

				try
				{
					customTabsIntent.LaunchUrl(this, Android.Net.Uri.Parse(url));
				}
				catch (System.Exception exc)
				{
					StringBuilder sb = new StringBuilder();
					sb.AppendLine("CustomTabsIntent.LaunchUrl Exception");
				}
				return;
			}
		}







		/// <summary>
		/// Once per second, asks the framework for the process importance, and logs any change.
		/// </summary>
		private Java.Lang.IRunnable mLogImportance = new RunnableAnonymousInnerClassHelper();

        private class RunnableAnonymousInnerClassHelper : Java.Lang.Object, Java.Lang.IRunnable
        {
            public RunnableAnonymousInnerClassHelper()
            {
            }

            private int mPreviousImportance = -1;
            private bool mPreviousServiceInUse = false;
            private Handler mHandler = new Handler(Looper.MainLooper);

            public virtual void Run()
            {
                ActivityManager.RunningAppProcessInfo state = new ActivityManager.RunningAppProcessInfo();
                ActivityManager.GetMyMemoryState(state);
                int importance = (int)state.Importance;
                bool serviceInUse =
                            state.ImportanceReasonCode
                            ==
                            //ActivityManager.RunningAppProcessInfo.ReasonProviderInUse
                            Android.App.ImportanceReason.ProviderInUse
                            ;

                if (importance != mPreviousImportance || serviceInUse != mPreviousServiceInUse)
                {
                    mPreviousImportance = importance;
                    mPreviousServiceInUse = serviceInUse;
                    string message = "New importance = " + importance;
                    if (serviceInUse)
                    {
                        message += " (Reason: Service in use)";
                    }
                    Log.Warn(TAG, message);
                }
                mHandler.PostDelayed(this, 1000);
            }
        }

        private class NavigationCallback : CustomTabsCallback
        {
            public override void OnNavigationEvent(int navigationEvent, Bundle extras)
            {
                Log.Warn(TAG, "OnNavigationEvent: Code = " + navigationEvent);
            }
        }

    }
}