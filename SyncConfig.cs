using System;

namespace AzureConnectDnsSync {
    internal class SyncConfig : ISyncConfig {
        public TimeSpan Every { get; set; }

        public string Azure { get; set; }

        public string Local { get; set; }

        public bool Force { get; set; }

        public SyncConfig() {
        }

        public SyncConfig(TimeSpan every, string azure, string local, bool force) {
            Every = every;
            Azure = azure;
            Local = local;
            Force = force;
        }
    }
}
