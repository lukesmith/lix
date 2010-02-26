namespace Lix.Commons.Specifications
{
    /// <summary>
    /// Represents a specification.
    /// </summary>
    /// <typeparam name="TContext">The context.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface ISpecification<TContext, TResult> : ISpecification
    {
        /// <summary>
        /// Sets the context to use for the specification.
        /// </summary>
        /// <param name="context">The context to use.</param>
        void SetContext(TContext context);

        new TResult Build();
    }
}
