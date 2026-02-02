using System.Diagnostics;

namespace VpnOpenAutomation
{
    public interface IWindowsProcess
    {
        bool RunVpnConnection(string username, string password);
    }

    public class WindowsProcess : IWindowsProcess
    {
        private const string VPN_NAME = "VPN-NDDPRINT";

        public bool RunVpnConnection(string username, string password)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c rasdial {VPN_NAME} {username} {password}",
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
