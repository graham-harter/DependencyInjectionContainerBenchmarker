using System;
using System.Text;
using DependencyInjectionContainerBenchmarker.Common.Extensions;

namespace DependencyInjectionContainerBenchmarker.Application.DataClasses
{
    /// <summary>
    /// Data class for returning the results of a benchmark test.
    /// </summary>
    public sealed class BenchmarkResult
    {
        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of <see cref="BenchmarkResult"/> with the supplied values.
        /// </summary>
        /// <param name="numberOfTypesCreated">
        /// The number of types used in the benchmark test.
        /// </param>
        /// <param name="timeToRegisterTransientByInterface">
        /// The time taken to register types with transient lifetime by interface.
        /// </param>
        /// <param name="timeToGetInstances">
        /// The time taken to retrieve instances of types from the container.
        /// </param>
        public BenchmarkResult(
            int numberOfTypesCreated,
            TimeSpan timeToRegisterTransientByInterface,
            TimeSpan timeToGetInstances)
        {
            // Make these arguments available to the object.
            NumberOfTypesCreated = numberOfTypesCreated;
            TimeToRegisterTransientByInterface = timeToRegisterTransientByInterface;
            TimeToGetInstances = timeToGetInstances;
        }

        #endregion // #region Constructor(s)

        #region Public properties

        /// <summary>
        /// Gets the number of types used in the benchmark test.
        /// </summary>
        public int NumberOfTypesCreated { get; }

        /// <summary>
        /// Gets the time taken to register types with transient lifetime by interface.
        /// </summary>
        public TimeSpan TimeToRegisterTransientByInterface { get; }

        /// <summary>
        /// Gets the time taken to retrieve instances of types from the container.
        /// </summary>
        public TimeSpan TimeToGetInstances { get; }

        #endregion // #region Public properties

        #region ToString() override

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{GetType().Name} {{");

            sb.Append($"{nameof(NumberOfTypesCreated)} = {NumberOfTypesCreated}");

            sb.Append(", ");
            sb.Append($"{nameof(TimeToRegisterTransientByInterface)} = {TimeToRegisterTransientByInterface.Format()}");

            sb.Append(", ");
            sb.Append($"{nameof(TimeToGetInstances)} = {TimeToGetInstances.Format()}");

            sb.Append("}");

            return sb.ToString();
        }

        #endregion // #region ToString() override
    }
}
