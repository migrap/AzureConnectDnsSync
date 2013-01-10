using System;

namespace AzureConnectDnsSync {
    internal interface ISyncConfig {
        TimeSpan Every { get; set; }
        string Azure { get; set; }
        string Local { get; set; }
        bool Force { get; set; }
    }
}
