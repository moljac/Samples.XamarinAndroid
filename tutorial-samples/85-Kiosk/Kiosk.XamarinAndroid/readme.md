# Kiosk Component and Application for Xamarin.Android

## Steps

1.	Permissions
	1.	permission to receive the RECEIVE_BOOT_COMPLETED broadcast. 		
		This message means that the phone was booted.		
		```
		<uses-permission android:name="android.permission.RECEIVE_BOOT_COMPLETED" />
		```		
	2.	add Intent Filter for Broadcast receivers			
		```
		<receiver android:name=".BootReceiver">
		    <intent-filter >
		        <action android:name="android.intent.action.BOOT_COMPLETED"/>
		    </intent-filter>
		</receiver>
		```		
	3.	Create a class called BootReceiver that extends BroadcastReceiver 			
		1.	add the code to the onReceive method to start your application			
		```
		```
2.	Disable actions		
	1.	Back button in Activity
		```
		public override void OnBackPressed() 
		{
		    // NOP, do nothing, swallow Back press

			return;
		}
		```		
	2.	Power Off button		
		Difficult to do in Android without changing core OS		
		Detect short press (screen off)







## References / Links

* [http://www.andreas-schrade.de/2015/02/16/android-tutorial-how-to-create-a-kiosk-mode-in-android/v](http://www.andreas-schrade.de/2015/02/16/android-tutorial-how-to-create-a-kiosk-mode-in-android/)
* [https://github.com/andreasschrade/android-kiosk-mode](https://github.com/andreasschrade/android-kiosk-mode)
* [http://arnab.ch/blog/2013/11/developing-kiosk-mode-applications-in-android/](http://arnab.ch/blog/2013/11/developing-kiosk-mode-applications-in-android/)
* [http://sdgsystems.com/blog/implementing-kiosk-mode-android-part-1/](http://sdgsystems.com/blog/implementing-kiosk-mode-android-part-1/)
* [http://sdgsystems.com/blog/implementing-kiosk-mode-android-part-2/](http://sdgsystems.com/blog/implementing-kiosk-mode-android-part-2/)
* [http://sdgsystems.com/blog/implementing-kiosk-mode-android-part-3-android-lollipop/](http://sdgsystems.com/blog/implementing-kiosk-mode-android-part-3-android-lollipop/)
* []()

## Authors 

*	leon s
*	moljac	
	[https://github.com/moljac](https://github.com/moljac)



