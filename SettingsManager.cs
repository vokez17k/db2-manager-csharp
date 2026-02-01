using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DatabaseManager
{
    public static class SettingsManager
    {
        private static readonly string SettingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "DatabaseManager",
            "connectionsettings.xml");

        public static void SaveSettings(ConnectionSettings settings)
        {
            var dir = Path.GetDirectoryName(SettingsPath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var serializer = new XmlSerializer(typeof(ConnectionSettings));
            using (var writer = new StreamWriter(SettingsPath))
            {
                serializer.Serialize(writer, settings);
            }
        }

        public static ConnectionSettings LoadSettings()
        {
            if (!File.Exists(SettingsPath))
                return new ConnectionSettings();

            var serializer = new XmlSerializer(typeof(ConnectionSettings));
            using (var reader = new StreamReader(SettingsPath))
            {
                return (ConnectionSettings)serializer.Deserialize(reader);
            }
        }

        public static void ExportSettings(string filePath, ConnectionProfile profile)
        {
            var serializer = new XmlSerializer(typeof(ConnectionProfile));
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, profile);
            }
        }

        public static ConnectionProfile ImportSettings(string filePath)
        {
            var serializer = new XmlSerializer(typeof(ConnectionProfile));
            using (var reader = new StreamReader(filePath))
            {
                return (ConnectionProfile)serializer.Deserialize(reader);
            }
        }
    }
}
