using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Linq;
using System.Diagnostics;

namespace HolisticWare.Productivity.XamarinAndroid.JavaUtilities
{
	public class JarInfo
	{
		
		public JarInfo ()
		{
		}

		public string FileNameJar
		{
			get;
			set;
		}

		public List<Java.Lang.Class> Classes
		{
			get;
			set;
		}

		public List<Java.Lang.Package> Packages
		{
			get;
			set;
		}

		public string JarTFAsync (string filename_jar)
		{
			Console.WriteLine("jar tf {0}", filename_jar);

			string arguments = " -tf " + filename_jar;
			ProcessStartInfo start = new ProcessStartInfo();
			start.FileName = @"jar"; // Specify exe name.
			start.Arguments =arguments;
			start.UseShellExecute = false;
			start.RedirectStandardOutput = true;
			start.RedirectStandardError = true;

			string output = "";
			string error = "";
			using (Process process = Process.Start(start))
			{
				//
				// Read in all the text from the process with the StreamReader.
				//
				using (System.IO.StreamReader reader = process.StandardOutput)
				{
					string result = reader.ReadToEnd();
					Console.Write(result);
					output = result;
				}

				using (System.IO.StreamReader reader = process.StandardError)
				{
					string result = reader.ReadToEnd();
					Console.Write(result);
					error = result;
				}
				process.WaitForExit();
				process.Close();
			}

			string[] output_lines = output.Split
										(
											new string[] 
											{
												Environment.NewLine
											}, 
											StringSplitOptions.None
										);

			Classes =  new List<Java.Lang.Class>();
			Packages = new List<Java.Lang.Package>();

			foreach (string java_lang_item_jar_tf in output_lines)
			{
				if(java_lang_item_jar_tf.ToLowerInvariant().StartsWith("meta-inf"))
				{
					continue;
				}

				if(java_lang_item_jar_tf.ToLowerInvariant().EndsWith(".class"))
				{
					// class
					Java.Lang.Class c = Java.Lang.Class.ParseJarTFOutput(java_lang_item_jar_tf);
					Classes.Add(c);
				}
				else
				{
					//package
					Java.Lang.Package p = Java.Lang.Package.Parse(java_lang_item_jar_tf);
					Packages.Add(p);
				}
			}

			return output;
		}

		int i = 0;

		public string JavaPClassInfo (string filename_jar, string jar_tf_class_info)
		{
			if (jar_tf_class_info.StartsWith("META-INF"))
			{
				return "ignored - METADATA";
			}

			if (! jar_tf_class_info.EndsWith(".class"))
			{
				return "ignored - not a class";
			}

			string arguments = 
							" -classpath " 
							+ 
							filename_jar 
							+
							" "
							+ 
							jar_tf_class_info.Replace("/",".").Replace(".class", "")
							;

			i++;
			Console.WriteLine(@"	javap {0}	{1}", i, arguments);

			ProcessStartInfo start = new ProcessStartInfo();
			start.FileName = @"javap"; // Specify exe name.
			start.Arguments=arguments;
			start.UseShellExecute = false;
			start.RedirectStandardOutput = true;
			start.RedirectStandardError = true;

			string output = "";
			string error = "";
			using (Process process = Process.Start(start))
			{
				//
				// Read in all the text from the process with the StreamReader.
				//
				using (System.IO.StreamReader reader = process.StandardOutput)
				{
					string result = reader.ReadToEnd();
					Console.Write(result);
					output = result;
				}

				using (System.IO.StreamReader reader = process.StandardError)
				{
					string result = reader.ReadToEnd();
					Console.Write(result);
					error = result;
				}
				process.WaitForExit();
				process.Close();
			}

			List<Java.Lang.Class> classes = 
				(
					from c in Classes 
						where c.JavaClassFullyQualified == jar_tf_class_info
							select c
				).ToList()
				;

			Java.Lang.Class class_found = classes[0];
			class_found.JavaPOutput = output;

			return output;
		}
	}
}

