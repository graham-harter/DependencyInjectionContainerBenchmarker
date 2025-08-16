using System;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;
using DependencyInjectionContainerBenchmarker.Application.DataClasses;

namespace DependencyInjectionContainerBenchmarker.Application
{
    /// <summary>
    /// Helper class for creating a set of dynamic classes to be used for benchmarking.
    /// </summary>
    internal sealed class DynamicClassesCreator
    {
        #region Public methods

        /// <summary>
        /// Create the specified number of dynamic classes.
        /// </summary>
        /// <param name="numberOfClassesToCreate">
        /// The number of classes to be created.
        /// </param>
        /// <returns>
        /// A list of <see cref="CreatedTypeInfo"/> objects describing the created types and the interface types which they implement.
        /// </returns>
        public IList<CreatedTypeInfo> CreateDynamicClasses(int numberOfClassesToCreate)
        {
            // Use the current namespace as the namespace for the created classes.
            var @namespace = typeof(DynamicClassesCreator).Namespace;

            var codeNamespace = new CodeNamespace(@namespace);
            codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.ComponentModel"));

            // Create the interface and class definitions.
            IList<CreatedTypeName> createdTypeNames = new List<CreatedTypeName>(numberOfClassesToCreate);

            var typeCreator = new TypeCreator();

            for (var i = 1; i <= numberOfClassesToCreate; i++)
            {
                // Create an interface.
                var interfaceDeclaration = typeCreator.CreateInterface(i);

                // Create a class which implements this interface.
                var classDeclaration = typeCreator.CreateClass(i, interfaceDeclaration);

                // Record the name of our class and the interface it implements, so that we can retrieve them later.
                createdTypeNames.Add(new CreatedTypeName(
                    interfaceDeclaration.Name,
                    classDeclaration.Name));

                // Add these types to the code namespace.
                codeNamespace.Types.Add(interfaceDeclaration);
                codeNamespace.Types.Add(classDeclaration);
            }

            // Compile these into a dynamic assembly.
            var assembly = CompileAssembly(codeNamespace);

            // Pull out the created types from the assembly.
            IList<CreatedTypeInfo> createdTypes = ExtractCreatedTypes(assembly, createdTypeNames);

            return createdTypes;
        }

        #endregion // #region Public methods

        #region Private methods

        private static Assembly CompileAssembly(CodeNamespace codeNamespace)
        {
            var codeCompileUnit = new CodeCompileUnit();
            codeCompileUnit.Namespaces.Add(codeNamespace);

            var compilerParameters = new CompilerParameters
            {
                GenerateInMemory = true,
                IncludeDebugInformation = true,
                TreatWarningsAsErrors = true,
                WarningLevel = 4,
            };

            compilerParameters.ReferencedAssemblies.Add("System.dll");

            var codeProvider = new CSharpCodeProvider();
            var compilerResult = codeProvider.CompileAssemblyFromDom(compilerParameters, codeCompileUnit);

            if (compilerResult is null)
            {
                throw new InvalidOperationException("Error: No results returned from compiler.");
            }
            else if (compilerResult.Errors.HasErrors)
            {
                StringBuilder errors = new StringBuilder();
                foreach (CompilerError compilerError in compilerResult.Errors)
                {
                    errors.AppendLine(compilerError.ErrorText);
                }

                throw new InvalidOperationException("The following errors occurred during compilation:\n" + errors.ToString());
            }

            var assembly = compilerResult.CompiledAssembly;
            return assembly;
        }

        private static IList<CreatedTypeInfo> ExtractCreatedTypes(
            Assembly assembly,
            IList<CreatedTypeName> createdTypeNames)
        {
            // Get the public types from the assembly, as a lookup table by type name.
            var createdTypesLookupByTypeName = assembly
                .GetExportedTypes()
                .ToDictionary(ty => ty.Name);

            // Extract these types into a list of classes, with the interfaces they implement.
            IList<CreatedTypeInfo> result = new List<CreatedTypeInfo>(createdTypeNames.Count);
            foreach (var createdTypeName in createdTypeNames)
            {
                var interfaceName = createdTypeName.InterfaceName;
                var className = createdTypeName.ClassName;

                var @interface = createdTypesLookupByTypeName[interfaceName];
                var @class = createdTypesLookupByTypeName[className];

                result.Add(new CreatedTypeInfo(@interface, @class));
            }

            return result;
        }

        #endregion // #region Private methods

        #region Private class CreatedTypeName

        private sealed class CreatedTypeName
        {
            public CreatedTypeName(
                string interfaceName,
                string className)
            {
                // Validate arguments.
                if (string.IsNullOrWhiteSpace(interfaceName)) throw new ArgumentNullException(nameof(interfaceName));
                if (string.IsNullOrWhiteSpace(className)) throw new ArgumentNullException(nameof(className));

                // Make these arguments available to the object.
                InterfaceName = interfaceName;
                ClassName = className;
            }

            /// <summary>
            /// Gets the name of the interface which the class implements.
            /// </summary>
            public string InterfaceName { get; }

            /// <summary>
            /// Gets the name of the class.
            /// </summary>
            public string ClassName { get; }

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
        }

        #endregion // #region Private class CreatedTypeName
    }
}
