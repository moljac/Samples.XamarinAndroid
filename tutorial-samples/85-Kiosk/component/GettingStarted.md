# Getting Started

## Intro

Brightcove is video (multimedia) streaming platform which offers the powerfull 
cloud-based video solutions for driving awareness, engagement, and revenue.

Brightcove can help users quickly and affordably launch video apps on the 
fourth-generation Apple TV.

users can create powerful, engaging video portal experiences. Generate more views, 
more conversions and more time on user's site.

This component is binding of Brightcove's native player SDKs (currently Android only,
but iOS is under test).


## Integration in Xamarin apps

Add Xamarin Brightcove component in Xamarin.Studio or Visual Studio.

Add references from component in the application if they are not added automatically.

Add namespace usings 

	using BrightcoveSDK.Player.Views;
	using BrightcoveSDK.Player.Model;


Xamarin Brightcove component utilizes Brightcove native views (BrightcoveVideoView) which
can be added to any layout:

		<?xml version="1.0" encoding="utf-8"?>
		<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
		    android:orientation="vertical"
		    android:layout_width="fill_parent"
		    android:layout_height="fill_parent">
		    <com.brightcove.player.view.BrightcoveVideoView
		        android:id="@+id/brightcove_video_view"
		        android:layout_width="match_parent"
		        android:layout_height="280dp"
		        android:layout_gravity="center_horizontal|top" />
		</LinearLayout> 


In the activity where video will be used (steramed) add following code:

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.PlayerSimple);
			BrightcoveVideoView brightcoveVideoView = FindViewById<BrightcoveVideoView>(Resource.Id.brightcove_video_view);

	        brightcoveVideoView.Add
	        					(
	        					Video.CreateVideo
	        							(
	        							"http://solutions.brightcove.com/bcls/assets/videos/Bird_Titmouse.mp4", 
	        							BrightcoveSDK.Player.Media.DeliveryType.Mp4
	        							)
	        					);
	        brightcoveVideoView.Start();

        	return;
		}



## Plans

*	iOS support
*	integration with various Brightcove (they need additional bindings)
	*	Playback Formats and Captions
	*	Advertising
		*	Freewheel
		*	Google IMA
		*	OnceUX
	*	Analytics
		*	Omniture
	*	Digital Rights Management (DRM)
		*	Widevine
	*	ChromeCast
*	Xamarin.Forms plugin

## Changelog

### v.0.0.0.1 2015-10-09

Initial version - Android only
