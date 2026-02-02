using CredentialManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpnOpenAutomation
{
    public class CredentialManager : ICredentialManager
    {
        private const string TARGET_APP = "VpnOpenAutomation";
        public (string userName, string password) LoadCredentials()
        {
            var cred = new Credential { Target = TARGET_APP };
            if (cred.Load())
            {
               return (cred.Username, cred.Password);

            }

            Console.WriteLine("No credentials found for VPN connection.");

            return (string.Empty, string.Empty);
        }

        public bool SetCredentials(string username, string password)
        {
            var cred = new Credential
            {
                Target = TARGET_APP,
                Username = username,
                Password = password,
                PersistanceType = PersistanceType.LocalComputer,
            };

            return cred.Save();
        }
    }
}
