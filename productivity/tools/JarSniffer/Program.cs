using System;
using System.Threading.Tasks;

using HolisticWare.Productivity.XamarinAndroid.JavaUtilities;

namespace JarSniffer
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string[] path_project_data = new string[] {
				"..",
				"..",
				"..",
				"jars"
			};


			string path_folders = System.IO.Path.Combine (path_project_data);

			System.IO.DirectoryInfo di = new System.IO.DirectoryInfo (path_folders);
			System.IO.FileInfo[] file_infos = di.GetFiles ();

			foreach (System.IO.FileInfo fi in file_infos)
			{
				
				string filename_jar = fi.FullName;
				JarInfo jar_info = new JarInfo();
				string jar_tf_classes = jar_info.JarTFAsync(filename_jar);

				string[] jar_tf_class_array = jar_tf_classes.Split
																(
																	new string[]{Environment.NewLine}, 
																	StringSplitOptions.None
																);
				foreach(string s in jar_tf_class_array)
				{	
					string java_class_info = jar_info.JavaPClassInfo(filename_jar, s);
				}
			}

			return;
		}	}
}
