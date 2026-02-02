using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpnOpenAutomation
{
    public interface ICredentialManager
    {
        bool SetCredentials(string username, string password);

        (string userName, string password) LoadCredentials();
    }
}
