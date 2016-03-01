using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Moka.Lang;
using System.Xml;

namespace HolisticWare.Productivity.XamarinAndroid.JavaUtilities
{
	public class JarInfo
	{
		
		public JarInfo ()
		{
            FilePathJar2Xml =
                "/Library/Frameworks/Xamarin.Android.framework/Versions/5.1.9-0/lib/mandroid/jar2xml.jar"
                ;

			this.Classes = new List<Class> ();

            return;
		}


        public string FilePathJar2Xml
        {
            get;
            set;
        }

		public string FileNameJar
		{
			get;
			set;
		}

		public string TextOutputJar2Xml
		{
			get;
			set;
		}

        public string TextErrorJar2Xml
        {
            get;
            set;
        }

        public string TextOutputJarTF
        {
            get;
            set;
        }

        public string TextErrorJarTF
        {
            get;
            set;
        }

		public Dictionary<string,string> TextOutputClassXJavaP
		{
			get;
			set;
		}
        
		public List<Moka.Lang.SyntaxElement> SyntaxElements
		{
			get;
			set;
		}

		public List<Moka.Lang.Class> Classes
		{
			get;
			set;
		}

		public List<Moka.Lang.Interface> Interfaces
		{
			get;
			set;
		}

		public List<Moka.Lang.Package> Packages
		{
			get;
			set;
		}

        public static List<JarInfo> JarsInformation
        {
            get;
            set;
        }

        public static List<JarInfo> AnalyseJars(List<FileInfo> file_infos)
        {
            List<JarInfo> jar_infos = GenerateJarInfoCollection(file_infos);

            JarsInformation = jar_infos;

            foreach (JarInfo ji in jar_infos)
            {

                string output_jar2xml = ji.JarToGoogleAOSPFormatApiXml();
                string output_jartf = ji.JarTF();

                Dictionary<string, string> class_parse = ji.JavaPClassInfo(output_jartf);

                ji.Dump();
            }

            /*
            Task.Run
            (
                async () => 
                {
                    try
                    {
                        jar_infos = new List<JarInfo>();
                        Dictionary<string, JarInfo> filename_jar_x_jar_info = null;
                        filename_jar_x_jar_info = new Dictionary<string, JarInfo> ();



                            foreach (string s in jar_tf_class_array)
                            {   
                                string java_class_info = jar_info.JavaPClassInfo (filename_jar, s).Result;
                            }

                            filename_jar_x_jar_info.Add (filename_jar, jar_info);

                            foreach (KeyValuePair<string, JarInfo> kvp_name_jar_info in filename_jar_x_jar_info)
                            {
                                // Compile with define
                                //      TRACE
                                //      /d:TRACE

                                Trace.WriteLine("jar2xml --jar " + kvp_name_jar_info.Value.FileNameJar );
                                Trace.Indent();
                                Trace.WriteLine(kvp_name_jar_info.Value.TextOutputJar2Xml);
                                Trace.Unindent();

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
                    }
                    catch(System.Exception exc)
                    {
                        string msg = exc.Message;
                        throw;
                    }

                }
            );
            */
            return jar_infos;
        }


        public string JarToGoogleAOSPFormatApiXml()
        {
            string output = null;
            string error = null;
            // java -jar \
            //    /Library/Frameworks/Xamarin.Android.framework/Versions/5.1.9-0/lib/mandroid/jar2xml.jar
            string command_exe = "java";
            string command_args = "-jar" + " " + this.FilePathJar2Xml;

            try
            {
                Console.WriteLine("java --jar jar2xml {0}", this.FileNameJar);
                string arguments = 
                        command_args + 
                        " --jar=" + this.FileNameJar
                        + " " +
                        " --out=/dev/null"
                        ;

                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = @"java"; // Specify exe name.
                start.Arguments = arguments;
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.RedirectStandardError = true;

                using (Process process = Process.Start(start))
                {
                    StringBuilder o = new StringBuilder();
                    StringBuilder e = new StringBuilder();

                    using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                    using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                    {
                        process.OutputDataReceived += (sender, ea) => 
                        {
                            if (ea.Data == null)
                            {
                                outputWaitHandle.Set();
                            }
                            else
                            {
                                o.AppendLine(ea.Data);
                            }
                        };
                        process.ErrorDataReceived += (sender, ea) =>
                        {
                            if (ea.Data == null)
                            {
                                errorWaitHandle.Set();
                            }
                            else
                            {
                                e.AppendLine(ea.Data);
                            }
                        };

                        process.Start();

                        process.BeginOutputReadLine();
                        process.BeginErrorReadLine();
                        int timeout = 30 * 1000;
                        if 
                            (
                                process.WaitForExit(timeout) 
                                &&
                                outputWaitHandle.WaitOne(timeout) 
                                &&
                                errorWaitHandle.WaitOne(timeout)
                            )
                        {
                            // Process completed. Check process.ExitCode here.
                            Trace.WriteLine("output = ");
                            Trace.WriteLine("output = ");
                            Trace.WriteLine(o.ToString());
                            Trace.WriteLine("error = ");
                            Trace.WriteLine(e.ToString());

                            output = o.ToString();
                            error = e.ToString();
                        }
                        else
                        {
                            // Timed out.
                        }

                    }
                    process.WaitForExit();
                    process.Close();
                }

                TextOutputJar2Xml = output;
                TextErrorJar2Xml = error;
            }
            catch(System.Exception exc)
            {
                string msg = exc.Message;
                throw;
            }

            Trace.WriteLine("Error = ");
            Trace.WriteLine(error);
            Trace.WriteLine("Output = ");
            Trace.WriteLine(output);

            return TextOutputJar2Xml;
        }

		public string JarTF ()
		{
            string output = null;
            string error = null;
            Trace.WriteLine("jar tf {0}", this.FileNameJar);

            string arguments = " -tf " + this.FileNameJar;
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"jar"; // Specify exe name.
            start.Arguments = arguments;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;


			using (Process process = Process.Start(start))
			{
                // Read in all the text from the process with the StreamReader.
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                    output = result;
                }

                using (StreamReader reader = process.StandardError)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                    error = result;
                }

				process.WaitForExit();
				process.Close();
			}

    		TextOutputJarTF = output;
            
			return output;
		}

        public Dictionary<string, string> JavaPClassInfo(string jar_tf_output)
        {
            string[] output_jartf_array = null;
            output_jartf_array = jar_tf_output.Split
                                                (
                                                    new string[] 
                                                    {
                                                        Environment.NewLine
                                                    }, 
                                                    StringSplitOptions.None
                                                );

            string[] output_lines = TextOutputJarTF.Split
                                                        (
                                                            new string[] 
                                                            {
                                                                Environment.NewLine
                                                            }, 
                                                            StringSplitOptions.None
                                                        );
            
            TextOutputClassXJavaP = new Dictionary<string, string>();
                                                            
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
                    string javap_output = JavaPClassInfo(this.FileNameJar,jar_tf_class_for_javap);

                    TextOutputClassXJavaP.Add(jar_tf_class_for_javap, javap_output);

                    List<SyntaxElement> syntax_elements = ParseJavaPClassInfo(javap_output);
                }
            }

            return TextOutputClassXJavaP;
        }

        public List<SyntaxElement> ParseJavaPClassInfo(string javap_output)
        {
            string[] lines = javap_output.Split
                            (
                                 new string[]
						                {
						                    Environment.NewLine
						                }, 
                                 StringSplitOptions.None
                             );

            Dictionary<string, string> parts = new Dictionary<string, string>();

			Class c = new Class ();

            foreach (string l in lines)
            {
                string compiled_from = null;
 
                if (l.StartsWith("Compiled from \""))
                {
                    string tmp = l.Replace("\"", "").Replace("Compiled from ", "");
                    parts.Add("Compiled from", tmp);
					c.JavaPCompiledFrom = tmp;
                }
                if (l.Contains("class"))
                {
					c.ParseClassLine (l);
                }

			}

			List<SyntaxElement> list = new List<SyntaxElement>();

			this.Classes.Add (c);


            return list;
        }

		int i = 0;

		public string JavaPClassInfo (string filename_jar, string jar_tf_class_for_javap)
		{
			string output = "";
			string error = "";

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
			Trace.WriteLine(@"	javap " + i + "    " + arguments);

			ProcessStartInfo start = new ProcessStartInfo();
			start.FileName = @"javap"; // Specify exe name.
			start.Arguments=arguments;
			start.UseShellExecute = false;
			start.RedirectStandardOutput = true;
			start.RedirectStandardError = true;

			using (Process process = Process.Start(start))
			{
				// Read in all the text from the process with the StreamReader.
				using (StreamReader reader = process.StandardOutput)
				{
					string result = reader.ReadToEnd();
					Console.Write(result);
					output = result;
				}

				using (StreamReader reader = process.StandardError)
				{
					string result = reader.ReadToEnd();
					Console.Write(result);
					error = result;
				}
				process.WaitForExit();
				process.Close();
			}

			return output;
		}



        public static List<JarInfo> GenerateJarInfoCollection(List<FileInfo> file_infos)
        {
            List<JarInfo> jar_infos = new List<JarInfo>();
            foreach (FileInfo fi in file_infos)
            {
                JarInfo jar_info = new JarInfo() 
                {
                   FileNameJar = fi.FullName
                };
                jar_infos.Add(jar_info);
            }

            return jar_infos;
        }

        public void Dump()
        {
            string fn = this.FileNameJar;
            string dump_folder = this.FileNameJar + ".dump";

            Directory.CreateDirectory(dump_folder);

			DumpJar2XmlApiXml (dump_folder);

			DumpJarTFOutput (dump_folder);
				
			DumpJavaPClassPathOutput (dump_folder);

            return;
        }

		void DumpJar2XmlApiXml (string dump_folder)
		{
			File.WriteAllText 
				(
					Path.Combine (dump_folder, "jar2xml.api.xml"), 
					this.TextOutputJar2Xml
				);

			return;
		}

		void DumpJarTFOutput (string dump_folder)
		{
			File.WriteAllText 
				(
					Path.Combine (dump_folder, "jar-tf.txt"), 
					this.TextOutputJarTF
				);

			return;
		}

		void DumpJavaPClassPathOutput (string dump_folder)
		{
			foreach (KeyValuePair<string, string> kvp in this.TextOutputClassXJavaP)
			{
				File.WriteAllText 
					(
						Path.Combine (dump_folder, "javap-" + kvp.Key + ".txt"), 
						kvp.Value
					);

				File.WriteAllText 
				(
					Path.Combine (dump_folder, "Metadata-javap-" + kvp.Key + ".xml"),
					kvp.Value
				);
			}
		}

	}
}

