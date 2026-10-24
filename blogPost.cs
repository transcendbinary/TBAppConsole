using System;
using System.Text.Json;
using System.Text.RegularExpressions;

public class blogPost 
{
    string latestPost;
    public string formattedPost;
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
    
    public async Task FetchPost()
    {
        if (!IsOnline())
        {
            Console.WriteLine("\rError 2.0: Cannot Fetch Blog Post - Offline");
            return;
        }
        else if (IsOnline())
        {
            try
            {
                using var client = new HttpClient();
                string json = await client.GetStringAsync(
                    "https://api.transcendbinary.co.uk/blog/index.json"
                );
                JsonDocument doc = JsonDocument.Parse(json);
                var posts = JsonSerializer.Deserialize<List<string>>(json);
                var latestPostTitle = posts[^1];
                latestPost = await client.GetStringAsync($"https://cdn.transcendbinary.co.uk/blog/posts/{latestPostTitle}");
                latestPost = latestPost.Replace("#", "").TrimStart();
                latestPost = Regex.Replace(latestPost, @"\[([^\]]*)\]\([^)]*\)", "$1");
                formattedPost = latestPost;
            }
            catch
            {
                Console.WriteLine("\rError 2.1: Cannot Fetch Blog Post - Connection Error");
            }
        }
    }
}
