void ShowStuff ()
{
	var client = new WebClient ();
	var content = JsonValue.Parse (client.DownloadString ("http://api.worldbank.org/countries?format=json&per_page=50"));
	int number_of_countries = content [0] ["total"];
	int done = 0, error = 0;
 
	InvokeOnMainThread (() => {
		CountriesLabel.Text = string.Format ("Countries: {0} done: 0 error: 0", number_of_countries);
	});
 
	foreach (JsonObject c in content [1]) {
		string country_url = string.Format ("http://api.worldbank.org/countries/{0}/indicators/NY.GDP.MKTP.CD&format=json", (string)c ["id"]);
		JsonValue json = null;
 
		try {
			json = JsonValue.Parse (client.DownloadString (country_url));
		} catch (Exception e){
			++error;
			InvokeOnMainThread (()=> status.Text = "Got exception "+ e);
			continue;
		}
 
		ThreadPool.QueueUserWorkItem (delegate {
			Map map = null;
			try {
				map = LoadCountryLogo (c ["name"]).Result;
			} catch (Exception e){
				++error;
				InvokeOnMainThread (()=> status.Text = "Got exception "+ e);
			}
			if (map != null){
				ThreadPool.QueueUserWorkItem (delegate {
					Position position = null;
 
					try {
						position = LookupCountryPosition (c ["longitude"], c ["latitude"]).Result;
						if (position != null)
							InvokeOnMainThread (() => {
								AddPin (map, position); 
								++done;
								status.Text = json ["name"];
 
							});
					} catch (Exception e){
						error++;
						InvokeOnMainThread (()=> status.Text = "Got exception "+ e);
					}
 
				});
			}
		});
		
		InvokeOnMainThread (() => CountriesLabel.Text = string.Format ("Countries: {0} done: {1} error: {2}", number_of_countries, done, error));
	}
	InvokeOnMainThread (() => {
		CountriesLabel.Text = string.Format ("Countries: {0}", number_of_countries);
	});
}