using System;

namespace HolisticWare.Productivity.XamarinAndroid.JavaUtilities.Java.Lang
{
	public class Package
	{
		public Package ()
		{
		}

		public string[] PackageParts
		{
			get;
			set;
		}

		public string PackageFullyQualified
		{
			get;
			set;
		}

		public static Package Parse (string java_class)
		{
			Package p =  new Package();

			p.PackageParts = java_class.Split
										(
											new String[]{"/"},
											StringSplitOptions.RemoveEmptyEntries
										);
			return p;
		}
	}
}

