using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace HolisticWare.Productivity.Utilities
{
    public partial class PathProber
    {
        Dictionary<string, string[]> folders_to_probe = null;

        public PathProber()
        {
            folders_to_probe = new Dictionary<string, string[]>();

            folders_to_probe.Add
                    (
                        "jars during tests in project",
                        new string[] { "..", "..", "..", "Samples.Data", "jars"}
                    );
            folders_to_probe.Add
                    (
                        "jars in project",
                        new string[] { "..", "jars"}
                    );
        }

        public Dictionary<string, List<FileInfo>> DirectoryRootsWithFiles()
        {
            Dictionary<string, List<FileInfo>> directory_roots_with_files_found = null;
            directory_roots_with_files_found = new Dictionary<string, List<FileInfo>>();


            foreach (KeyValuePair<string, string[]> kvp in folders_to_probe)
            {
                string[] files_jars = null;
                List<FileInfo> list_file_info_jars = new List<FileInfo>();
                string path_folder_root = Path.Combine(kvp.Value);

                if (Directory.Exists(path_folder_root))
                {
                    files_jars = Directory.GetFiles
                                                        (
                                                            path_folder_root, 
                                                            "*.jar", 
                                                            SearchOption.AllDirectories
                                                        );

                    foreach (string f in files_jars)
                    {
                        FileInfo fi = new FileInfo(f);
                        list_file_info_jars.Add(fi);

                        Trace.WriteLine("file found: " + f);
                    }
                    
                }
                else
                {
                }

                directory_roots_with_files_found.Add(kvp.Key, list_file_info_jars);
            }

            return directory_roots_with_files_found;
        }
    }
}

