using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HolisticWare.Productivity.XamarinAndroid.Porting;

namespace HarryPotterJavaAndroid
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			PorterTangibleJavaToCSharpConverter p = new PorterTangibleJavaToCSharpConverter();

			p.LoadConfiguration();
			p.LoadFiles();











			return;
		}

	}
}
