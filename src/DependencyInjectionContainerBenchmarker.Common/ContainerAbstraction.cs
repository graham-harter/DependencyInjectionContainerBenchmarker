using System;
using DependencyInjectionContainerBenchmarker.Common.Interfaces;

namespace DependencyInjectionContainerBenchmarker.Common
{
    /// <summary>
    /// Abstract base class for dependency injection container abstractions.
    /// </summary>
    public abstract class ContainerAbstraction : IContainerAbstraction
    {
        private bool _hasContainerBeenCreated;

        #region ContainerAbstraction implementation

        public void CreateContainer()
        {
            // Check that we haven't already created the container.
            if (_hasContainerBeenCreated) throw new InvalidOperationException(
                $"Cannot invoke: The {nameof(CreateContainer)} method has already been invoked on this object");

            // Create the container.
            HandleCreateContainer();

            // Record the fact that we have created the container.
            _hasContainerBeenCreated = true;
        }

        public void RegisterTransient<TImplementation>()
            where TImplementation : class
        {
            CheckContainerHasBeenCreated();

            HandleRegisterTransient<TImplementation>();
        }

        public void RegisterTransient<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            CheckContainerHasBeenCreated();

            HandleRegisterTransient<TInterface, TImplementation>();
        }

        public void RegisterSingleton<TImplementation>()
            where TImplementation : class
        {
            CheckContainerHasBeenCreated();

            HandleRegisterSingleton<TImplementation>();
        }

        public void RegisterSingleton<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            CheckContainerHasBeenCreated();

            HandleRegisterSingleton<TInterface, TImplementation>();
        }

        public void RegisterInstance<TImplementation>(TImplementation instance)
            where TImplementation : class
        {
            // Validate argument.
            if (instance is null) throw new ArgumentNullException(nameof(instance));

            CheckContainerHasBeenCreated();

            HandleRegisterInstance(instance);
        }

        public void RegisterInstance<TInterface, TImplementation>(TImplementation instance)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            // Validate argument.
            if (instance is null) throw new ArgumentNullException(nameof(instance));

            CheckContainerHasBeenCreated();

            HandleRegisterInstance<TInterface, TImplementation>(instance);
        }

        public TService GetInstance<TService>()
            where TService : class
        {
            CheckContainerHasBeenCreated();

            return HandleGetInstance<TService>();
        }

        #endregion // #region ContainerAbstraction implementation

        #region Protected abstract methods

        /// <summary>
        /// Create the container.
        /// </summary>
        protected abstract void HandleCreateContainer();

        /// <summary>
        /// Register a type with the container, with transient object lifetime.
        /// </summary>
        /// <typeparam name="TImplementation">
        /// The type to register.
        /// </typeparam>
        protected abstract void HandleRegisterTransient<TImplementation>()
            where TImplementation : class;

        /// <summary>
        /// Register a type with the container, with transient object lifetime, against an interface type.
        /// </summary>
        /// <typeparam name="TInterface">
        /// The interface type which will be used to register this type.
        /// </typeparam>
        /// <typeparam name="TImplementation">
        /// The implementation type to register.
        /// </typeparam>
        protected abstract void HandleRegisterTransient<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface;

        /// <summary>
        /// Register a type with the container as a singleton type.
        /// </summary>
        /// <typeparam name="TImplementation">
        /// The type to register.
        /// </typeparam>
        protected abstract void HandleRegisterSingleton<TImplementation>()
            where TImplementation : class;

        /// <summary>
        /// Register a type with the container as a singleton type, against an interface type.
        /// </summary>
        /// <typeparam name="TInterface">
        /// The interface type which will be used to register this type.
        /// </typeparam>
        /// <typeparam name="TImplementation">
        /// The implementation type to register.
        /// </typeparam>
        protected abstract void HandleRegisterSingleton<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface;

        /// <summary>
        /// Register an instance of a type with the container.
        /// </summary>
        /// <typeparam name="TImplementation">
        /// The type against which to register the instance.
        /// </typeparam>
        /// <param name="instance">
        /// The instance of the type to register. This must not be <c>null</c>.
        /// </param>
        protected abstract void HandleRegisterInstance<TImplementation>(TImplementation instance)
            where TImplementation : class;

        /// <summary>
        /// Register an instance of a type with the container, against an interface type.
        /// </summary>
        /// <typeparam name="TInterface">
        /// The interface type which will be used to register this instance.
        /// </typeparam>
        /// <typeparam name="TImplementation">
        /// The implementation type of which an instance is being registered.
        /// </typeparam>
        /// <param name="instance">
        /// The instance of the implementation type to register. This must not be <c>null</c>.
        /// </param>
        protected abstract void HandleRegisterInstance<TInterface, TImplementation>(TImplementation instance)
            where TInterface : class
            where TImplementation : class, TInterface;

        /// <summary>
        /// Get an instance of <typeparamref name="TService"/> from the container.
        /// </summary>
        /// <typeparam name="TService">
        /// The type of object of which to get an instance from the container.
        /// </typeparam>
        /// <returns>
        /// An instance of <typeparamref name="TService"/> from the container.
        /// </returns>
        protected abstract TService HandleGetInstance<TService>()
            where TService : class;

        #endregion // #region Protected abstract methods

        #region Private methods

        private void CheckContainerHasBeenCreated()
        {
            if (!_hasContainerBeenCreated) throw new InvalidOperationException(
                $"Cannot invoke: The {nameof(CreateContainer)} method must be invoked prior to invoking this method");
        }

        #endregion // #region Private methods
    }
}
