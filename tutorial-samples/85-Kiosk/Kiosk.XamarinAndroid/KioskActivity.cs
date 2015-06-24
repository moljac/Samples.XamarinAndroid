
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

namespace Kiosk.Sample.XamarinAndroid
{
	[Activity (Label = "KioskActivity")]			
	public class KioskActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
	
			// Create your application here

			// add the following line in activity (before setContentView is called!). 
			// to deactivates the lock screen:
			Window.AddFlags
						(
							// WindowManager.LayoutParams.FLAG_DISMISS_KEYGUARD
							WindowManagerFlags.DismissKeyguard
						);

			return;
		}

		public override void OnBackPressed() 
		{
		    // NOP, do nothing, swallow Back press

			System.Diagnostics.Debug.WriteLine("KioskActivity OnBackPressed");

			return;
		}


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
			base.OnWindowFocusChanged(hasFocus);
			if(!hasFocus) 
			{
				// Close every kind of system dialog
				Intent close_dialog = new Intent(Intent.ActionCloseSystemDialogs);
				this.SendBroadcast(close_dialog);
  			}

  			return;
		}


		// https://www.google.hr/search?sourceid=chrome-psyapi2&ion=1&espv=2&ie=UTF-8&q=xamarin%20android%20KeyEvent.KEYCODE_VOLUME_DOWN&oq=xamarin%20android%20KeyEvent.KEYCODE_VOLUME_DOWN&aqs=chrome..69i57.6015j0j7
		List<Android.Views.Keycode> blockedKeys = new List<Android.Views.Keycode>()
		{
			// KeyEvent.KEYCODE_VOLUME_DOWN
			Android.Views.Keycode.VolumeDown,
			// KeyEvent.KEYCODE_VOLUME_UP
			Android.Views.Keycode.VolumeUp,
			Android.Views.Keycode.VolumeMute
		};

		public override bool DispatchKeyEvent(KeyEvent ke) 
		{
			if (blockedKeys.Contains(ke.KeyCode)) 
			{
				return true;
			} 
			else 
			{
				return base.DispatchKeyEvent(ke);
			}
			}

	}
}

