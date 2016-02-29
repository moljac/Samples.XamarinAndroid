using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace HolisticWare.Productivity.Utility
{
	public partial class ReplacementMapping
	{
		public ReplacementMapping ()
		{
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

