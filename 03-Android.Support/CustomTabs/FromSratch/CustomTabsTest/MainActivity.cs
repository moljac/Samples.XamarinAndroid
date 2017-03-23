using Android.App;
using Android.OS;
using Android.Content;
using Android.Widget;
using Android.Support.CustomTabs;
using System;

namespace CustomTabsTest
{
    [
        Activity
            (
                Label = "CustomTabsTest",
                MainLauncher = true,
                Icon = "@mipmap/icon"
            )
    ]
    [
        IntentFilter
        (
            new[] { "net.holisticware.xamarin.android.customtabstest.HANDLE_AUTHORIZATION_RESPONSE" },
            Categories = new[]
                {
                    Intent.CategoryDefault,
                }
        )
    ]
    public partial class MainActivity : Activity
    {
        string USED_INTENT = "USED_INTENT";

        Button buttonLoginCustomTabsOAuth2 = null;
        EditText editTextLoginCustomTabsOAuth2Uri = null;
        EditText editTextLoginOAuth2FacebookOAuth2AppId = null;
        EditText editTextLoginOAuth2FacebookOAuth2AppUriRedirect = null;

        string url = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            buttonLoginCustomTabsOAuth2 = FindViewById<Button>(Resource.Id.buttonLoginCustomTabsOAuth2);
            editTextLoginCustomTabsOAuth2Uri = FindViewById<EditText>(Resource.Id.editTextLoginCustomTabsOAuth2Uri);

            SetUpDataPublicNonSensitive();
			SetUpDataPrivateSensitiveSecret();

            editTextLoginCustomTabsOAuth2Uri.Text = url;
            buttonLoginCustomTabsOAuth2.Click += ButtonLoginCustomTabsOAuth2_Click;

            return;
        }


        partial void SetUpDataPrivateSensitiveSecret();

		private void SetUpDataPublicNonSensitive()
        {
            url = 
        			/*
                    @"https://www.facebook.com/v2.8/dialog/oauth"
                    + "?" +
                    "client_id={app-id}"
                    + "&" +
                    "redirect_uri={redirect-uri}"
                    */
        			@"https://www.fitbit.com/oauth2/authorize"
        			+ "?" +
        			"response_type=token"
        			+ "&" +
        			"client_id={app-id}"
        			+ "&" +
        			@"redirect_uri={redirect-uri}"
        			+ "&" +
        			"scope=activity%20nutrition%20heartrate%20location%20nutrition%20profile%20settings%20sleep%20social%20weight"
        			+ "&" +
        			"expires_in=604800"
        			;

            return;
		}

        void ButtonLoginCustomTabsOAuth2_Click(object sender, System.EventArgs e)
        {
            editTextLoginCustomTabsOAuth2Uri.Text = url;

            //UseCustomTabsActivityManagerOnly();
            UseCustomTabsActivityManagerWithIntent();

            return;
        }

        void UseCustomTabsActivityManagerOnly()
        {
            CustomTabsActivityManager mgr = new CustomTabsActivityManager(this);
            mgr.CustomTabsServiceConnected += delegate
            {
                mgr.LaunchUrl(url);
            };
            mgr.BindService();
        }

        void UseCustomTabsActivityManagerWithIntent()
        {
            // create & open chrome custom tab
            CustomTabsActivityManager mgr = new CustomTabsActivityManager(this);

            // build custom tab
            CustomTabsIntent.Builder builder = null;
            builder = new CustomTabsIntent.Builder(mgr.Session)
                           .SetToolbarColor(Android.Graphics.Color.Argb(255, 52, 152, 219))
                           .SetShowTitle(true)
                           .EnableUrlBarHiding();

            CustomTabsIntent customTabsIntent = builder.Build();

            // ensures the intent is not kept in the history stack, which makes
            // sure navigating away from it will close it
            customTabsIntent.Intent.AddFlags(Android.Content.ActivityFlags.NoHistory);
            customTabsIntent.LaunchUrl(this, Android.Net.Uri.Parse(url));

            return;
        }

        protected override void OnNewIntent(Android.Content.Intent intent)
        {
            return;
        }


        private void CheckIntent(Intent intent)
        {
            if (intent != null)
            {
                System.String action = intent.Action;
                switch (action)
                {
                    case "net.holisticware.xamarin.android.customtabstest.HANDLE_AUTHORIZATION_RESPONSE":
                        if (!intent.HasExtra(USED_INTENT))
                        {
                            HandleAuthorizationResponse(intent);
                            intent.PutExtra(USED_INTENT, true);
                        }
                        break;
                    default:
                        // do nothing
                        break;
                }
            }

            return;
        }

        protected override void OnStart()
        {
            base.OnStart();
            CheckIntent(this.Intent);

            return;
        }

        private void HandleAuthorizationResponse(Intent intent)
        {
            return;
        }

    }
}

