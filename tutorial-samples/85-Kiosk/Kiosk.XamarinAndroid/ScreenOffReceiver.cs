using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kiosk
{
	[Android.Content.BroadcastReceiver]
	public class ScreenOffReceiver : Android.Content.BroadcastReceiver
	{
		public override void OnReceive (Android.Content.Context context, Android.Content.Intent intent)
		{
			if (Android.Content.Intent.ActionScreenOff.Equals(intent.Action))
			{
				KioskApplication ctx = (KioskApplication)context.ApplicationContext;

				// is Kiosk Mode active?
				if (SharedPrefferencesUtilities.IsKioskModeActive (ctx))
				{
					WakeUpDevice (ctx);
				}
			}

			return;
		}
	
		private void WakeUpDevice(KioskApplication context) 
		{
			// get WakeLock reference via AppContext
    		Android.OS.PowerManager.WakeLock wake_lock = context.WakeLock(); 
			if (wake_lock.IsHeld) 
		    {
				wake_lock.Release(); // release old wake lock
			}

			// create a new wake lock...
			wake_lock.Acquire();

			// ... and release again
			wake_lock.Release();
		}

//		private Boolean IsKioskModeActive(Context context) 
//  		{
//    		ISharedPreferences sp = null;
//    		sp = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(context);
//
//			bool is_kiosk_mode = sp.GetBoolean(PREF_KIOSK_MODE, false);
//
//    		return is_kiosk_mode;
//		}
	}

}