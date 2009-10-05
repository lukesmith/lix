using System;
using System.Linq;
using Lix.Commons.Specifications;

namespace Lix.Commons
{
    /// <summary>
    /// Represents an instance of a <see cref="LixObjectFactory"/>.
    /// </summary>
    public static class LixObjectFactory
    {
        static LixObjectFactory()
        {
            SetUpContainer();
        }

        private static void SetUpContainer()
        {
            Container = new Container();
            var expression = new InitializeExpression(Container);
            expression.UseDefaults();
        }

        /// <summary>
        /// The IoC container instance the <see cref="LixObjectFactory"/> uses.
        /// </summary>
        public static Container Container
        {
            get;
            private set;
        }

        /// <summary>
        /// Resets and initializes the <see cref="LixObjectFactory"/> with the <see cref="IInitializeExpression"/> configuration settings.
        /// </summary>
        /// <param name="action">The <see cref="IInitializeExpression"/> to use to configure the <see cref="LixObjectFactory"/> with.</param>
        public static void Initialize(Action<IInitializeExpression> action)
        {
            Reset();
            var expression = new InitializeExpression(Container);
            action(expression);
        }

        /// <summary>
        /// Resets the <see cref="LixObjectFactory"/> to the default state.
        /// </summary>
        public static void Reset()
        {
            Container.Dispose();
            Container = null;
            SetUpContainer();
        }

        /// <summary>
        /// Creates an instance of generic type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The generic type of the object to create an instance of.</typeparam>
        /// <returns>
        /// A new instance of the generic type <typeparamref name="T"/>.
        /// </returns>
        public static T CreateInstance<T>()
        {
            var instance = CreateInstance(typeof(T));

            return (T) instance;
        }

        /// <summary>
        /// Creates an instance of the <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type of the object to create an instance of.</param>
        /// <returns>
        /// A new instance of the <paramref name="type"/>.
        /// </returns>
        public static object CreateInstance(Type type)
        {
            return CreateInstance(type, null);
        }

        /// <summary>
        /// Creates an instance of the <paramref name="type"/> with the specified <paramref name="args"/>.
        /// </summary>
        /// <param name="type">The type of the object to create an instance of.</param>
        /// <param name="args">The constructor arguments for the instance to create.</param>
        /// <returns>
        /// A new instance of the <paramref name="type"/>.
        /// </returns>
        public static object CreateInstance(Type type, params object[] args)
        {
            var implementingType = Container.FindTypeFor(type);

            if (implementingType != null)
            {
                return Activator.CreateInstance(implementingType, args);
            }

            return null;
        }

        /// <summary>
        /// Creates a new <see cref="ISpecificationInterceptor"/> from the <see cref="Container"/>.
        /// </summary>
        /// <returns>
        /// An object implementing <see cref="ISpecificationInterceptor"/>.
        /// </returns>
        public static ISpecificationInterceptor CreateSpecificationInterceptor()
        {
            var type = Container.FindTypeFor(x => x.GetInterfaces().Contains(typeof (ISpecificationInterceptor)));

            return CreateInstance(type) as ISpecificationInterceptor;
        }

        /// <summary>
        /// Checks whether the two types match, including whether they are the same generic type.
        /// </summary>
        /// <param name="typeA">The first type.</param>
        /// <param name="typeB">The second type.</param>
        /// <returns>
        /// true if the types match; otherwise false.
        /// </returns>
        internal static bool DoTypesMatch(Type typeA, Type typeB)
        {
            if (typeA == typeB)
            {
                return true;
            }

            if (typeA.IsGenericType && typeB.IsGenericType && typeB.GetGenericTypeDefinition() == typeA.GetGenericTypeDefinition())
            {
                return true;
            }

            return false;
        }
    }
}