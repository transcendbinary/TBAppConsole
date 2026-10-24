using System;
using System.Text.Json;

public class motdMethod
{
    public string motd;
    string json;

    bool IsOnline()
    {
        try
        {
            using var client = new HttpClient();
            var response = client.GetAsync("https://api.transcendbinary.co.uk").Result;
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
    public async Task setMOTD()
    {
        if (!IsOnline())
        {
            motd = "\rError 1.0: Cannot Fetch MOTD - Offline";
            return;
        }
        else if (IsOnline())
        {
            try
            {
                using var client = new HttpClient();

                string json = await client.GetStringAsync(
                    "https://api.transcendbinary.co.uk/app/motd.json"
                );

                JsonDocument doc = JsonDocument.Parse(json);

                string today = DateTime.Today.ToString("yyyy-MM-dd");

                if (doc.RootElement.GetProperty("special").TryGetProperty(today, out JsonElement special))
                {
                    motd = special.GetString();
                }
                else
                {
                    string[] generic = doc.RootElement
                        .GetProperty("generic")
                        .Deserialize<string[]>();

                    int index = DateTime.Today.DayOfYear % generic.Length;
                    motd = generic[index];
                }
            }
            catch
            {
                motd = "\rError 1.1: Cannot Fetch MOTD - Connection Error";
            }
            return;
        }
    }
}
