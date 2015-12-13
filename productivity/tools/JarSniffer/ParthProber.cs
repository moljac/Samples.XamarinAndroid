using System;
using System.Collections.Generic;

namespace JarSniffer
{
    public class ParthProber
    {
        Dictionary<string, string[]> folders_to_probe = null;

        string[] path_xamarin_android_binding_project_folder_jars = 
                                    new string[] 
                                        {
                                            "..",
                                            "jars"
                                        };

            string[] path_project_data = 
                                    new string[] 
                                        {
                                            "..",
                                            "..",
                                            "..",
                                            "jars"
                                        };


        public ParthProber()
        {
            folders_to_probe = new Dictionary<string, string[]>();

            folders_to_probe.Add
                    (
                        "jars in project",
                        new string[] { "..", "jars"}
                    );
            folders_to_probe.Add
                    (
                        "jars during tests in project",
                        new string[] { "..", "..", "..", "jars"}
                    );
        }
    }
}

