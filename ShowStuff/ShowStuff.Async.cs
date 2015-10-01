async Task ShowStuffAsync ()
{
	var client = new HttpClient ();
 
	var content = JsonValue.Parse (await client.GetStringAsync ("http://api.worldbank.org/countries?format=json"));
	int number_of_countries = content [0] ["per_page"];
	int done = 0, error = 0;
 
	CountriesLabel.Text = string.Format ("Countries: {0} done: 0 error: 0", number_of_countries);
 
	foreach (JsonObject c in content [1]) {
		try {
			string country_url = string.Format ("http://api.worldbank.org/countries/{0}/indicators/NY.GDP.MKTP.CD&format=json", (string)c ["id"]);
			var json = JsonValue.Parse (await client.GetStringAsync (country_url));
			var map = await LoadCountryLogoAsync (json ["name"]);
			if (map != null){
				var position = await LookupCountryPositionAsync (c ["longitude"], c ["latitude"]);
				if (position != null){
					AddPin (map, position);
					status.Text = json ["name"];
					++done;
				}
			}
 
		} catch (Exception e) {
			++error;
			status.Text = "Got exception "+ e;
		}
		CountriesLabel.Text = string.Format ("Countries: {0} done: {1} error: {2}", number_of_countries, done, error);
	}
	CountriesLabel.Text = string.Format ("Countries: {0}", number_of_countries);
}