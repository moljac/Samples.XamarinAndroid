# Details

HolisticWare USB Reader Component for Xamarin.Android

*	several Point of Sale applications as well as some 
*	field applications like integrating scanning of the MAC addresses of the 
	IP phones for Asterisk IP PBX servers to generate DHCP settings.
	
## Environment setup

### WiFi Debugging

When debugging applications that use USB accessory or host features, user most likely 
will have USB hardware connected to Android-powered device. This will prevent user from 
having an adb connection to the Android-powered device via USB. It is still possible to 
access adb over a network connection. 

To enable adb over a network connection:

Check the list of devices:

	adb devices

To see if test device is listed. If not, first try to restart adb server

	adb kill-server && adb start-server && adb devices

If this does not help. Restart Android device and/or IDE.

When device shows up in the device list, check device's IP address:

	adb shell netcfg

Configure device to listen on WiFi (TCP/IP):

	adb tcpip 5555

Connect to the device over WiFi:

	adb connect $IPADDRESS:5555

To configure device to listen on USB port again:

	adb usb



### Tested Devices

Device list:

*	
*	

## Integration

## Planned



