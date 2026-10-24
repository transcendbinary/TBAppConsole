using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

public class statusCheck
{
    public bool Check(string host)
    {
        try
        {
            using Ping ping = new Ping();
            PingReply reply = ping.Send(host, 3000);

            Console.WriteLine($"\r{host}: {(reply.Status == IPStatus.Success ? "Online" : "Offline")}\n");

            return reply.Status == IPStatus.Success;
        }
        catch
        {
            Console.WriteLine($"\r{host}: Offline\n");

            return false;
        }
    }
}
