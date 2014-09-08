using System.Configuration;

namespace Server.Core
{
    public class ServerSettings : ConfigurationSection
    {
        [ConfigurationProperty("PortNumber")]
        public int PortNumber
        {
            get { return (int)(this["PortNumber"]); }
            set { base["PortNumber"] = value; }
        }

        [ConfigurationProperty("SitePath")]
        public string SitePath
        {
            get { return (string)(this["SitePath"]); }
            set { base["SitePath"] = value; }
        }

        [ConfigurationProperty("SiteConfigPath")]
        public string SiteConfigPath
        {
            get { return (string)(this["SiteConfigPath"]); }
            set { base["SiteConfigPath"] = value; }
        }
    }
}