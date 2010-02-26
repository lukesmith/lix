using System;

namespace Lix.Commons.Specifications
{
    public abstract class AbstractSpecification<TContext, TResult> : ISpecification<TContext, TResult>
        where TContext : class
    {
        protected TContext Context
        {
            get;
            set;
        }

        void ISpecification.SetContext(object context)
        {
            if (context is TContext)
            {
                ((ISpecification<TContext, TResult>) this).SetContext(context as TContext);
            }
            else
            {
                throw new ArgumentException("Cannot convert context to type of TContext.");
            }
        }

        TResult ISpecification<TContext, TResult>.Build()
        {
            if (this.Context == null)
            {
                throw new InvalidOperationException("Attempting to build the specification when no context has been set.");
            }

            return this.Build(this.Context);
        }

        void ISpecification<TContext, TResult>.SetContext(TContext context)
        {
            this.Context = context;
        }

        object ISpecification.Build()
        {
            return ((ISpecification<TContext, TResult>)this).Build();
        }

        protected abstract TResult Build(TContext context);

        protected TResult Execute()
        {
            return ((ISpecification<TContext, TResult>)this).Build();
        }
    }
}