using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

using HolisticWare.Productivity.XamarinAndroid.JavaUtilities;

namespace JarSniffer
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			TraceSetup(args);

		    string path_folders = System.IO.Path.Combine (path_project_data);

			System.IO.DirectoryInfo di = new System.IO.DirectoryInfo (path_folders);
			System.IO.FileInfo[] file_infos = di.GetFiles ();

			Dictionary<string, JarInfo> filename_jar_x_jar_info = null;
			filename_jar_x_jar_info = new Dictionary<string, JarInfo> ();

			foreach (System.IO.FileInfo fi in file_infos)
			{
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
			 	//		TRACE
			 	//		/d:TRACE
				Trace.WriteLine("jar = " + kvp_name_jar_info.Key);
				Trace.Indent();
				Trace.WriteLine("jar = " + kvp_name_jar_info.Value.FileNameJar);

				Trace.WriteLine("jar -tf = ");
				Trace.Indent();
				Trace.WriteLine(kvp_name_jar_info.Value.TextOutputJarTF);
				Trace.Unindent();

				Trace.WriteLine("javap -classname " + kvp_name_jar_info.Value.FileNameJar );
				Trace.Indent();
				Trace.WriteLine(kvp_name_jar_info.Value.TextOutputJarTF);
				Trace.Unindent();

				Trace.Indent();
				Trace.WriteLine("SyntaxElements = " + kvp_name_jar_info.Value.SyntaxElements?.Count);
				Trace.WriteLine("Packages       = " + kvp_name_jar_info.Value.Packages?.Count);
				Trace.WriteLine("Classes        = " + kvp_name_jar_info.Value.Classes?.Count);
				Trace.WriteLine("Iterfaces      = " + kvp_name_jar_info.Value.Interfaces?.Count);
				Trace.Unindent();
				Trace.Unindent();
			}

				return;
		}

		public static void TraceSetup(string[] args)
		{
			// Compile with define
			//		TRACE
			//		/d:TRACE

			// Define a trace listener to direct trace output from this method
			// to the console.
			ConsoleTraceListener consoleTracer;

			// Check the command line arguments to determine which
			// console stream should be used for trace output.
			if ((args.Length>0)&&(args[0].ToString().ToLower().Equals("/stderr")))
			// Initialize the console trace listener to write
			// trace output to the standard error stream.
			{
				consoleTracer = new ConsoleTraceListener(true);
			}
			else
			{
			// Initialize the console trace listener to write
			// trace output to the standard output stream.
			consoleTracer = new ConsoleTraceListener();
			}
			// Set the name of the trace listener, which helps identify this 
			// particular instance within the trace listener collection.
			consoleTracer.Name = "mainConsoleTracer";

			// Write the initial trace message to the console trace listener.
			consoleTracer.WriteLine
							(
								DateTime.Now.ToString()
								+
								" ["
								+consoleTracer.Name
								+
								"] - Starting output to trace listener."
								);

			// Add the new console trace listener to 
			// the collection of trace listeners.
			System.Diagnostics.Trace.Listeners.Clear();
			System.Diagnostics.Trace.Listeners.Add
												(
													new System.Diagnostics.TextWriterTraceListener(Console.Out)
												);
			Trace.Listeners.Add(consoleTracer);
			Trace.Listeners.Add(new TextWriterTraceListener("TextWriterOutput.log", "TextWritterListener"));

			// Call a local method, which writes information about the current 
			// execution environment to the configured trace listeners.
			//WriteEnvironmentInfoToTrace();

			// Write the final trace message to the console trace listener.
			consoleTracer.WriteLine
								(
									DateTime.Now.ToString()
									+
									" ["
									+
									consoleTracer.Name
									+
									"] - Ending output to trace listener."
									);

			// Flush any pending trace messages, remove the 
			// console trace listener from the collection,
			// and close the console trace listener.

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
