using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using DependencyInjectionContainerBenchmarker.Application.DataClasses;
using DependencyInjectionContainerBenchmarker.Common.Interfaces;

namespace DependencyInjectionContainerBenchmarker.Application
{
    /// <summary>
    /// Application class for benchmarking the performance of a container.
    /// </summary>
    public sealed class ContainerBenchmarker
    {
        private const int NumberOfTypesToCreate = 2000;
        private static readonly object[] _emptyParameters = new object[0];

        #region Public methods

        /// <summary>
        /// Benchmark the supplied instance of <see cref="IContainerAbstraction"/>.
        /// </summary>
        /// <param name="container">
        /// An instance of <see cref="IContainerAbstraction"/> to be benchmarked. This must not be <c>null</c>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="BenchmarkResult"/> containing the results of the benchmark test.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="container"/> argument is <c>null</c>.
        /// </exception>
        public BenchmarkResult BenchmarkContainer(
            IContainerAbstraction container)
        {
            // Validate argument(s).
            if (container is null) throw new ArgumentNullException(nameof(container));

            // Generate the dynamic types to register with the container.
            var dynamicClassesCreator = new DynamicClassesCreator();
            var createdTypes = dynamicClassesCreator.CreateDynamicClasses(NumberOfTypesToCreate);

            // Create the dependency injection container.
            container.CreateContainer();

            // Time how long it takes to register these types against their interfaces.
            var timeToRegisterTransientByInterface = BenchmarkTransientRegistrationsByInterface(container, createdTypes);

            // Time how long it takes to retrieve these types by their interfaces.
            var timeToGetInstances = BenchmarkGetInstances(container, createdTypes);

            var result = new BenchmarkResult(
                NumberOfTypesToCreate,
                timeToRegisterTransientByInterface,
                timeToGetInstances);

            return result;
        }

        #endregion // #region Public methods

        #region Private methods

        private static TimeSpan BenchmarkTransientRegistrationsByInterface(
            IContainerAbstraction container,
            IList<CreatedTypeInfo> createdTypes)
        {
            return Benchmark(
                container,
                createdTypes,
                GetRegisterTransientMethodByInterface,
                (method, createdType) => method.MakeGenericMethod(createdType.Interface, createdType.Class),
                (ctr, method, createdType) => method.Invoke(ctr, _emptyParameters));
        }

        private static TimeSpan BenchmarkGetInstances(
            IContainerAbstraction container,
            IList<CreatedTypeInfo> createdTypes)
        {
            return Benchmark(
                container,
                createdTypes,
                GetGetInstanceMethod,
                (method, createdType) => method.MakeGenericMethod(createdType.Interface),
                (ctr, method, createdType) => method.Invoke(ctr, _emptyParameters));
        }

        private static TimeSpan Benchmark(
            IContainerAbstraction container,
            IList<CreatedTypeInfo> createdTypes,
            Func<MethodInfo> getBasicMethod,
            Func<MethodInfo, CreatedTypeInfo, MethodInfo> getGenericMethod,
            Func<IContainerAbstraction, MethodInfo, CreatedTypeInfo, object> invokeGenericMethod)
        {
            // Get the basic transient registration method.
            var method = getBasicMethod();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var createdType in createdTypes)
            {
                // Get the generic method for this interface and class type.
                var genericMethod = getGenericMethod(method, createdType);

                // Invoke generic method on the container.
                var invocationResult = invokeGenericMethod(container, genericMethod, createdType);
            }
            stopwatch.Stop();

            var result = stopwatch.Elapsed;
            return result;
        }

        private static MethodInfo GetRegisterTransientMethodByInterface()
        {
            return GetMethod(
                nameof(IContainerAbstraction.RegisterTransient),
                numberOfTypeParameters: 2);
        }

        private static MethodInfo GetGetInstanceMethod()
        {
            return GetMethod(
                nameof(IContainerAbstraction.GetInstance),
                numberOfTypeParameters: 1);
        }

        private static MethodInfo GetMethod(
            string methodName,
            int numberOfTypeParameters)
        {
            var type = typeof(IContainerAbstraction);
            var methods = type
                .GetMember(
                    methodName,
                    MemberTypes.Method,
                    BindingFlags.Public | BindingFlags.Instance);

            foreach (var method in methods)
            {
                var methodInfo = method as MethodInfo;

                var typeParameterInfo = methodInfo.GetGenericArguments();

                if (typeParameterInfo.Length == numberOfTypeParameters) return methodInfo;
            }

            throw new InvalidOperationException($"Error: Cannot locate {methodName} method with expected number of type parameters");
        }

        #endregion // #region Private methods
    }
}
