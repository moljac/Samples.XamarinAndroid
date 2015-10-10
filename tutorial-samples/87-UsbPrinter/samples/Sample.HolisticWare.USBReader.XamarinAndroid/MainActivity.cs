using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Sample.HolisticWare.USBReader.XamarinAndroid
{
	[Activity(Label = "Kiosk.Sample.XamarinAndroid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : global::HolisticWare.USBReader.ActivityUSBReader
	{
		Button buttonMusicPlayer = null;
		Button buttonBrowser = null;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
			
			// Get our button from the layout resource,
			// and attach an event to it

			return;
		}
	}
}


