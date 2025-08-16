using System;

namespace DependencyInjectionContainerBenchmarker.Common.Interfaces
{
    /// <summary>
    /// Interface implemented by classes which provide an abstraction for a dependency injection container.
    /// </summary>
    public interface IContainerAbstraction
    {
        /// <summary>
        /// Create the container.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// If the container has already been created (that is, if this method has been invoked previously on this object).
        /// </exception>
        void CreateContainer();

        /// <summary>
        /// Register a type with the container, with transient object lifetime.
        /// </summary>
        /// <typeparam name="TImplementation">
        /// The type to register.
        /// </typeparam>
        /// <exception cref="InvalidOperationException">
        /// If the container has not yet been created (that is, if the <see cref="CreateContainer"/> method has not yet been invoked on this object).
        /// </exception>
        void RegisterTransient<TImplementation>()
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
        /// <exception cref="InvalidOperationException">
        /// If the container has not yet been created (that is, if the <see cref="CreateContainer"/> method has not yet been invoked on this object).
        /// </exception>
        void RegisterTransient<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface;

        /// <summary>
        /// Register a type with the container as a singleton type.
        /// </summary>
        /// <typeparam name="TImplementation">
        /// The type to register.
        /// </typeparam>
        /// <exception cref="InvalidOperationException">
        /// If the container has not yet been created (that is, if the <see cref="CreateContainer"/> method has not yet been invoked on this object).
        /// </exception>
        void RegisterSingleton<TImplementation>()
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
        /// <exception cref="InvalidOperationException">
        /// If the container has not yet been created (that is, if the <see cref="CreateContainer"/> method has not yet been invoked on this object).
        /// </exception>
        void RegisterSingleton<TInterface, TImplementation>()
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
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="instance"/> argument is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// If the container has not yet been created (that is, if the <see cref="CreateContainer"/> method has not yet been invoked on this object).
        /// </exception>
        void RegisterInstance<TImplementation>(TImplementation instance)
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
        /// <exception cref="InvalidOperationException">
        /// If the container has not yet been created (that is, if the <see cref="CreateContainer"/> method has not yet been invoked on this object).
        /// </exception>
        void RegisterInstance<TInterface, TImplementation>(TImplementation instance)
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
        /// <exception cref="InvalidOperationException">
        /// If the container has not yet been created (that is, if the <see cref="CreateContainer"/> method has not yet been invoked on this object).
        /// </exception>
        TService GetInstance<TService>()
            where TService : class;
    }
}
