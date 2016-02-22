using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

using HolisticWare.Productivity.Utilities;
using HolisticWare.Productivity.XamarinAndroid.JavaUtilities;

namespace JarSniffer
{
	class MainClass
	{
		public static void Main(string[] args)
        {
            TraceSetup.Initialize(args);

			PathProber pp = new PathProber () 
			{
				FileExtensionPattern = new string[] { "*.jar" },
				Directories = new Dictionary<string, string[]> () 
				{ 
					{
						"jars during tests in project",
						new string[] { "..", "..", "..", "Samples.Data", "jars" }
					}, 
					{
						"jars in project",
						new string[] { "..", "jars" }
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


		public static void WriteEnvironmentInfoToTrace()
		{
			string methodName = "WriteEnvironmentInfoToTrace";

			Trace.Indent();
			Trace.WriteLine(DateTime.Now.ToString() + " - Start of " + methodName);
			Trace.Indent();

			// Write details on the executing environment to the trace output.
			Trace.WriteLine("Operating system: " + System.Environment.OSVersion.ToString());
			Trace.WriteLine("Computer name: " + System.Environment.MachineName);
			Trace.WriteLine("User name: " + System.Environment.UserName);
			Trace.WriteLine("CLR runtime version: " + System.Environment.Version.ToString());
			Trace.WriteLine("Command line: " + System.Environment.CommandLine);

			// Enumerate the trace listener collection and 
			// display details about each configured trace listener.
			Trace.WriteLine("Number of configured trace listeners = " + Trace.Listeners.Count.ToString());

			foreach (TraceListener tl in Trace.Listeners)
			{
			Trace.WriteLine("Trace listener name = " + tl.Name);
			Trace.WriteLine("               type = " + tl.GetType().ToString());
			}

			Trace.Unindent();
			Trace.WriteLine(DateTime.Now.ToString() + " - End of " + methodName);
			Trace.Unindent();

		}

	}
}
