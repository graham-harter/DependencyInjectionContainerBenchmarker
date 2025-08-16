using System;
using Unity;
using DependencyInjectionContainerBenchmarker.Common;

namespace UnityAbstraction
{
    /// <summary>
    /// Dependency injection container abstraction for Unity.
    /// </summary>
    public sealed class UnityContainerAbstraction : ContainerAbstraction
    {
        private UnityContainer _container;

        #region ContainerAbstraction implementation

        protected override void HandleCreateContainer()
        {
            _container = new UnityContainer();
        }

        protected override void HandleRegisterTransient<TImplementation>()
        {
            _container.RegisterType<TImplementation>();
        }

        protected override void HandleRegisterTransient<TInterface, TImplementation>()
        {
            _container.RegisterType<TInterface, TImplementation>();
        }

        protected override void HandleRegisterSingleton<TImplementation>()
        {
            _container.RegisterSingleton<TImplementation>();
        }

        protected override void HandleRegisterSingleton<TInterface, TImplementation>()
        {
            _container.RegisterSingleton<TInterface, TImplementation>();
        }

        protected override void HandleRegisterInstance<TImplementation>(TImplementation instance)
        {
            _container.RegisterInstance(instance);
        }

        protected override void HandleRegisterInstance<TInterface, TImplementation>(TImplementation instance)
        {
            _container.RegisterInstance<TInterface>(instance);
        }

        protected override TService HandleGetInstance<TService>()
        {
            return _container.Resolve<TService>();
        }

        #endregion // #region ContainerAbstraction implementation
    }
}
