using System;
using System.Text;

namespace VpnOpenAutomation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Begin VPN AUTOMATION");
            var credentialManager = new CredentialManager();
            var windowsProcess = new WindowsProcess();
            var vpnService = new VpnService(credentialManager, windowsProcess);

            Console.WriteLine("If you want to connect to VPN-MFA press 0, if you want VPN-NDDPRINT press anything");

            var vpnChoice = Console.ReadLine();

            while (true)
            {
                Console.WriteLine("Do you want to enter your VPN credentials? (if you already have your credentials saved is not needed)(press 1 if you want)");

                if (Console.ReadLine() == "1")
                {
                    Console.WriteLine("Enter VPN Username: ");
                    var username = Console.ReadLine();

                    var password = ReadPassword("Enter VPN Password: ");

                    var saved = vpnService.SetCredentials(username, password);

                    if (!saved)
                    {
                        Console.WriteLine("Failed to save credentials.");
                        return;
                    }
                }

                if(vpnChoice == "0")
                {
                    vpnService.Connect(VpnType.MFA);
                }
                else
                {
                    vpnService.Connect(VpnType.PRINT);
                }

                Console.WriteLine("Press 'q' to quit, or any key to try to connect again");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    return;
                }

            }
        }

        private static string ReadPassword(string prompt, char mask = '*')
        {
            Console.WriteLine(prompt);
            var sb = new StringBuilder();
            ConsoleKeyInfo key;

            while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.Backspace && sb.Length > 0)
                {
                    sb.Length--;
                    Console.CursorLeft--;
                    Console.Write(" ");
                    Console.CursorLeft--;
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    sb.Append(key.KeyChar);
                    if (mask != '\0') Console.Write(mask);
                }
            }

            Console.WriteLine();
            return sb.ToString();
        }
    }
}
