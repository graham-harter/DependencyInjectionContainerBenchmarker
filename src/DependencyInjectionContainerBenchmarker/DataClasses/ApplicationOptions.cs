using System;
using System.Text;
using DependencyInjectionContainerBenchmarker.Enumerations;

namespace DependencyInjectionContainerBenchmarker.DataClasses
{
    /// <summary>
    /// Data class for holding the options under which the application is being executed.
    /// </summary>
    internal sealed class ApplicationOptions
    {
        #region Constructor(s)

        /// <summary>
        /// Initialize a new instance of <see cref="ApplicationOptions"/> with the supplied values.
        /// </summary>
        /// <param name="containerType">
        /// The type of dependency injection container we are benchmarking.
        /// </param>
        public ApplicationOptions(
            ContainerType containerType)
        {
            // Make these arguments available to the object.
            ContainerType = containerType;
        }

        #endregion // #region Constructor(s)

        #region Public properties

        /// <summary>
        /// Gets the type of dependency injection container we are benchmarking.
        /// </summary>
        public ContainerType ContainerType { get; }

        #endregion // #region Public properties

        #region ToString() override

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{GetType().Name} {{");

            sb.Append($"{nameof(ContainerType)} = {ContainerType}");

            sb.Append("}");

            return sb.ToString();
        }

        #endregion // #region ToString() override
    }
}
