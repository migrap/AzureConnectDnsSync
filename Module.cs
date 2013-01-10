using ninject.configurer;
using Ninject;
using Ninject.Modules;

namespace AzureConnectDnsSync {
    internal class Module : NinjectModule {
        public override void Load() {
            Bind<SyncConfig>().ToConfigSection("azure.connect.dns.sync");
            Bind<ISyncConfig>().ToMethod(context=>context.Kernel.Get<SyncConfig>());

            Bind<ITaskService>().To<TaskService>().InSingletonScope();
            Bind<IInitializable>().ToMethod(context => context.Kernel.Get<ITaskService>() as IInitializable);
            Bind<IStartable>().ToMethod(context => context.Kernel.Get<ITaskService>() as IStartable);

            Bind<ISyncService>().To<SyncService>().InSingletonScope();
            Bind<IInitializable>().ToMethod(context => context.Kernel.Get<ISyncService>() as IInitializable);
            Bind<IStartable>().ToMethod(context => context.Kernel.Get<ISyncService>() as IStartable);
        }
    }
}
