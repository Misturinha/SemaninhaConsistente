using HttpClient httpClient = new HttpClient();
try
{
    // Make GET request
    HttpResponseMessage response = await httpClient.GetAsync("https://ws.audioscrobbler.com/2.0/?method=user.getinfo&user=palio_do_mal&api_key=681b18bb53abc9da1ba5d84252cd6aac&format=json");

    // Ensure success status code
    response.EnsureSuccessStatusCode();

    // Read response content
    string responseBody = await response.Content.ReadAsStringAsync();

    Console.WriteLine("Response:");
    Console.WriteLine(responseBody);
}
catch (Exception)
{

	throw;
}
Console.WriteLine("Hello, World!");
