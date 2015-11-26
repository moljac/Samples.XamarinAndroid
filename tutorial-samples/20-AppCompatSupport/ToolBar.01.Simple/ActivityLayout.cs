using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Views;

using Android.Support.V7.App;
using Android.Support.V7.Widget;


namespace HolisticWare.ToolbarSimple
{
	[Activity 
		(
			Label = "HolisticWere.Toolbar.Simple", 
			MainLauncher = true, 
			Icon = "@mipmap/icon",
			ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape
		)
	]
	public class ActivityLayout : AppCompatActivity
	{

		protected override void OnCreate (Bundle savedInstanceState)
		{
			Xamarin.Insights.Initialize (XamarinInsights.ApiKey, this);
			base.OnCreate (savedInstanceState);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.layout_authorization);

			buttonAuthenticate = FindViewById<Button>(Resource.Id.buttonAuthenticate);
			buttonAuthenticate.Click += ButtonAuthenticate_Click;

			InitializeToolbars();

			return;
		}


		Button buttonAuthenticate = null;
		protected void ButtonAuthenticate_Click (object sender, System.EventArgs e)
		{

	       return;
		}

		//==========================================================================================
		Toolbar toolbar_header = null;
		Toolbar toolbar_footer = null;

		private void InitializeToolbars ()
		{
			//---------------------------------------------------------------
			toolbar_header = FindViewById<Toolbar> (Resource.Id.toolbar_header);
			toolbar_footer = FindViewById<Toolbar> (Resource.Id.toolbar_footer);

			//Toolbar will now take on default actionbar characteristics
			// v.5 API (no AppCompat Support)
			//SetActionBar (toolbar);
			// v.4 API (AppCompatSupport)
			SetSupportActionBar(toolbar_header);

			toolbar_footer.InflateMenu(Resource.Menu.menu_bottom);
			toolbar_footer.MenuItemClick += Toolbar_footer_MenuItemClick;
			// works
			//SupportActionBar.SetHomeButtonEnabled(true);
			//SupportActionBar.SetDisplayShowHomeEnabled(true);
			//SupportActionBar.SetDisplayUseLogoEnabled(true);
			//SupportActionBar.SetIcon(Resource.Drawable.Icon);
			SupportActionBar.SetDisplayShowTitleEnabled(false);
			//---------------------------------------------------------------

			return;
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.menu_top, menu);
			return base.OnCreateOptionsMenu (menu);
		}

		void Toolbar_footer_MenuItemClick (object sender, Toolbar.MenuItemClickEventArgs e)
		{
			Toast.MakeText(this, "Bottom ActionBar pressed: " + e.Item.TitleFormatted, ToastLength.Short).Show();
			return;
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{	
			Toast.MakeText(this, "Top ActionBar pressed: " + item.TitleFormatted, ToastLength.Short).Show();
			return base.OnOptionsItemSelected (item);
		}
		//==========================================================================================

	}
}
