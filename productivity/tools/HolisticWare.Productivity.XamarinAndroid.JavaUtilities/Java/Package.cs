using System;

namespace HolisticWare.Productivity.XamarinAndroid.JavaUtilities.Java.Lang
{
	public partial class Package : SyntaxElement
	{
		public Package ()
		{
		}


		public static Package Parse (string java_class)
		{
			Package p =  new Package();

			p.NameFullyQualifiedParts = java_class.Split
													(
														new String[]{"/"},
														StringSplitOptions.RemoveEmptyEntries
													);
			return p;
		}

	}
}

