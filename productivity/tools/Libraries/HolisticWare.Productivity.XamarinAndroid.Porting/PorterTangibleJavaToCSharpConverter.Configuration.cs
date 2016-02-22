using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HolisticWare.Productivity.XamarinAndroid.Porting
{
	public partial class PorterTangibleJavaToCSharpConverter
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

		public async Task<Dictionary<TKey, TValue>> LoadJsonAsync<TKey, TValue> (string filename)
		{
			StringBuilder sb = new StringBuilder ();
			using (StreamReader sr = new StreamReader (filename))
			{
				String line;
				// Read and display lines from the file until the end of 
				// the file is reached.
				while ((line = sr.ReadLine ()) != null)
				{
					sb.AppendLine (line);
				}
			}
			string json_content = sb.ToString ();
			System.Json.JsonValue jv = null;
			try
			{

				jv = System.Json.JsonValue.Parse (json_content);
			}
			catch (Exception exc)
			{
				string msg = exc.Message;
				Trace.WriteLine("Exception JsonValue.Parse: {0}", msg);
				throw; 
			}
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue> ();
			dictionary.FromJsonValue (jv);

			return dictionary;
		}

	}
}

