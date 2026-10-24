using System;
using System.Diagnostics;

motdMethod motd = new motdMethod();
blogPost blog = new blogPost();
statusCheck status = new statusCheck();
versionCheck version = new versionCheck();

bool anyMethodCalled = false;
bool exitStatusMenu = false;

Console.WriteLine("Welcome to the Transcend Binary CLI App!");

Console.Write("Loading");
version.Check();
motd.setMOTD();
while (motd.motd == null || version.available == null)
{
    Console.Write(".");
    System.Threading.Thread.Sleep(250);
}
Console.Write("\rMOTD: " + motd.motd + "\n\n");

if(version.LatestVersion > version.CurrentVersion)
{
    Console.WriteLine("A new version of the Transcend Binary CLI App is available: v" + version.LatestVersion.ToString());
    Console.WriteLine("Do you want to download it? (Y/N)\n");
    if (Console.ReadKey().Key == ConsoleKey.Y)
    {
        Process.Start(new ProcessStartInfo(version.downloadLink) { UseShellExecute = true });
    }
}

while (true)
{
    Console.WriteLine("\rSelect an option:");
    Console.WriteLine("1. See Latest Blog Post");
    Console.WriteLine("2. Visit Website");
    Console.WriteLine("3. Contact Support");
    Console.WriteLine("4. Check Status of Services");
    Console.WriteLine("5. About");
    Console.WriteLine("Esc. Exit");

    var selection = Console.ReadKey();
    Console.Clear();
    switch (selection.Key)
    {
        case ConsoleKey.Escape:
            Console.WriteLine("\nExiting.");
            return;
        case ConsoleKey.D1: //blog post
            anyMethodCalled = true;
            Console.Write("\rLoading");
            blog.FetchPost();
            while (blog.formattedPost == null)
            {
                Console.Write(".");
                System.Threading.Thread.Sleep(250);
            }
            Console.Write("\r" + blog.formattedPost + "\n\n\n");
            break;
        case ConsoleKey.D2: //website
            anyMethodCalled = true;
            Process.Start(new ProcessStartInfo("https://transcendbinary.co.uk") { UseShellExecute = true });
            break;
        case ConsoleKey.D3: //contact
            anyMethodCalled = true;
            Console.WriteLine("1. eMail");
            Console.WriteLine("Press Any Key to Go Back");
            if (Console.ReadKey().Key == ConsoleKey.D1)
            {
                Process.Start(new ProcessStartInfo("mailto:support@transcendbinary.co.uk") { UseShellExecute = true });
            }
            Console.Clear();
            break;
        case ConsoleKey.D4: //Status
            while (!exitStatusMenu)
            {
                anyMethodCalled = true;
                Console.WriteLine("1. Status Page");
                Console.WriteLine("2. Origin");
                Console.WriteLine("3. Main Site");
                Console.WriteLine("4. Beta Site");
                Console.WriteLine("5. CDN");
                Console.WriteLine("6. API");
                Console.WriteLine("7. Full Test");
                Console.WriteLine("Press Any Key to Go Back\n");
                var statusSelection = Console.ReadKey();
                switch (statusSelection.Key)
                {
                    case ConsoleKey.D1:
                        status.Check("status.transcendbinary.co.uk");
                        break;
                    case ConsoleKey.D2:
                        status.Check("origin.transcendbinary.co.uk");
                        break;
                    case ConsoleKey.D3:
                        status.Check("transcendbinary.co.uk");
                        break;
                    case ConsoleKey.D4:
                        status.Check("beta.transcendbinary.co.uk");
                        break;
                    case ConsoleKey.D5:
                        status.Check("cdn.transcendbinary.co.uk");
                        break;
                    case ConsoleKey.D6:
                        status.Check("api.transcendbinary.co.uk");
                        break;
                    case ConsoleKey.D7:
                        status.Check("status.transcendbinary.co.uk");
                        status.Check("origin.transcendbinary.co.uk");
                        status.Check("transcendbinary.co.uk");
                        status.Check("beta.transcendbinary.co.uk");
                        status.Check("cdn.transcendbinary.co.uk");
                        status.Check("api.transcendbinary.co.uk");
                        break;
                    default:
                        exitStatusMenu = true;
                        Console.Clear();
                        break;
                }
            }
            break;
        case ConsoleKey.D5: //About
            anyMethodCalled = true;
            Console.WriteLine("Transcend Binary CLI App");
            Console.WriteLine("Version: v" + version.CurrentVersion.ToString());
            Console.WriteLine("Developed by Transcend Binary");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey(true);
            Console.Clear();
            break;
        default:
            if (!anyMethodCalled)
            {
                Console.WriteLine("Welcome to the Transcend Binary CLI App!");
                Console.Write("\rMOTD: " + motd.motd + "\n\n");
            }
            Console.WriteLine("Invalid selection. Please try again.");
            break;
    }
}
