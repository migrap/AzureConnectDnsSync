using System;

namespace AzureConnectDnsSync {
    internal class SyncConfig : ISyncConfig {
        public string OnPremiseDnsServer { get; set; }

        public TimeSpan Every { get; set; }

        public string Local { get; set; }

        public bool Force { get; set; }

        public SyncConfig() {
        }

        public SyncConfig(string onPremiseDnsServer, TimeSpan every, string local, bool force) {
            OnPremiseDnsServer = onPremiseDnsServer;
            Every = every;
            Local = local;
            Force = force;
        }
    }
}
