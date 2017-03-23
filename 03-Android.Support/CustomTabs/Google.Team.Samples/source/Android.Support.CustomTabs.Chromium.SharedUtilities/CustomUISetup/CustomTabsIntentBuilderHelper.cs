using System;
using Android.Content;

namespace Android.Support.CustomTabs.Chromium.SharedUtilities
{
    
    public partial class CustomTabsIntentBuilderHelper
    {
		public Action<CustomTabsIntent.Builder> SetupMenuItems
		{
			get;
			set;
		};

		public Action<CustomTabsIntent.Builder> SetupActionButton
		{
			get;
			set;
		};

		public Action<CustomTabsIntent.Builder> SetupBottomBar
		{
			get;
			set;
		};

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
		}

		private void PrepareBottombar(CustomTabsIntent.Builder builder)
		{
			BottomBarManager.MediaPlayer = mMediaPlayer;
			builder.SetSecondaryToolbarViews
                                (
                                    BottomBarManager.CreateRemoteViews(this, true), 
                                    BottomBarManager.ClickableIDs, 
                                    BottomBarManager.GetOnClickPendingIntent(this)
                                );
		}
		public

		CustomTabsIntentBuilderHelper(CustomTabsIntent.Builder builder)
        {
            this.SetupMenuItems = this.PrepareMenuItems;
            this.SetupActionButton = this.PrepareActionButton;
            this
        }
    }
}
