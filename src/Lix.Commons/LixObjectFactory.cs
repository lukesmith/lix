using System;
using System.Linq;
using Lix.Commons.Specifications;

namespace Lix.Commons
{
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

        public static Container Container
        {
            get;
            private set;
        }

        public static void Initialize(Action<IInitializeExpression> action)
        {
            Reset();
            var expression = new InitializeExpression(Container);
            action(expression);
        }

        public static void Reset()
        {
            Container.Dispose();
            Container = null;
            SetUpContainer();
        }

        public static T CreateInstance<T>()
        {
            var instance = CreateInstance(typeof(T));

            return (T) instance;
        }

        public static object CreateInstance(Type type)
        {
            var implementingType = Container.FindTypeFor(type);
            
            if (implementingType != null)
            {
                return Activator.CreateInstance(implementingType);
            }

            return null;
        }

        public static ISpecificationInterceptor CreateSpecificationInterceptor()
        {
            var type = Container.FindTypeFor(x => x.GetInterfaces().Contains(typeof (ISpecificationInterceptor)));

            return CreateInstance(type) as ISpecificationInterceptor;
        }
    }
}