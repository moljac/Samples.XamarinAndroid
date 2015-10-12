
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

namespace Sample.HolisticWare.USBReader.XamarinAndroid
{
	/// <summary>
	/// USB device attached receiver.
	/// </summary>
	/// <see cref="http://developer.android.com/guide/topics/connectivity/usb/index.html"/>
	/// http://www.codepool.biz/how-to-monitor-usb-events-on-android.html
	[BroadcastReceiver]
	public class USBDeviceAttachedReceiver : BroadcastReceiver
	{
		public override void OnReceive (Context context, Intent intent)
		{
			Toast.MakeText (context, "Received intent!", ToastLength.Short).Show ();

			UsbManager manager = (UsbManager)context.GetSystemService (Context.UsbService);
			IDictionary<string, UsbDevice> deviceList = manager.DeviceList;

			Dictionary<string, string> device_info = new Dictionary<string, string>();

			foreach (KeyValuePair<string, UsbDevice> kvp in deviceList)
			{
				device_info.Add("Name", kvp.Value.DeviceName);
			}

			return;
		}
	}
}

