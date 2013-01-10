using System;
using System.Configuration;

namespace AzureConnectDnsSync.Configuration {
    public class SyncSection : ConfigurationSection {
        [ConfigurationProperty("Every", DefaultValue = "00:00:30", IsRequired = false)]
        public TimeSpan Every {
            get { return (TimeSpan)base["Every"]; }
            set { base["Every"] = value; }
        }

        [ConfigurationProperty("Azure", DefaultValue = "Windows Azure Connect Relay", IsRequired = false)]
        public string Azure {
            get { return (string)base["Azure"]; }
            set { base["Azure"] = value; }
        }

        [ConfigurationProperty("Local", DefaultValue = "Local Area Connection", IsRequired = false)]
        public string Local {
            get { return (string)base["Local"]; }
            set { base["Local"] = value; }
        }

        [ConfigurationProperty("Force", DefaultValue = false, IsRequired = false)]
        public bool Force {
            get { return (bool)base["Force"]; }
            set { base["Force"] = value; }
        }
    }
}