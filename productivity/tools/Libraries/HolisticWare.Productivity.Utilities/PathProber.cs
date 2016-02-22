using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace HolisticWare.Productivity.Utilities
{
    public partial class PathProber
    {
		public Dictionary<string, string[]> Directories
		{
			get;
			set;
		}

		public string[] FileExtensionPattern
		{
			get;
			set;
		}

        public PathProber()
        {
        }

        public Dictionary<string, List<FileInfo>> DirectoryRootsWithFiles()
        {
            Dictionary<string, List<FileInfo>> directory_roots_with_files_found = null;
            directory_roots_with_files_found = new Dictionary<string, List<FileInfo>>();


			foreach (KeyValuePair<string, string[]> kvp in this.Directories)
            {
				string[] files = null;
				List<FileInfo> list_file_info = new List<FileInfo>();
                string path_folder_root = Path.Combine(kvp.Value);

                if (Directory.Exists(path_folder_root))
                {
					foreach (string file_pattern in FileExtensionPattern)
					{
						files = Directory.GetFiles
											(
												path_folder_root, 
												file_pattern, 
												SearchOption.AllDirectories
											);
						
						foreach (string f in files)
						{
							FileInfo fi = new FileInfo(f);
							list_file_info.Add(fi);
						}

						directory_roots_with_files_found.Add
															(
																kvp.Key + " for " + file_pattern, 
																list_file_info
															);
					}
                }

            }

            return directory_roots_with_files_found;
        }
    }
}

