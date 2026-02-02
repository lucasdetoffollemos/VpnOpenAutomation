using CredentialManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpnOpenAutomation
{
    public class VpnService
    {
        private const string TARGET_APP = "VpnOpenAutomation";
        public VpnService() { }


        public bool SetCredentials(string? username, string? password)
        {
            // Store or process the provided VPN credentials

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Username and password cannot be null or empty.");
            }

            var cred = new Credential
            {
                Target = TARGET_APP,
                Username = username,
                Password = password,
                PersistanceType = PersistanceType.LocalComputer,
            };

            return cred.Save();

        }


        public void Connect()
        {
            // Implement VPN connection logic here using the provided credentials
            try
            {
                var cred = new Credential { Target = TARGET_APP };
                if (!cred.Load())
                {
                    Console.WriteLine("No credentials found for VPN connection.");
                    return;
                }

                var username = cred.Username;
                var password = cred.Password;

                //logic to connect to VPN using username and password
                Console.WriteLine($"Connecting to VPN with username: {username}");

                bool canRun = RunVpnConnection(username, password);

                if (canRun)
                {
                    Console.WriteLine("VPN connected successfully.");
                    return;
                }

                Console.WriteLine("Failed to connect to VPN.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while connecting to VPN: {ex.Message}");
            }
        }

        private bool RunVpnConnection(string username, string password)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c rasdial VPN-NDDPRINT {username} {password}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using var process = Process.Start(startInfo);
            try
            {
                if (process == null)
                {
                    Console.WriteLine("Failed to start VPN connection process.");
                    return false;
                }
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit(7000);

                Console.WriteLine(output);
                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine($"Error: {error}");
                    return false;
                }

                return true;
            }
            finally
            {
                process?.Close();
            }
            

        }
    }
}
