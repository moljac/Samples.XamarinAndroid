
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
using Android.Hardware.Usb;

using Android;

[assembly: UsesFeature("android.hardware.usb.host")]
//[assembly: UsesPermission(Manifest.Permission.HardwareTest)]

namespace HolisticWare.USBReader
{
	[Activity (Label = "HolisticWare.USBReader.ActivityUSBReader")]	
	[IntentFilter 
		(
			new[] 
				{ 
					UsbManager.ActionUsbDeviceAttached,
					UsbManager.ActionUsbDeviceDetached 
				})]
		
	public class ActivityUSBReader : Activity
	{
		Sample.HolisticWare.USBReader.XamarinAndroid.USBDeviceAttachedReceiver _broadcastReceiverA = null;
		Sample.HolisticWare.USBReader.XamarinAndroid.USBDeviceDetachedReceiver _broadcastReceiverD = null;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			if (_broadcastReceiverA != null || _broadcastReceiverD != null) 
			{
      			throw new InvalidOperationException 
      				(
          				"Network status monitoring already active."
          			);
			  }

			// Create the broadcast receiver and bind the event handler
			// so that the app gets updates of the network connectivity status

			// UsbDevice device = (UsbDevice) Intent.GetParcelableExtra(UsbManager.ExtraDevice);

			return;
		}

		protected override void OnResume ()
		{
			base.OnResume();

			if (null == _broadcastReceiverA)
			{

				_broadcastReceiverA = new Sample.HolisticWare.USBReader.XamarinAndroid.USBDeviceAttachedReceiver ();
				// Register the broadcast receiver
				//Application.Context.RegisterReceiver 
				this.RegisterReceiver 
										(
											_broadcastReceiverA,
											new IntentFilter (UsbManager.ActionUsbDeviceAttached)
										);
			}

			if (null == _broadcastReceiverD)
			{
				_broadcastReceiverD = new Sample.HolisticWare.USBReader.XamarinAndroid.USBDeviceDetachedReceiver ();
				//Application.Context.RegisterReceiver
				this.RegisterReceiver 
										(
											_broadcastReceiverD,
				                            new IntentFilter (UsbManager.ActionUsbDeviceDetached)
				                        );
			}

			return;
		}

		protected override void OnPause ()
		{
			base.OnPause();
			try
		    {
				if ( null != _broadcastReceiverA )
				{
					UnregisterReceiver(_broadcastReceiverA);
				}
				if ( null != _broadcastReceiverD )
				{
					UnregisterReceiver(_broadcastReceiverD);
				}
			}
			catch(Java.Lang.IllegalArgumentException exc) 
			{
				string msg = "Java.Lang.IllegalArgumentException = " + exc.Message;
				System.Diagnostics.Debug.WriteLine(msg);
				Toast.MakeText(this, msg, ToastLength.Long); 
			}

			return;
		}

		protected override void OnDestroy ()
		{
			base.OnDestroy();

			return;
		}

		public override bool OnKeyDown(Keycode key_code, KeyEvent event_key) 
		{
			Android.Util.Log.Info("TAG", ""+ key_code);

			//I think you'll have to manually check for the digits and do what you want with them.
			//Perhaps store them in a String until an Enter event comes in (barcode scanners i've 
			// used can be configured to send an enter keystroke after the code)

			return true;
 		}
 	}
}

