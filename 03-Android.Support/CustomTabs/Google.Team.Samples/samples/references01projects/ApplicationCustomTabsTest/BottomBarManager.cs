// Copyright 2016 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

using Android.Content;
using Android.Widget;
using Android.Media;

using Android.Support.CustomTabs;
using Android.App;

namespace ApplicationCustomTabsTest
{
    /// <summary>
    /// A <seealso cref="BroadcastReceiver"/> that manages the interaction with the active Custom Tab.
    /// </summary>
    public class BottomBarManager : BroadcastReceiver
	{
		private static WeakReference<MediaPlayer> sMediaPlayerWeakRef;

		public override void OnReceive(Context context, Intent intent)
		{
			int clickedId = intent.GetIntExtra(CustomTabsIntent.ExtraRemoteviewsClickedId, -1);
			Toast.MakeText(context, "Clicked", Android.Widget.ToastLength.Long).Show();

			CustomTabsSession session = SessionHelper.GetCurrentSession();
			if (session == null)
			{
				return;
			}

			if (clickedId == Resource.Id.play_pause)
			{
                MediaPlayer player = null;
                sMediaPlayerWeakRef.TryGetTarget(out player);

				if (player != null)
				{
					bool isPlaying = player.IsPlaying;
					if (isPlaying)
					{
						player.Pause();
					}
					else
					{
						player.Start();
					}
					// Update the play/stop icon to respect the current state.
					session.SetSecondaryToolbarViews(CreateRemoteViews(context, isPlaying), ClickableIDs, GetOnClickPendingIntent(context));
				}
			}
            /*
			else if (clickedId == Resource.Id.cover)
			{
				// Clicking on the cover image will dismiss the bottom bar.
				session.SetSecondaryToolbarViews(null, null, null);
			}
			*/
		}

		/// <summary>
		/// Creates a RemoteViews that will be shown as the bottom bar of the custom tab. </summary>
		/// <param name="showPlayIcon"> If true, a play icon will be shown, otherwise show a pause icon. </param>
		/// <returns> The created RemoteViews instance. </returns>
		public static RemoteViews CreateRemoteViews(Context context, bool showPlayIcon)
		{
			RemoteViews remoteViews = new RemoteViews(context.PackageName, Resource.Layout.remote_view);

			int iconRes = showPlayIcon ? Resource.Drawable.ic_play : Resource.Drawable.ic_stop;
			remoteViews.SetImageViewResource(Resource.Id.play_pause, iconRes);

			return remoteViews;
		}
        /*
    <Box
        android:id="@+id/cover"
        android:layout_width="48dp"
        android:layout_height="48dp"
        android:layout_margin="4dp"
        android:scaleType="centerCrop"
        android:src="@drawable/cover" />
    <!--
    <Android.Widget.ImageView
        android:id="@+id/cover"
        android:layout_width="48dp"
        android:layout_height="48dp"
        android:layout_margin="4dp"
        android:scaleType="centerCrop"
        android:src="@drawable/cover" />
    -->
         */ 
		/// <returns> A list of View ids, the onClick event of which is handled by Custom Tab. </returns>
		public static int[] ClickableIDs
		{
			get
			{
				return new int[]
                {
                    Resource.Id.play_pause, 
                    //Resource.Id.cover
                };
			}
		}

		/// <returns> The PendingIntent that will be triggered when the user clicks on the Views listed by
		/// <seealso cref="BottomBarManager#getClickableIDs()"/>. </returns>
		public static PendingIntent GetOnClickPendingIntent(Context context)
		{
			Intent broadcastIntent = new Intent(context, typeof(BottomBarManager));
			return PendingIntent.GetBroadcast(context, 0, broadcastIntent, 0);
		}

		/// <summary>
		/// Sets the <seealso cref="MediaPlayer"/> to be used when the user clicks on the RemoteViews.
		/// </summary>
		public static MediaPlayer MediaPlayer
		{
			set
			{
				sMediaPlayerWeakRef = new WeakReference<MediaPlayer>(value);
			}
		}
	}

}
