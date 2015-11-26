using System;

using Android.App;
using Android.Runtime;

namespace HolisticWare.ToolbarSimple
{
	[Application
		(
			Theme="@style/AppThemeHolisticWare"	// descendant of the Theme.AppCompat
		)
	]
	public class ApplicationHolisticWare : Application
	{
		public ApplicationHolisticWare ()
		{
		}

		public ApplicationHolisticWare(IntPtr javaReference, JniHandleOwnership transfer) 
			: base(javaReference, transfer)
	    {
	    }

	}
}

