
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
	[BroadcastReceiver]
	public class ScreenOffReceiver : BroadcastReceiver
	{
		private static readonly string PREF_KIOSK_MODE = "pref_kiosk_mode";

		public override void OnReceive (Context context, Intent intent)
		{
			if (Intent.ActionScreenOff.Equals(intent.Action))
			{
				KioskApplication ctx = (KioskApplication)context.ApplicationContext;

				// is Kiosk Mode active?
				if (IsKioskModeActive (ctx))
				{
					WakeUpDevice (ctx);
				}
			}

			return;
		}
	
		private void WakeUpDevice(KioskApplication context) 
		{
			// get WakeLock reference via AppContext
    		PowerManager.WakeLock wake_lock = context.WakeLock(); 
			if (wake_lock.IsHeld) 
		    {
				wake_lock.Release(); // release old wake lock
			}

			// create a new wake lock...
			wake_lock.Acquire();

			// ... and release again
			wake_lock.Release();
		}

		private Boolean IsKioskModeActive(Context context) 
  		{
    		ISharedPreferences sp = null;
    		sp = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(context);

			bool is_kiosk_mode = sp.GetBoolean(PREF_KIOSK_MODE, false);

    		return is_kiosk_mode;
		}
	}

}