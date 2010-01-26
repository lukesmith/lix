using System;
using System.Linq;
using Lix.Commons.Specifications;
using Lix.Commons.Specifications.Executors;

namespace Lix.Commons
{
    /// <summary>
    /// Represents an instance of a <see cref="LixObjectFactory"/>.
    /// </summary>
    public static class LixObjectFactory
    {
        private static void SetUpContainer()
        {
            Container = new Container();
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
        /// Resets and initializes the <see cref="LixObjectFactory"/> with default configuration settings.
        /// </summary>
        public static void Initialize()
        {
            Initialize(x => { });
        }

        /// <summary>
        /// Resets and initializes the <see cref="LixObjectFactory"/> with the default and configuration settings registered by the <see cref="IInitializeExpression"/>.
        /// </summary>
        /// <param name="action">The <see cref="IInitializeExpression"/> to use to configure the <see cref="LixObjectFactory"/> with.</param>
        public static void Initialize(Action<IInitializeExpression> action)
        {
            Reset();
            var expression = new InitializeExpression(Container);
            expression.UseDefaults();
            action(expression);
        }

        /// <summary>
        /// Resets the <see cref="LixObjectFactory"/> to the default state.
        /// </summary>
        public static void Reset()
        {
            if (Container != null)
            {
                Container.Dispose();
                Container = null;
            }

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
    }
}