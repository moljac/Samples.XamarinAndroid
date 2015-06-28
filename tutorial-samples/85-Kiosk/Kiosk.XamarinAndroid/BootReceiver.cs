

/// <uses-permission android:name="android.permission.RECEIVE_BOOT_COMPLETED" />
[assembly: Android.App.UsesPermission
							(
								Android.Manifest.Permission.ReceiveBootCompleted
								// or
								// Name = "android.permission.RECEIVE_BOOT_COMPLETED"
							)
]

namespace Kiosk
{


	/// <summary>
	///		
	///		
	/// </summary>
	/// <see cref="http://forums.xamarin.com/discussion/981/unable-to-start-a-background-service-on-device-bootup"/>
	/// <see cref="http://forums.xamarin.com/discussion/7955/broadcast-receivers"/>
	/// <see cref="http://forums.xamarin.com/discussion/39344/broadcastreceiver-onreceive-while-the-app-is-not-running"/>
	
	[Android.Content.BroadcastReceiver]
	[Android.App.IntentFilter
		(
			new[] 
				{ 
					//[Register ("ACTION_BOOT_COMPLETED")]
					// public const string ActionBootCompleted = "android.intent.action.BOOT_COMPLETED";
					Android.Content.Intent.ActionBootCompleted 
				},
			Categories = new[] { "android.intent.category.HOME" }
		)
	]
	public class BootCompletedBroadcastReciver : Android.Content.BroadcastReceiver
	{
		public override void OnReceive(Android.Content.Context context, Android.Content.Intent intent)
		{
			Android.Content.Intent intent_start = null;

			intent_start = new Android.Content.Intent(context, typeof(KioskActivity));
			intent_start.AddFlags(Android.Content.ActivityFlags.NewTask);
		    context.StartActivity(intent_start);
			
			return;
		}
	}
}