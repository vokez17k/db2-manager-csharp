using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using IBM.Data.DB2;

namespace DatabaseManager
{
    [Serializable]
    public class ConnectionProfile
    {
        public string ProfileName { get; set; }
        public string Server { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Schema { get; set; }
        public string Password { get; set; } // Note: Should be encrypted in production
    }

    [Serializable]
    public class ConnectionSettings
    {
        public List<ConnectionProfile> Profiles { get; set; } = new List<ConnectionProfile>();
        public string LastUsedProfile { get; set; }
    }


}
