namespace Lix.Commons.Tests
{
    public class FluentAnd<T>
    {
        public T And
        {
            get;
            private set;
        }

        public FluentAnd(T target)
        {
            this.And = target;
        }
    }
}