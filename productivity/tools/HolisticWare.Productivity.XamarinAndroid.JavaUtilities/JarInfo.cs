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

		public string TextOutputJarTF
		{
			get;
			set;
		}

		public Dictionary<string,string> TextOutputClassXJavaP
		{
			get;
			set;
		}

		public List<Java.Lang.SyntaxElement> SyntaxElements
		{
			get;
			set;
		}

		public List<Java.Lang.Class> Classes
		{
			get;
			set;
		}

		public List<Java.Lang.Interface> Interfaces
		{
			get;
			set;
		}

		public List<Java.Lang.Package> Packages
		{
			get;
			set;
		}

		public async Task<string> JarTFAsync (string filename_jar)
		{
			string output = "";
			string error = "";
			string javap_output = null;

			Task.Run
				(
					async ()
					=>
					{
						Console.WriteLine("jar tf {0}", filename_jar);

						string arguments = " -tf " + filename_jar;
						ProcessStartInfo start = new ProcessStartInfo();
						start.FileName = @"jar"; // Specify exe name.
						start.Arguments =arguments;
						start.UseShellExecute = false;
						start.RedirectStandardOutput = true;
						start.RedirectStandardError = true;

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

						TextOutputJarTF = output;

						string[] output_lines = output.Split
													(
														new string[] 
														{
															Environment.NewLine
														}, 
														StringSplitOptions.None
													);
						Dictionary<string,string> class_x_javap_output = new Dictionary<string, string>();
															
						foreach (string java_lang_item_jar_tf in output_lines)
						{
							if(java_lang_item_jar_tf.ToLowerInvariant().StartsWith("meta-inf"))
							{
								continue;
							}

							if(java_lang_item_jar_tf.ToLowerInvariant().EndsWith(".class"))
							{
								string jar_tf_class_for_javap = null;
								jar_tf_class_for_javap = java_lang_item_jar_tf
																.Replace("/",".")
																.Replace(".class", "")
																;
								javap_output = await JavaPClassInfo(filename_jar,jar_tf_class_for_javap);

								TextOutputClassXJavaP.Add(jar_tf_class_for_javap,javap_output);
							}
						}
					}
				);

			return output;
		}

		int i = 0;

		public async Task<string> JavaPClassInfo (string filename_jar, string jar_tf_class_for_javap)
		{
			string output = "";
			string error = "";

			await Task.Run
					(
						() =>
						{
							string arguments = 
											" -classpath " 
											+ 
											filename_jar 
											+
											" "
											+ 
											jar_tf_class_for_javap
											;

							i++;
							Console.WriteLine(@"	javap {0}	{1}", i, arguments);

							ProcessStartInfo start = new ProcessStartInfo();
							start.FileName = @"javap"; // Specify exe name.
							start.Arguments=arguments;
							start.UseShellExecute = false;
							start.RedirectStandardOutput = true;
							start.RedirectStandardError = true;

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
						}
					);

			return output;
		}
	}
}

