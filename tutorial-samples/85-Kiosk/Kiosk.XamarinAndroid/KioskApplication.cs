
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
	[Application]
	public class KioskApplication : Android.App.Application
	{
		private KioskApplication instance;
		private PowerManager.WakeLock wake_lock;
		private ScreenOffReceiver screen_off_receiver;

		public KioskApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle,transfer)
        {
            // do any initialisation you want here (for example initialising properties)

			return;
        }

		public override void OnCreate() 
		{
			base.OnCreate();
			instance = this;

			RegisterKioskModeScreenOffReceiver();

			return;
		}

		private void RegisterKioskModeScreenOffReceiver() 
		{
			// register screen off receiver
			IntentFilter filter = new IntentFilter(Intent.ActionScreenOff);
			screen_off_receiver = new ScreenOffReceiver();
			RegisterReceiver(screen_off_receiver, filter);

			System.Diagnostics.Debug.WriteLine("KioskApplication RegisterKioskModeScreenOffReceiver");

			return;
		}

		public PowerManager.WakeLock WakeLock() 
		{
			if(wake_lock == null) 
			{
				// lazy loading: first call, create wakeLock via PowerManager.
				PowerManager pm = (PowerManager) this.GetSystemService(Context.PowerService);
				wake_lock = pm.NewWakeLock
								(
									// PowerManager.FULL_WAKE_LOCK
									WakeLockFlags.Full
									|
									// PowerManager.ACQUIRE_CAUSES_WAKEUP
									WakeLockFlags.AcquireCausesWakeup, 
									"wakeup"
								);
			}

			System.Diagnostics.Debug.WriteLine("KioskApplication WakeLock");

			return wake_lock;
		}

	}
}

