
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
			DataScheme = "xamarin-auth",
			DataHost = "localhost",
            /*
            DataSchemes = new[] 
            { 
                //"http", "https" 
                "xamarin-auth"
            },
            DataHosts = new[] 
            { 
                //"*.xamarin.com", "xamarin.com", 
                "localhost", 
                //"127.0.0.1" 
            },
            */
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
