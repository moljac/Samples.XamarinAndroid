using System;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace HolisticWare.Productivity.XamarinAndroid.JavaUtilities
{
	public partial class ApiXml
	{
		public static string FormatXml(string inputXml)
		{
			XmlDocument document = new XmlDocument();
			document.Load(new StringReader(inputXml));

			StringBuilder builder = new StringBuilder();
			using (XmlTextWriter writer = new XmlTextWriter(new StringWriter(builder)))
			{
				writer.Formatting = Formatting.Indented;
				document.Save(writer);
			}

			return builder.ToString();
		}
			
		public static Dictionary<string, string> ReplacementsMappings = new Dictionary<string, string> () { {
				"metadata",
				@"
				<?xml version=""1.0"" encoding=""UTF-8"" ?>
				<metadata>
					<!-- PLACEHOLDER_API -->
				</metadata>
				"
			}, {
				"metadata api",
				@"
				<?xml version=""1.0"" encoding=""UTF-8"" ?>
				<metadata>
					<api>
						<!-- PLACEHOLDER_PACKAGE -->
					</api>
				</metadata>
				"
			},
					
		};
	}
}

