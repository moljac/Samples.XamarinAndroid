
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
using Android.Webkit;

namespace Kiosk.Sample.XamarinAndroid
{
	[Activity (Label = "BrowserActivity")]			
	public class BrowserActivity : HolisticWare.Kiosk.KioskActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView(Resource.Layout.Browser);

			WebView web_view = FindViewById<WebView>(Resource.Id.webView1);
			web_view.SetWebViewClient(new WebViewClient());
			web_view.LoadUrl("http://holisticware.net");

			return;
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
}

