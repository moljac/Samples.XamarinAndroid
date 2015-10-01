using System;

using Android.App;
using Android.Hardware.Usb;
using Android.Content;

namespace Sample.PrintingUSB
{
	public class USBAdapter
	{
		private UsbManager mUsbManager;
		private UsbDevice mDevice;
		private PendingIntent mPermissionIntent;
		UsbDeviceConnection connection;
		String TAG = "USB";
	
		public USBAdapter()
		{
			
		}
	
		public void createConn(Context context) 
		{
			mUsbManager = (UsbManager) context.GetSystemService(Context.USB_SERVICE);
			string ACTION_USB_PERMISSION = "com.pradeep.usbprinter.USB_PERMISSION";
			mPermissionIntent = PendingIntent.GetBroadcast(context, 0, new Intent(ACTION_USB_PERMISSION), 0);
			IntentFilter filter = new IntentFilter(ACTION_USB_PERMISSION);
			context.RegisterReceiver(mUsbReceiver, filter);

			return;
		}
	
		private static String ACTION_USB_PERMISSION = "com.pradeep.usbprinter.USB_PERMISSION";

		private BroadcastReceiver mUsbReceiver = new UsbPrintBroadcastReceiver();

	



	}

	public class UsbPrintBroadcastReceiver : BroadcastReceiver
	{
		public void onReceive(Context context, Intent intent) 
		{
			String action = intent.getAction();
			if (ACTION_USB_PERMISSION.equals(action)) 
			{
				//synchronized (this) 
				{
					mDevice = (UsbDevice)intent.getParcelableExtra(UsbManager.EXTRA_DEVICE);

					if (intent.getBooleanExtra(UsbManager.EXTRA_PERMISSION_GRANTED, false)) 
					{
						if(mDevice != null)
						{
							//call method to set up device communication
						}
					} 
					else 
					{
						Log.d(TAG, "permission denied for device " + mDevice);
					}
				}
			}
		}


		public void printMessage(Context context,String msg) 
	{
		// TODO Auto-generated method stub
		final String printdata = msg;
		final UsbEndpoint mEndpointBulkOut;
		if (mUsbManager.hasPermission(mDevice)){
			UsbInterface intf = mDevice.getInterface(0);
			for (int i = 0; i < intf.getEndpointCount(); i++) {
				   UsbEndpoint ep = intf.getEndpoint(i);
				 if (ep.getType() == UsbConstants.USB_ENDPOINT_XFER_BULK) {
					   if (ep.getDirection() == UsbConstants.USB_DIR_OUT) {
				           mEndpointBulkOut = ep;
				               connection = mUsbManager.openDevice(mDevice);
				           if(connection!=null)
				           {
				             Log.e("Connection:"," connected");
				             Toast.makeText(context, "Device connected", Toast.LENGTH_SHORT).show();
				           }
				           boolean forceClaim = true;
				           connection.claimInterface(intf, forceClaim );
				           Integer res = connection.bulkTransfer(mEndpointBulkOut, printdata.getBytes(), printdata.getBytes().length, 10000);
				           new Thread(new Runnable() 
				           { 
					           @Override 
					           public void run() 
					           { 
					               // TODO Auto-generated method stub 
					               Log.i("Thread:", "in run thread"); 
					               byte[] bytes = printdata.getBytes();
					               int b = connection.bulkTransfer(mEndpointBulkOut, bytes, bytes.length, 100000); 
					               Log.i("Return Status", "b-->" + b); 
				               } 
				            }).start(); 

				               connection.releaseInterface(intf);
				           break;
				       }
				}
			}
		}else{
			mUsbManager.requestPermission(mDevice, mPermissionIntent);
			Toast.makeText(context, "Device have no permission", Toast.LENGTH_SHORT).show();
		}
	}
	@SuppressLint("NewApi")
	public void closeConnection(Context context){
		BroadcastReceiver mUsbReceiver = new BroadcastReceiver() {
		    public void onReceive(Context context, Intent intent) {
		        String action = intent.getAction(); 

		      if (UsbManager.ACTION_USB_DEVICE_DETACHED.equals(action)) {
		            UsbDevice device = (UsbDevice)intent.getParcelableExtra(UsbManager.EXTRA_DEVICE);
		            if (device != null) {
		            	Toast.makeText(context, "Device closed", Toast.LENGTH_SHORT).show();
		            	connection.close();
		            }
		        }
		    }
		};

	}
}

