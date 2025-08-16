using System;
using DependencyInjectionContainerBenchmarker.Application;
using DependencyInjectionContainerBenchmarker.Common.Extensions;
using DependencyInjectionContainerBenchmarker.Common.Interfaces;
using DependencyInjectionContainerBenchmarker.Helpers;

namespace DependencyInjectionContainerBenchmarker
{
    /// <summary>
    /// This is an application which can be used to benchmark dependency injection containers.
    /// <para>
    /// To execute this, specify the type of dependency injection container as the first command-line argument.
    /// </para>
    /// <para>
    /// Containers currently supported:—<br/>
    /// - SimpleInjector<br/>
    /// - Unity
    /// </para>
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Get the application options from the command-line.
            var parser = new CommandLineParser();
            var applicationOptions = parser.ParseCommandLine(args);

            // Create the dependency injection container abstraction specified on the command-line.
            var containerAbstractionFactory = new ContainerAbstractionFactory();
            IContainerAbstraction containerAbstraction = containerAbstractionFactory.CreateContainerAbstraction(
                applicationOptions.ContainerType);

            // Benchmark this dependency injection container.
            var benchmarker = new ContainerBenchmarker();
            var benchmarkResult = benchmarker.BenchmarkContainer(containerAbstraction);

            // Output results.
            Console.WriteLine($"Container benchmarked: {applicationOptions.ContainerType}");
            Console.WriteLine($"Time taken to register {benchmarkResult.NumberOfTypesCreated} types with transitive lifetime by interface: {benchmarkResult.TimeToRegisterTransientByInterface.Format()}");
            Console.WriteLine($"Time taken to get {benchmarkResult.NumberOfTypesCreated} implementation instances by interface: {benchmarkResult.TimeToGetInstances.Format()}");

            Console.WriteLine("Press [Enter] to finish.");
            Console.ReadLine();
        }
    }
}
