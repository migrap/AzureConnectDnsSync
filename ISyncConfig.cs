using System;

namespace AzureConnectDnsSync {
    internal interface ISyncConfig {
        string OnPremiseDnsServer { get; set; }
        TimeSpan Every { get; set; }        
        string Local { get; set; }
        bool Force { get; set; }
    }
}
