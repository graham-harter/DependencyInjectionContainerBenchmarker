using System;
using SimpleInjector;
using DependencyInjectionContainerBenchmarker.Common;

namespace SimpleInjectorAbstraction
{
    /// <summary>
    /// Dependency injection container abstraction for SimpleInjector.
    /// </summary>
    public sealed class SimpleInjectorContainerAbstraction : ContainerAbstraction
    {
        private Container _container;

        #region ContainerAbstraction implementation

        protected override void HandleCreateContainer()
        {
            _container = new Container();
        }

        protected override void HandleRegisterTransient<TImplementation>()
        {
            _container.Register<TImplementation>();
        }

        protected override void HandleRegisterTransient<TInterface, TImplementation>()
        {
            _container.Register<TInterface, TImplementation>();
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
            return _container.GetInstance<TService>();
        }

        #endregion // #region ContainerAbstraction implementation
    }
}
