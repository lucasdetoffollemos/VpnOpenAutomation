using System;

namespace VpnOpenAutomation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Begin VPN AUTOMATION");

            var vpnService = new VpnService();

            Console.WriteLine("Do you want to enter your VPN credentials? (if you already have your credentials saved is not needed)(press 1 if you want)");

            if(Console.ReadLine() == "1")
            {
                Console.WriteLine("Enter VPN Username: ");
                var username = Console.ReadLine();
                Console.WriteLine("Enter VPN Password: ");
                var password = Console.ReadLine();

                var saved = vpnService.SetCredentials(username, password);

                if(!saved)
                {
                    Console.WriteLine("Failed to save credentials.");
                    return;
                }
            }
            while (true) 
            {
                vpnService.Connect();

                Console.WriteLine("Press 'q' to quit, or any key to try to connect again");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    return;
                }

            }
        }
    }
}
