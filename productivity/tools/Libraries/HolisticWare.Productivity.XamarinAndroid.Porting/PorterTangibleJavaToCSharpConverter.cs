using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace HolisticWare.Productivity.XamarinAndroid.Porting
{
	public partial class PorterTangibleJavaToCSharpConverter
	{
		public PorterTangibleJavaToCSharpConverter ()
		{
		}

		public void LoadFiles ()
		{
			string path = ".";

			IEnumerable<string> files = null;
			files = Directory.EnumerateFiles (path, "*.cs", SearchOption.AllDirectories);

			foreach (string file in files)
			{
				Console.WriteLine(file);
			}

			return;
		}

	}
}

