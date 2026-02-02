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


        private readonly ICredentialManager _credentialManager;
        private readonly IWindowsProcess _windowsProcess;
        public VpnService(ICredentialManager credentialManager, IWindowsProcess windowsProcess)
        {
            _credentialManager = credentialManager;
            _windowsProcess = windowsProcess;
        }

        public bool SetCredentials(string? username, string? password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Username and password cannot be null or empty.");
                return false;
            }

            return _credentialManager.SetCredentials(username, password);
        }

        public void Connect()
        {
            var usernamePassword = _credentialManager.LoadCredentials();

            var username = usernamePassword.userName;
            var password = usernamePassword.password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Username and password cannot be empty.");
                return;
            }

            Console.WriteLine($"Connecting to VPN with username: {username}");

            bool canRun = _windowsProcess.RunVpnConnection(username, password);

            if (canRun)
            {
                Console.WriteLine("VPN connected successfully.");
                return;
            }

            Console.WriteLine("Failed to connect to VPN.");
        }
    }
}
