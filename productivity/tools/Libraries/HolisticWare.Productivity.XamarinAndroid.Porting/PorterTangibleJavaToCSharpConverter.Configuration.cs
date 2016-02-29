using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

using HolisticWare.Productivity.Utility;

namespace HolisticWare.Productivity.XamarinAndroid.Porting
{
	public partial class PorterTangibleJavaToCSharpConverter : ReplacementMapping
	{
		public Dictionary<string, string> ReplacementMapping
		{
			get;
			set;
		}

		public Dictionary<string, string[]> FolderRoots
		{
			get;
			set;
		}

		public async Task<bool> LoadConfiguration ()
		{
			bool success = false;

			this.ReplacementMapping = await this.LoadConfigurationReplacementMappingAsync();
			this.FolderRoots = await this.LoadConfigurationFolderRootsAsync();

			success = true;

			return success;
		}

		public async Task<Dictionary<string, string>> LoadConfigurationReplacementMappingAsync ()
		{
			Dictionary<string, string> replacements = null;
			replacements = await LoadJsonAsync<string, string>("Replacements.json");

			return replacements;
		}

		public async Task<Dictionary<string, string[]>> LoadConfigurationFolderRootsAsync ()
		{
			Dictionary<string, string[]> folder_roots = null;
			folder_roots = await LoadJsonAsync<string, string[]>("FolderRoots.json");

			return folder_roots;
		}


	}
}

