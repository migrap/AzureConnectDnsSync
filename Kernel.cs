using Ninject;
using Ninject.Activation.Strategies;
using Ninject.Modules;

namespace AzureConnectDnsSync {
    internal class Kernel : StandardKernel {
        public Kernel(params INinjectModule[] modules)
            : base(modules) {
        }

        public Kernel(INinjectSettings settings, params INinjectModule[] modules)
            : base(settings, modules) {
        }

        /// <summary>
        /// Adds components to the kernel during startup.
        /// </summary>
        protected override void AddComponents() {
            base.AddComponents();
            Components.RemoveAll<IActivationStrategy>();
            Components.Add<IActivationStrategy, ActivationCacheStrategy>();
            Components.Add<IActivationStrategy, PropertyInjectionStrategy>();
            Components.Add<IActivationStrategy, MethodInjectionStrategy>();
            //Components.Add<IActivationStrategy, InitializableStrategy>();            
            Components.Add<IActivationStrategy, BindingActionStrategy>();
            Components.Add<IActivationStrategy, DisposableStrategy>();
        }
    }
}
