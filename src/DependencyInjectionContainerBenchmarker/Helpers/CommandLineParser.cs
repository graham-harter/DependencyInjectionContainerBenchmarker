using System;
using DependencyInjectionContainerBenchmarker.DataClasses;
using DependencyInjectionContainerBenchmarker.Enumerations;

namespace DependencyInjectionContainerBenchmarker.Helpers
{
    /// <summary>
    /// Helper class for parsing command-line arguments.
    /// </summary>
    internal sealed class CommandLineParser
    {
        /// <summary>
        /// Parse the supplied command-line arguments.
        /// </summary>
        /// <param name="args">
        /// Command-line arguments. This must not be <c>null</c>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ApplicationOptions"/> containing the options under which the application is being executed.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="args"/> argument is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// If the <paramref name="args"/> argument does not contain at least one argument.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If the <paramref name="args"/> argument has specified a dependency injection container type that is unrecognized.
        /// </exception>
        public ApplicationOptions ParseCommandLine(string[] args)
        {
            if (args is null) throw new ArgumentNullException(nameof(args));

            if (args.Length == 0)
            {
                throw new InvalidOperationException(
                    $"At least one argument must be specified: Unity | SimpleInjector");
            }

            var containerTypeArg = args[0];

            containerTypeArg = containerTypeArg.ToLowerInvariant().Trim();

            ContainerType containerType;
            switch (containerTypeArg)
            {
                case "unity":
                    containerType = ContainerType.Unity;
                    break;

                case "simpleinjector":
                    containerType = ContainerType.SimpleInjector;
                    break;

                default:
                    throw new ArgumentException(
                        $"Unrecognized value, \"{containerTypeArg}\", for the container type",
                        nameof(args));
            }

            return new ApplicationOptions(containerType);
        }
    }
}
