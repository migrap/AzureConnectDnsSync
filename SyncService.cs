using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace AzureConnectDnsSync {
    internal class SyncService : ISyncService {
        private ISyncConfig _conf;
        private ITaskService _task;

        public SyncService(ISyncConfig conf, ITaskService task) {
            _conf = conf;
            _task = task;
        }

        public void Start() {
            this.Log().Info(() => string.Format("Starting {0}", typeof(SyncService).Name));
            _task.Add(() => Run(), x => x.ToRunNow().AndEvery(_conf.Every));
        }

        public void Stop() {
            this.Log().Info(() => string.Format("Stopping {0}", typeof(SyncService).Name));
        }

        private void Run() {
            this.Log().Info(() => string.Format("Running {0}", typeof(SyncService).Name));
                       
            
            var nia = NetworkInterface.GetAllNetworkInterfaces().Where(ni => ni.OperationalStatus == OperationalStatus.Up && ni.Supports(NetworkInterfaceComponent.IPv6)).ToArray();            
            var lanPredicate = new Func<NetworkInterface,bool>(ni => ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet && ni.Name.StartsWith(_conf.Local));            

            if (false == nia.Any(lanPredicate)) {
                this.Log().Info(() => string.Format("Unable to find matching Local connection {0}", _conf.Local));
                return;
            }

            var ping = new Ping();
            var reply = default(PingReply);
            try {
                reply = ping.Send(_conf.OnPremiseDnsServer);
            }
            catch {                
            }

            if (null == reply || reply.Status != IPStatus.Success) {
                this.Log().Info(() => string.Format("Unable to ping on-premise DNS server {0}", _conf.OnPremiseDnsServer));
                return;
            }
            else if (reply.Address.AddressFamily != AddressFamily.InterNetworkV6) {
                this.Log().Info(() => string.Format("Unable to resolve an IPv6 address for on-premise DNS server {0}", _conf.OnPremiseDnsServer));
            }

            var lan = nia.Where(lanPredicate).First();

            var address = reply.Address;

            if ((_conf.Force) || (false == lan.GetIPProperties().DnsAddresses.Contains(address))) {
                var psi = new ProcessStartInfo() {
                    FileName = "netsh.exe",
                    Arguments = string.Format(@"interface ipv6 set dns name=""{0}"" source=static addr={1}", lan.Name, address),
                    CreateNoWindow = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    Verb = "runas",
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                };

                var process = Process.Start(psi);
                process.WaitForExit();
                this.Log().Debug(() => string.Format("{0} {1} -> {2}", psi.FileName, psi.Arguments, process.StandardOutput.ReadToEnd().Replace(Environment.NewLine, " ")).Trim());
            }
        }
    }
}