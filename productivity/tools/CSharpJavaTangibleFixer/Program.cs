#define TRACE

using System;
using System.Collections.Generic;
using System.IO;

using HolisticWare.Productivity.Utilities;

namespace CSharpJavaTangibleFixer
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			TraceSetup.Initialize(args);

			PathProber pp = new PathProber () 
			{
				FileExtensionPattern = new string[] { "*.cs" },
				Directories = new Dictionary<string, string[]> () 
				{ 
					{
						"csharp code during tests in project",
						new string[] { "..", "..", "..", "Samples.Data", "java2csharp", "csharp", }
					}, 
					{
						"csharp code in project",
						new string[] { "..", }
					},
				}
			};

			Dictionary<string, List<FileInfo>> dir_find = null;
			dir_find = pp.DirectoryRootsWithFiles();

			foreach (KeyValuePair<string, List<FileInfo>> kvp in dir_find)
			{
				System.Diagnostics.Trace.WriteLine (kvp.Key);

				foreach (FileInfo fi in kvp.Value)
				{
					System.Diagnostics.Trace.WriteLine ("    file = " + fi.FullName);
				}
			}

			return;
		}
	}
}
