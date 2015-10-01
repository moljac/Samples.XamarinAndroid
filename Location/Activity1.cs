using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using System.Collections.Generic;

namespace Location
{
	[Activity (Label = "Location", MainLauncher = true, Icon = "@drawable/icon")]
	public class Activity1 : Activity, ILocationListener
	{
		Android.Locations.Location _currentLocation;
		LocationManager _locationManager;
		TextView _locationText;
		TextView _addressText;
		String _locationProvider;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			_addressText = FindViewById<TextView> (Resource.Id.address_text);
			_locationText = FindViewById<TextView> (Resource.Id.location_text);
			//FindViewById<TextView> (Resource.Id.get_address_button).Click += AddressButton_OnClick;

			 _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
			
			InitializeLocationManager ();
		}

		void InitializeLocationManager ()
		{
			_locationManager = (LocationManager)GetSystemService (LocationService);
			Criteria criteriaForLocationService = new Criteria {
				Accuracy = Accuracy.Fine
			};
			IList<string> acceptableLocationProviders = _locationManager.GetProviders (criteriaForLocationService, true);

			//if (acceptableLocationProviders.Any ())
			//{
			//	_locationProvider = acceptableLocationProviders.First ();
			//}
			//else
		//	{
		//		_locationProvider = String.Empty;
		//	}
		}
	}
}

