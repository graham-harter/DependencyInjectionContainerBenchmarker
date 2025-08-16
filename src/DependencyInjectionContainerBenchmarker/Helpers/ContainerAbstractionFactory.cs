using System;
using DependencyInjectionContainerBenchmarker.Common.Interfaces;
using DependencyInjectionContainerBenchmarker.Enumerations;
using SimpleInjectorAbstraction;
using UnityAbstraction;

namespace DependencyInjectionContainerBenchmarker.Helpers
{
    /// <summary>
    /// Class for creating an instance of <see cref="IContainerAbstraction"/> for the specified dependency injection container.
    /// </summary>
    internal sealed class ContainerAbstractionFactory
    {
        /// <summary>
        /// Create an instance of <see cref="IContainerAbstraction"/> for the specified dependency injection container.
        /// </summary>
        /// <param name="containerType">
        /// The type of dependency injection container to create.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="IContainerAbstraction"/> for the specified dependency injection container.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// If the <paramref name="containerType"/> argument is an unrecognized value.
        /// </exception>
        public IContainerAbstraction CreateContainerAbstraction(
            ContainerType containerType)
        {
            IContainerAbstraction result;
            switch (containerType)
            {
                case ContainerType.SimpleInjector:
                    result = new SimpleInjectorContainerAbstraction();
                    break;

                case ContainerType.Unity:
                    result = new UnityContainerAbstraction();
                    break;

                default:
                    throw new ArgumentException(
                        $"Unrecognized value, {containerType}, supplied for the {nameof(containerType)} parameter",
                        nameof(containerType));
            }

            return result;
        }
    }
}
