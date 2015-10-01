using System;
using System.Collections.Generic;
using System.Text;

namespace AppicationObject
{
	[
		Android.App.Application
						(
						  Label = "ApplicationObject"
						//, Theme = "@android:style/Theme.Holo.Light.DarkActionBar"
						)
	]
	public partial class ApplicationObject : Android.App.Application
	{
		public ApplicationObject(IntPtr handle, Android.Runtime.JniHandleOwnership transfer)
			: base(handle,transfer)
		{
		}

		public override void OnCreate ()
		{
			Console.WriteLine("ApplicationOject.OnCreate");
		}
		// readStream is the stream you need to read
		// writeStream is the stream you want to write to
	}
}
