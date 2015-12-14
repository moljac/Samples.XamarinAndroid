using System;
using System.Collections.Generic;

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

        public Dictionary<string, List<System.IO.FileInfo>> DirectoryRootsWithFiles()
        {
            Dictionary<string, List<System.IO.FileInfo>> directory_roots_with_files_found = null;

            foreach (KeyValuePair<string, string[]> kvp in folders_to_probe)
            {
                string[] files_jars = null;
                List<System.IO.FileInfo> list_file_info_jars = new List<System.IO.FileInfo>();
                string path_folder_root = System.IO.Path.Combine(kvp.Value);

                if (System.IO.Directory.Exists(path_folder_root))
                {
                    files_jars = System.IO.Directory.GetFiles
                                                        (
                                                            path_folder_root, 
                                                            "*.jar", 
                                                            System.IO.SearchOption.AllDirectories
                                                        );

                    foreach (string f in files_jars)
                    {
                        System.IO.FileInfo fi = new System.IO.FileInfo(f);
                        list_file_info_jars.Add(fi);

                        System.Diagnostics.Debug.WriteLine("file found: " + f);
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

