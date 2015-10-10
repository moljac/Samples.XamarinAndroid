
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HolisticWare.Kiosk
{
	[Activity (Label = "KioskActivity")]		
	[
		IntentFilter 
			(
				new[]{Intent.ActionMain},
			    Categories = new string[] 
			    					{ 
			    						Intent.CategoryHome, 
			    						Intent.CategoryDefault 
			    					}
			 )
	]		
	public class KioskActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			System.Diagnostics.Debug.WriteLine("KioskActivity base.OnCreate(bundle)");
			base.OnCreate (bundle);
	
			// Create your application here
			System.Diagnostics.Debug.WriteLine("KioskActivity this.OnCreate(bundle) start");

			BlockedKeys = new List<Android.Views.Keycode>()
			{
				// KeyEvent.KEYCODE_VOLUME_DOWN
				Android.Views.Keycode.VolumeDown,
				// KeyEvent.KEYCODE_VOLUME_UP
				Android.Views.Keycode.VolumeUp,
				Android.Views.Keycode.Home,
				Android.Views.Keycode.Menu,
				Android.Views.Keycode.Power,

			};
			if (((int)Android.OS.Build.VERSION.SdkInt) >= 11) 
			{

				#if __ANDROID_11_
				BlockedKeys.Add(Android.Views.Keycode.VolumeMute);
				BlockedKeys.Add(Android.Views.Keycode.AppSwitch);
				#endif
			}	
		
			// add the following line in activity (before setContentView is called!). 
			// to deactivates the lock screen:
			Window.AddFlags
						(
							// WindowManager.LayoutParams.FLAG_DISMISS_KEYGUARD
							WindowManagerFlags.DismissKeyguard
						);
			Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);

			/*
			----------------------------------------------------------------------------------
			It is also very easy to keep the screen bright as long as the app is 
			visible (also forever). Add the following flag to your root layout:

			android:keepScreenOn="true"

			<RelativeLayout xmlns:android="http://schemas.android.com/apk/res/android"
			  android:id="@+id/myActivityRootLayout"
			  android:layout_width="match_parent" 
			  android:layout_height="match_parent"
			  android:keepScreenOn="true"
			  >
			  
			  <!-- your layout -->
			  
			</RelativeLayout>
			----------------------------------------------------------------------------------
			*/
			Window.AddFlags
				(
					Android.Views.WindowManagerFlags.KeepScreenOn
					|
	                Android.Views.WindowManagerFlags.DismissKeyguard
	                |
	                Android.Views.WindowManagerFlags.ShowWhenLocked
	                |
	                Android.Views.WindowManagerFlags.TurnScreenOn
				);      


			//
			HomeWatcher mHomeWatcher = new HomeWatcher(this);
			mHomeWatcher.SetOnHomePressedListener(new OnHomePressedListener());
			mHomeWatcher.StartWatch();

			System.Diagnostics.Debug.WriteLine("KioskActivity this.OnCreate(bundle) stop");

			return;
		}

		public override void OnBackPressed() 
		{
		    // NOP, do nothing, swallow Back press

			System.Diagnostics.Debug.WriteLine("KioskActivity OnBackPressed()");

			return;
		}

		//protected override void OnUserLeaveHint()
		//{
		//	System.Diagnostics.Debug.WriteLine("KioskActivity this.OnUserLeaveHint()");
		//	base.OnUserLeaveHint();

		//	return;
		//}
		/// <Docs>Whether the window of this activity has focus.</Docs>
		/// <summary>
		/// Raises the window focus changed event.
		///
		/// 	Disable Long Power Button Press
		///
		///		The idea is simple: in case any system dialog pops up, 
		///		kill it instantly by firing an ACTION_CLOSE_SYSTEM_DIALOG broadcast.
		///
		/// </summary>
		/// <param name="hasFocus">Has focus.</param>
		public override void OnWindowFocusChanged(bool hasFocus) 
		{
			System.Diagnostics.Debug.WriteLine("KioskActivity OnWindowsFocusChanged");

			base.OnWindowFocusChanged(hasFocus);
			if(!hasFocus) 
			{
				// Close every kind of system dialog
				Intent close_dialog = new Intent(Intent.ActionCloseSystemDialogs);
				this.SendBroadcast(close_dialog);
  			}

  			return;
		}

		/// <summary>
		/// Gets or sets the blocked keys.
		/// </summary>
		/// <value>The blocked keys.</value>
		/// <see cref="https://www.google.hr/search?sourceid=chrome-psyapi2&ion=1&espv=2&ie=UTF-8&q=xamarin%20android%20KeyEvent.KEYCODE_VOLUME_DOWN&oq=xamarin%20android%20KeyEvent.KEYCODE_VOLUME_DOWN&aqs=chrome..69i57.6015j0j7"/>
		public List<Android.Views.Keycode> BlockedKeys
		{
			get;
			set;
		}


		public override bool DispatchKeyEvent(KeyEvent ke) 
		{
			System.Diagnostics.Debug.WriteLine("KioskActivity DispatchkeyEvent - " + ke.KeyCode);
			if (BlockedKeys.Contains(ke.KeyCode)) 
			{
				return true;
			} 

			return base.DispatchKeyEvent(ke);
		}

	}

	public class OnHomePressedListener
	{
	    public virtual void OnHomePressed() 
	    {
	    	return;
	    }
		public virtual void OnHomeLongPressed() 
		{
			return;
	    }
	}

	// http://stackoverflow.com/questions/2208912/how-can-i-detect-user-pressing-home-key-in-my-activity
	// http://stackoverflow.com/questions/8881951/detect-home-button-press-in-android/8883447#8883447
	public class HomeWatcher 
	{
		static private string TAG = "hg";
		static private Context mContext;
		static private IntentFilter mFilter;
		static private OnHomePressedListener mListener;
		static private InnerRecevier mRecevier;

		public HomeWatcher(Context context) 
		{
			mContext = context;
			mFilter = new IntentFilter(Intent.ActionCloseSystemDialogs);
		}

		public void SetOnHomePressedListener(OnHomePressedListener listener) 
		{
			mListener = listener;
			mRecevier = new InnerRecevier();
		}

		public void StartWatch() 
		{
			if (mRecevier != null) 
			{
				mContext.RegisterReceiver(mRecevier, mFilter);
			}
		}

		public void StopWatch() 
		{
			if (mRecevier != null)
			{
				mContext.UnregisterReceiver(mRecevier);
			}
		}

	    class InnerRecevier : BroadcastReceiver 
		{
			string SYSTEM_DIALOG_REASON_KEY = "reason";
			string SYSTEM_DIALOG_REASON_GLOBAL_ACTIONS = "globalactions";
			string SYSTEM_DIALOG_REASON_RECENT_APPS = "recentapps";
			string SYSTEM_DIALOG_REASON_HOME_KEY = "homekey";


			public override void OnReceive (Context context, Intent intent)
			{
				String action = intent.Action;
				if (action.Equals (Intent.ActionCloseSystemDialogs))
				{
					String reason = intent.GetStringExtra (SYSTEM_DIALOG_REASON_KEY);
					if (reason != null)
					{
						Android.Util.Log.Error (TAG, "action:" + action + ",reason:" + reason);
						if (mListener != null)
						{
							if (reason.Equals (SYSTEM_DIALOG_REASON_HOME_KEY))
							{
								mListener.OnHomePressed ();
							}
							else if (reason.Equals (SYSTEM_DIALOG_REASON_RECENT_APPS))
							{
								mListener.OnHomeLongPressed ();
							}
						}
					}
				}
			}
        }




    }
}

