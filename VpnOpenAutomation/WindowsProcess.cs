using System.Diagnostics;

namespace VpnOpenAutomation
{
    public interface IWindowsProcess
    {
        bool RunVpnConnection(string username, string password, VpnType vpnType);
    }

    public class WindowsProcess : IWindowsProcess
    {
        private const string VPN_NDDPRINT = "VPN-NDDPRINT";
        private const string VPN_MFA = "VPN-MFA";

        public bool RunVpnConnection(string username, string password, VpnType vpnType)
        {
            var vpnName = vpnType switch
            {
                VpnType.PRINT => VPN_NDDPRINT,
                VpnType.MFA => VPN_MFA,
                _ => throw new ArgumentOutOfRangeException(nameof(vpnType), "Unsupported VPN type")
            };

            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c rasdial {vpnName} {username} {password}",
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
