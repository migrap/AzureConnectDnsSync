using Ninject;
using System;
using Topshelf;

namespace AzureConnectDnsSync {
    public class Program {
        public static void Main(string[] args) {
            Console.SetBufferSize(999, 999);
            using (var kernel = CreateKernel()) {
                HostFactory.New(x => {
                    x.Service<ITaskService>(s => {
                        s.ConstructUsing(() => kernel.Get<ITaskService>());
                        s.WhenStarted(z => z.Start());
                        s.WhenStopped(z => z.Stop());
                    });

                    x.Service<ISyncService>(s => {
                        s.ConstructUsing(() => kernel.Get<ISyncService>());
                        s.WhenStarted(z => z.Start());
                        s.WhenStopped(z => z.Stop());
                    });
                }).Run();
            }
        }

        private static IKernel CreateKernel() {
            var kernel = new Kernel(new Module());
            return kernel;
        }
    }
}
