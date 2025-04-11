using POCSemaninhaConsciente;
using System.Text.Json;

internal class Program
{
    private static async Task Main(string[] args)
    {
        string baseDirectory = AppContext.BaseDirectory;
        string projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\"));
        string configPath = Path.Combine(projectRoot, "appsettings.json");
        var config = JsonSerializer.Deserialize<Config>(File.ReadAllText(configPath));

        string apiKey = config?.ApiKey ?? throw new Exception("Api Key não informada");
        Console.WriteLine("Para qual usuário será analisado o LastFM?");
        string user = Console.ReadLine();

        var integrador = new LastFmIntegrator(user,apiKey);
        await integrador.ObterMusicasMaisOuvidas();
    }
}