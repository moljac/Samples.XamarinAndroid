using System;

namespace System.Collections.Generic
{
    public static class Extensions
    {
		public static void FromJsonValue<TKey, TValue>
						(
			this System.Collections.Generic.Dictionary<TKey, TValue> dictionary,
			System.Json.JsonValue json_value
		)
		{
			dictionary.Clear ();

			foreach (System.Collections.Generic.KeyValuePair<TKey ,System.Json.JsonValue> kvp in json_value)
			{
				TKey key = kvp.Key;
				switch (kvp.Value.JsonType)
				{
					case Json.JsonType.Object :
						break;
					case Json.JsonType.String :
						break;
					case Json.JsonType.Number :
						break;
					case Json.JsonType.Boolean :
						break;
					case Json.JsonType.Array :
						break;
					default:
						break;
				}
				//TValue value = (TValue) kvp.Value;

				//dictionary.Add(key,value);
			}

        	return;
        }
    }   
}