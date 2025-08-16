using System;
using System.CodeDom;
using System.Reflection;

namespace DependencyInjectionContainerBenchmarker.Application
{
    /// <summary>
    /// Helper class for creating interfaces and classes dynamically.
    /// </summary>
    internal sealed class TypeCreator
    {
        #region Public methods

        /// <summary>
        /// Create the <see cref="CodeTypeDeclaration"/> for an interface with the supplied interface name suffix.
        /// </summary>
        /// <param name="nameSuffix">
        /// A numeric suffix to attach to the name of the created interface.
        /// </param>
        /// <returns>
        /// The <see cref="CodeTypeDeclaration"/> representing the interface to be created.
        /// </returns>
        public CodeTypeDeclaration CreateInterface(int nameSuffix)
        {
            var interfaceDeclaration = CreateInterfaceDeclaration(nameSuffix);

            AddValueProperty(interfaceDeclaration);

            return interfaceDeclaration;
        }

        /// <summary>
        /// Create the <see cref="CodeTypeDeclaration"/> for a class with the supplied class name suffix and implemented interface declaration.
        /// </summary>
        /// <param name="nameSuffix">
        /// A numeric suffix to attach to the name of the created class.
        /// </param>
        /// <param name="interfaceDeclaration">
        /// The <see cref="CodeTypeDeclaration"/> for the interface which this class will implement. This must not be <c>null</c>.
        /// </param>
        /// <returns>
        /// The <see cref="CodeTypeDeclaration"/> representing the class to be created.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="interfaceDeclaration"/> argument is <c>null</c>.
        /// </exception>
        public CodeTypeDeclaration CreateClass(int nameSuffix, CodeTypeDeclaration interfaceDeclaration)
        {
            // Validate arguments.
            if (interfaceDeclaration is null) throw new ArgumentNullException(nameof(interfaceDeclaration));

            var classDeclaration = CreateClassDeclaration(nameSuffix);

            var interfaceName = interfaceDeclaration.Name;

            classDeclaration.BaseTypes.Add(
                new CodeTypeReference(interfaceName));

            AddDefaultConstructor(classDeclaration);

            AddValueProperty(classDeclaration, nameSuffix);

            return classDeclaration;
        }

        #endregion // #region Public methods

        #region Private methods

        private static CodeTypeDeclaration CreateInterfaceDeclaration(int nameSuffix)
        {
            var interfaceDeclaration = new CodeTypeDeclaration("MyInterface_" + ZeroPad(nameSuffix, 5))
            {
                TypeAttributes = TypeAttributes.Public | TypeAttributes.Interface,
            };

            return interfaceDeclaration;
        }

        private static CodeTypeDeclaration CreateClassDeclaration(int nameSuffix)
        {
            var classDeclaration = new CodeTypeDeclaration("MyClass_" + ZeroPad(nameSuffix, 5))
            {
                TypeAttributes = TypeAttributes.Public,
            };

            return classDeclaration;
        }

        private static void AddDefaultConstructor(CodeTypeDeclaration classDeclaration)
        {
            var constructor = new CodeConstructor
            {
                Attributes = MemberAttributes.Public,
            };

            classDeclaration.Members.Add(constructor);
        }

        private static void AddValueProperty(CodeTypeDeclaration interfaceDeclaration)
        {
            var property = new CodeMemberProperty
            {
                Name = "Value",
                Type = new CodeTypeReference(typeof(int)),
            };
            property.HasGet = true;

            interfaceDeclaration.Members.Add(property);
        }

        private static void AddValueProperty(CodeTypeDeclaration classDeclaration, int valueToReturn)
        {
            var property = new CodeMemberProperty
            {
                Name = "Value",
                Type = new CodeTypeReference(typeof(int)),
                Attributes = MemberAttributes.Public,
            };
            property.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodePrimitiveExpression(valueToReturn)));

            classDeclaration.Members.Add(property);
        }

        private static string ZeroPad(int n, int length)
        {
            return n.ToString().PadLeft(length, '0');
        }

        #endregion // #region Private methods
    }
}
