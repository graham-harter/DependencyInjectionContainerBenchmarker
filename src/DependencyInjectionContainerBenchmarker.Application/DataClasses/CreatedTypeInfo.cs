using System;
using System.Text;

namespace DependencyInjectionContainerBenchmarker.Application.DataClasses
{
    /// <summary>
    /// Data class for holding information about a created type (and the interface it implements).
    /// </summary>
    internal sealed class CreatedTypeInfo
    {
        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of <see cref="CreatedTypeInfo"/> with the supplied interface and class types.
        /// </summary>
        /// <param name="interface">
        /// The interface which the class implements. This must not be <c>null</c>.
        /// </param>
        /// <param name="class">
        /// The class. This must not be <c>null</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="interface"/> argument is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="class"/> argument is <c>null</c>.
        /// </exception>
        public CreatedTypeInfo(
            Type @interface,
            Type @class)
        {
            // Validate arguments.
            if (@interface is null) throw new ArgumentNullException(nameof(@interface));
            if (@class is null) throw new ArgumentNullException(nameof(@class));

            // Make these arguments available to the object.
            Interface = @interface;
            InterfaceName = @interface.Name;
            Class = @class;
            ClassName = @class.Name;
        }

        #endregion // #region Constructor(s)

        #region Public properties

        /// <summary>
        /// Gets the interface which the class implements.
        /// </summary>
        public Type Interface { get; }

        /// <summary>
        /// Gets the name of the interface which the class implements.
        /// </summary>
        public string InterfaceName { get; }

        /// <summary>
        /// Gets the class.
        /// </summary>
        public Type Class { get; }

        /// <summary>
        /// Gets the name of the class.
        /// </summary>
        public string ClassName { get; }

        #endregion // #region Public properties

        #region ToString() override

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{GetType().Name} {{");

            sb.Append($"{nameof(InterfaceName)} = \"{InterfaceName}\"");

            sb.Append(", ");
            sb.Append($"{nameof(ClassName)} = \"{ClassName}\"");

            sb.Append("}");

            return sb.ToString();
        }

        #endregion // #region ToString() override
    }
}
