using System;

namespace HolisticWare.Productivity.XamarinAndroid.JavaUtilities.Java.Lang
{
	public class Class
	{
		public Package Package
		 {
			get;
			set;
		}
		public Class ()
		{
		}

		public string[] ClassFullyQualifiedParts
		{
			get;
			set;
		}

		public string JavaClassName
		{
			get;
			set;
		}

		public string JavaClassFullyQualified
		{
			get;
			set;
		}

		public string ClassName
		{
			get;
			set;
		}

		public string JavaPOutput
		{
			get;
			set;
		}

		public static Class ParseJarTFOutput (string java_class)
		{
			Class c =  new Class();

			c.JavaClassFullyQualified=java_class;
			c.ClassFullyQualifiedParts = java_class.Split
													(
														new String[]{"/"},
														StringSplitOptions.RemoveEmptyEntries
													);

			int position_class = c.ClassFullyQualifiedParts.Length -1;
			c.JavaClassName = c.ClassFullyQualifiedParts[position_class];
			c.ClassName = c.JavaClassName.Replace(".class", "");
			string[] package_parts = new string[position_class];
			Array.Copy(c.ClassFullyQualifiedParts, package_parts, position_class);
			string pn = string.Join(".", c.ClassFullyQualifiedParts, 0, position_class);
			c.Package = new Package()
			{
				PackageParts = package_parts,
				PackageFullyQualified = pn, 
			};

			return c;
		}
	}
}

