
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

namespace CustomTabsTest
{
    /// <summary>
    /// Activity custom tabs callback interceptor.
    /// https://developer.chrome.com/multidevice/android/customtabs
    /// </summary>
    /// <see cref="https://developer.android.com/training/app-links/index.html"/> 
    /// <see cref="http://stackoverflow.com/questions/24236701/how-to-receive-urls-with-xamarin-intent-filters"/>
    /// <see cref="http://stackoverflow.com/questions/42470000/cant-open-youtube-in-custom-tabs#comment72171802_42470934"/>
    /// <see cref="https://github.com/KevinDockx/IdentityModel.OidcClient.Samples/blob/master/AndroidClientChromeCustomTabs/AndroidClientChromeCustomTabs/CallbackInterceptorActivity.cs"/>
    /// 
    [
        Activity
        (
            Label = "ActivityCustomTabsCallbackInterceptor"
        )
    ]
    [
        IntentFilter
        (
            new[] { Intent.ActionView },
            Categories = new[]
                {
                    Intent.CategoryDefault,
                    Intent.CategoryBrowsable
                },
            DataSchemes = new[] 
            { 
                //"http", "https" 
                "xamarin-auth"
            },
            // DataScheme = "https"
            DataHosts = new[] 
            { 
                //"*.xamarin.com", "xamarin.com", 
                "localhost", 
                //"127.0.0.1" 
            },
            // DataHost = "www.xamarin.com",
            DataMimeType = "text/plain"
        )
    ]
    public class ActivityCustomTabsCallbackInterceptor : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            if (!String.IsNullOrEmpty(Intent.GetStringExtra(Intent.ExtraText)))
            {
                string subject = Intent.GetStringExtra(Intent.ExtraSubject) ?? "subject not available";
                Toast.MakeText(this, subject, ToastLength.Long).Show();
            }
            return;
        }

        protected override void OnNewIntent(Intent intent)
        {
            return;
        }
    }
}
