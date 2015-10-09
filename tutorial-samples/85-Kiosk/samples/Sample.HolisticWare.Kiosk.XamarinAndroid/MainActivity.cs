using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Kiosk.Sample.XamarinAndroid
{
	[Activity(Label = "Kiosk.Sample.XamarinAndroid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : HolisticWare.Kiosk.KioskActivity
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
			buttonMusicPlayer = FindViewById<Button>(Resource.Id.buttonMusicPlayer);
			buttonBrowser = FindViewById<Button>(Resource.Id.buttonBrowser);

			buttonBrowser.Click += ButtonBrowser_Click;
			buttonMusicPlayer.Click += ButtonMusicPlayer_Click;

			return;
		}

		void ButtonMusicPlayer_Click (object sender, EventArgs e)
		{
			this.StartActivity(typeof(MusicPlayerActivity));
			Toast.MakeText(this, "Play Music", ToastLength.Long).Show();

			return;
		}

		void ButtonBrowser_Click (object sender, EventArgs e)
		{
			this.StartActivity(typeof(BrowserActivity));
			Toast.MakeText(this, "Open Browser", ToastLength.Long).Show();

			return;
		}
	}
}

