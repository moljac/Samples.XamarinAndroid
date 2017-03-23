using System.Collections.Generic;
using System.Text;

using Android.App;
using Android.OS;
using Android.Widget;
using Android.Util;
using Android.Views;
using Android.Text;
using Android.Graphics;
using Android.Support.CustomTabs;
using Android.Content;

namespace ApplicationCustomTabsTest
{
    public partial class MainActivity
    {
		private void PrepareMenuItems(CustomTabsIntent.Builder builder)
		{
			Intent menuIntent = new Intent();
			menuIntent.SetClass(ApplicationContext, this.GetType());
			// Optional animation configuration when the user clicks menu items.
			Bundle menuBundle = ActivityOptions.MakeCustomAnimation
													(
														this,
														Android.Resource.Animation.SlideInLeft,
														Android.Resource.Animation.SlideOutRight
													).ToBundle();
			PendingIntent pi = PendingIntent.GetActivity(ApplicationContext, 0, menuIntent, 0, menuBundle);
			builder.AddMenuItem("Menu entry 1", pi);
		}


		public void PrepareActionButton(CustomTabsIntent.Builder builder)
		{
			// An example intent that sends an email.
			Intent actionIntent = new Intent(Intent.ActionSend);
			actionIntent.SetType("*/*");
			actionIntent.PutExtra(Intent.ExtraEmail, "example@example.com");
			actionIntent.PutExtra(Intent.ExtraSubject, "example");
			PendingIntent pi = PendingIntent.GetActivity(this, 0, actionIntent, 0);
			Bitmap icon = BitmapFactory.DecodeResource(Resources, Resource.Drawable.ic_share);
			builder.SetActionButton(icon, "send email", pi, true);

            return;
		}

		private void PrepareBottomBar(CustomTabsIntent.Builder builder)
		{
			BottomBarManager.MediaPlayer = mMediaPlayer;
			builder.SetSecondaryToolbarViews
								(
									BottomBarManager.CreateRemoteViews(this, true),
									BottomBarManager.ClickableIDs,
									BottomBarManager.GetOnClickPendingIntent(this)
								);

            return;
		}
	}
}