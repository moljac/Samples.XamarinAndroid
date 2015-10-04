using System;

namespace HolisticWare.Productivity.XamarinAndroid.ApiXmlUtilities
{
	public class ApiXmlLocator
	{
		public ApiXmlLocator ()
		{
		}

		public int LocateXPath (string file, string xpath)
		{
			System.Xml.Linq.XDocument xdoc = null;

			xdoc = System.Xml.Linq.XDocument.Load(file, System.Xml.Linq.LoadOptions.SetLineInfo);
			System.Collections.Generic.IEnumerable<System.Xml.Linq.XElement> elements = null;

			elements = xdoc.Descendants("Category");
			int line_number = -1;

			foreach (System.Xml.Linq.XElement e in elements)
			{
			//get line number for element here...
				line_number = 
								((System.Xml.IXmlLineInfo)e).HasLineInfo() 
								? 
								((System.Xml.IXmlLineInfo)e).LineNumber 
								: -1
								;
			}

			return line_number;
		}
	}
}

