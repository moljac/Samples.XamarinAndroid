using Android.App;
using Android.Widget;
using Android.OS;

namespace LocalizationBasics
{
	[Activity (Label = "LocalizationBasic", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			string cultureName = "fr-FR";
			var locale = new Java.Util.Locale(cultureName);
			Java.Util.Locale.Default = locale;

			var config = new Android.Content.Res.Configuration { Locale = locale };
			BaseContext.Resources.UpdateConfiguration(config, BaseContext.Resources.DisplayMetrics);  



			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate
			{
				button.Text = string.Format ("{0} clicks!", count++);
			};
		}
	}
}


