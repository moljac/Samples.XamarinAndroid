﻿
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

namespace Kiosk.Sample.XamarinAndroid
{
	[Application]			
	public class ApplicationKiosk : HolisticWare.Kiosk.KioskApplication
	{
		public ApplicationKiosk(IntPtr handle, JniHandleOwnership transfer)
            : base(handle,transfer)
        {
            // do any initialisation you want here (for example initialising properties)

			return;
        }

		public override void OnCreate() 
		{
			base.OnCreate();
			// Create your application here


			return;
		}
	}
}

