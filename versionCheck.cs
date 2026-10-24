using System;
using System.Diagnostics;
using System.Text.Json;

public class versionCheck
{
    public Version CurrentVersion = new Version("1.0");
    public Version LatestVersion;
    public string downloadLink;
    public bool? available;

    public async Task Check()
    {
        try
        {
            using HttpClient client = new HttpClient();
            string json = await client.GetStringAsync("https://api.transcendbinary.co.uk/updates/CLIApp.json");

            using JsonDocument doc = JsonDocument.Parse(json);

            LatestVersion = new Version(doc.RootElement.GetProperty("latestVersion").GetString());
            downloadLink = doc.RootElement.GetProperty("url").GetString();

            if (LatestVersion > CurrentVersion)
            {
                available = true;
            }
            else
            {
                available = false;
            }
        }
        catch
        {
            available = false;
            return;
        }
    }
}
