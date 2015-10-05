using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolisticWare.Productivity.XamarinAndroid.JavaUtilities.Java.Lang
{
	public partial class Class : SyntaxElement
	{
		public Class()
		{
			this.JavaPOutputChanged += Class_JavaPOutputChanged;

			return;
		}
	
		public List<Method> Methods
		{
			get;
			set;
		}

		public List<Field> Fields
		{
			get;
			set;
		}
	}
}

