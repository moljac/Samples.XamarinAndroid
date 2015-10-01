using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Sample.PrintingUSB
{
	[Activity(Label = "Sample.PrintingUSB", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			buttonPrint = FindViewById<Button>(Resource.Id.buttonPrint);
			editTextContent = FindViewById<EditText>(Resource.Id.editTextContent);

			buttonPrint.Click += ButtonPrint_Click;


			UsbSetup();

			return;
		}

		Button buttonPrint = null;
		EditText editTextContent = null;

		void ButtonPrint_Click (object sender, EventArgs e)
		{
			// https://blog.xamarin.com/native-printing-with-android/
			Android.Print.PrintDocumentAdapter print_document_adapter = null;
			print_document_adapter =
							//  webView.CreatePrintDocumentAdapter()
							// editTextContent.CreatePrint
							null 
							;
			var printMgr = (Android.Print.PrintManager)GetSystemService(Context.PrintService);
			printMgr.Print("Print test", print_document_adapter, null);			
		}

		public void UsbSetup ()
		{
			Android.Hardware.Usb.UsbManager manager = (Android.Hardware.Usb.UsbManager)GetSystemService(Context.UsbService);
			Android.Hardware.Usb.UsbDevice device = null;

			System.Collections.Generic.Dictionary<string, Android.Hardware.Usb.UsbDevice> devices = null;
			devices = manager.DeviceList as System.Collections.Generic.Dictionary<string, Android.Hardware.Usb.UsbDevice>;
			string devices_names = "";

			if ( null != devices )
			{
				foreach (var d in devices)
				{
					devices_names += d.Value.DeviceName + System.Environment.NewLine;
				}
			}
			Android.Hardware.Usb.UsbDeviceConnection connection = manager.OpenDevice(device);
        	// Read some data! Most have just one port (port 0).

        	if (String.IsNullOrWhiteSpace(devices_names))
        	{
        		devices_names = "mc++ no devices";
        	}
        	editTextContent.Text = devices_names;

        	return;
		}
	}
}

