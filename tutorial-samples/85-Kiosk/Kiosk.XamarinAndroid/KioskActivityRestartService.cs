using System.Collections.Generic;


namespace Kiosk.Sample.XamarinAndroid
{
	/// <summary>
	/// Kiosk service.
	///		Disable the home button and detect when new applications are opened
	///		
	///
	///		Since Android 4 there is no effective method to deactivate the home button. 
	///		That is the reason why we need another little hack. In general the idea is to 
	///		detect when a new application is in foreground and restart your activity 
	///		immediately.
	/// </summary>
	[Android.App.Service]
	public class KioskActivityRestartService<ActivityType> 
		: 
		Android.App.Service 
		where ActivityType : Android.App.Activity

	{

		private static long INTERVAL = Java.Util.Concurrent.TimeUnit.Seconds.ToMillis(2); 
		// periodic interval to check in seconds -> 2 seconds
		private static string TAG = typeof(KioskActivityRestartService<ActivityType>).Name;

		private Java.Lang.Thread t = null;
		private Android.Content.Context ctx = null;
		private bool running = false;


		public override void OnDestroy() 
		{
			Android.Util.Log.Info(TAG, "Stopping service 'KioskService'");
			running =false;
			base.OnDestroy();

			return;
		}

		public override Android.App.StartCommandResult OnStartCommand
														(
															Android.Content.Intent intent, 
															Android.App.StartCommandFlags flags, 
															int startId
														) 
		{
			Android.Util.Log.Info(TAG, "Starting service 'KioskService'");
			running = true;
			ctx = this;

			// start a thread that periodically checks if your app is in the foreground
			t = new Java.Lang.Thread
					(
						() => 
						{
							do 
							{
								HandleKioskMode();
								try
								{
									Java.Lang.Thread.Sleep(INTERVAL);
								} 
								catch (Java.Lang.InterruptedException e) 
								{
									Android.Util.Log.Info(TAG, "Thread interrupted: 'KioskService'");
								}
							} while(running);
							StopSelf();
						}
					);

			t.Start();
			return Android.App.StartCommandResult.NotSticky;
		}

		private void HandleKioskMode() 
		{
			// is Kiosk Mode active? 
			if(SharedPrefferencesUtilities.IsKioskModeActive(ctx)) 
			{
				// is App in background?
				if(IsInBackground()) 
				{
					RestoreApp(); // restore!
				}
			}

			return;
		}

		private bool IsInBackground() 
		{
			Android.App.ActivityManager am = 
					(Android.App.ActivityManager) 
						ctx.GetSystemService(Android.Content.Context.ActivityService);

			List<Android.App.ActivityManager.RunningTaskInfo> taskInfo = null;

			taskInfo = (List<Android.App.ActivityManager.RunningTaskInfo>) am.GetRunningTasks(1);
			Android.Content.ComponentName componentInfo = null;
			componentInfo =  taskInfo[0].TopActivity;

			return (!ctx.ApplicationContext.PackageName.Equals(componentInfo.PackageName));
		}

		private void RestoreApp() 
		{
			// Restart activity
			Android.Content.Intent i = new Android.Content.Intent(ctx, typeof(ActivityType));
			i.AddFlags(Android.Content.ActivityFlags.NewTask);
			ctx.StartActivity(i);

		}

		public override Android.OS.IBinder OnBind(Android.Content.Intent intent) 
		{
			return null;
		}


	}
}