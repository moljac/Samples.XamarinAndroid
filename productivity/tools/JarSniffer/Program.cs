using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using HolisticWare.Productivity.Utilities;

using HolisticWare.Productivity.XamarinAndroid.JavaUtilities;

namespace JarSniffer
{
	class MainClass
	{
		public static void Main(string[] args)
        {
            Trace.Setup(args);

            Dictionary<string, List<System.IO.FileInfo>> dir_find = null;
            PathProber pp = new PathProber();
            dir_find = pp.DirectoryRootsWithFiles();

            foreach (KeyValuePair<string, List<System.IO.FileInfo>> kvp in dir_find)
            {
                List<System.IO.FileInfo> file_infos = kvp.Value;

                List<JarInfo> jarinfos = ParseJar(file_infos);

            }

			return;
		}


		public static void WriteEnvironmentInfoToTrace()
		{

			string methodName = "WriteEnvironmentInfoToTrace";

			System.Diagnostics.Trace.Indent();
			System.Diagnostics.Trace.WriteLine(DateTime.Now.ToString() + " - Start of " + methodName);
			System.Diagnostics.Trace.Indent();

			// Write details on the executing environment to the trace output.
			System.Diagnostics.Trace.WriteLine("Operating system: " + System.Environment.OSVersion.ToString());
			System.Diagnostics.Trace.WriteLine("Computer name: " + System.Environment.MachineName);
			System.Diagnostics.Trace.WriteLine("User name: " + System.Environment.UserName);
			System.Diagnostics.Trace.WriteLine("CLR runtime version: " + System.Environment.Version.ToString());
			System.Diagnostics.Trace.WriteLine("Command line: " + System.Environment.CommandLine);

			// Enumerate the trace listener collection and 
			// display details about each configured trace listener.
			System.Diagnostics.Trace.WriteLine("Number of configured trace listeners = " + System.Diagnostics.Trace.Listeners.Count.ToString());

			foreach (System.Diagnostics.TraceListener tl in System.Diagnostics.Trace.Listeners)
			{
			System.Diagnostics.Trace.WriteLine("Trace listener name = " + tl.Name);
			System.Diagnostics.Trace.WriteLine("               type = " + tl.GetType().ToString());
			}

			System.Diagnostics.Trace.Unindent();
			System.Diagnostics.Trace.WriteLine(DateTime.Now.ToString() + " - End of " + methodName);
			System.Diagnostics.Trace.Unindent();

		}

        public static List<JarInfo> ParseJar(List<System.IO.FileInfo> file_infos)
        {
            List<JarInfo> jar_infos = new List<JarInfo>();

            Dictionary<string, JarInfo> filename_jar_x_jar_info = null;

            foreach(System.IO.FileInfo fi in file_infos)
            {
                filename_jar_x_jar_info = new Dictionary<string, JarInfo> ();

                string filename_jar = fi.FullName;
                JarInfo jar_info = new JarInfo ();
                string jar_tf_classes = jar_info.JarTFAsync (filename_jar).Result;

                string[] jar_tf_class_array = jar_tf_classes.Split
                                                                (
                                                                    new string[] 
                                                                    {
                                                                    Environment.NewLine
                                                                    }, 
                                                                    StringSplitOptions.None
                                                                );
                foreach (string s in jar_tf_class_array)
                {   
                    string java_class_info = jar_info.JavaPClassInfo (filename_jar, s).Result;
                }

                filename_jar_x_jar_info.Add (filename_jar, jar_info);

            }

            foreach (KeyValuePair<string, JarInfo> kvp_name_jar_info in filename_jar_x_jar_info)
            {
                // Compile with define
                //      TRACE
                //      /d:TRACE
                System.Diagnostics.Trace.WriteLine("jar = " + kvp_name_jar_info.Key);
                System.Diagnostics.Trace.Indent();
                System.Diagnostics.Trace.WriteLine("jar = " + kvp_name_jar_info.Value.FileNameJar);

                System.Diagnostics.Trace.WriteLine("jar -tf = ");
                System.Diagnostics.Trace.Indent();
                System.Diagnostics.Trace.WriteLine(kvp_name_jar_info.Value.TextOutputJarTF);
                System.Diagnostics.Trace.Unindent();

                System.Diagnostics.Trace.WriteLine("javap -classname " + kvp_name_jar_info.Value.FileNameJar );
                System.Diagnostics.Trace.Indent();
                System.Diagnostics.Trace.WriteLine(kvp_name_jar_info.Value.TextOutputJarTF);
                System.Diagnostics.Trace.Unindent();

                System.Diagnostics.Trace.Indent();
                System.Diagnostics.Trace.WriteLine("SyntaxElements = " + kvp_name_jar_info.Value.SyntaxElements?.Count);
                System.Diagnostics.Trace.WriteLine("Packages       = " + kvp_name_jar_info.Value.Packages?.Count);
                System.Diagnostics.Trace.WriteLine("Classes        = " + kvp_name_jar_info.Value.Classes?.Count);
                System.Diagnostics.Trace.WriteLine("Iterfaces      = " + kvp_name_jar_info.Value.Interfaces?.Count);
                System.Diagnostics.Trace.Unindent();
                System.Diagnostics.Trace.Unindent();
            }
           return jar_infos;
        }
	}
}
