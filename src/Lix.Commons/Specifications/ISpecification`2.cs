namespace Lix.Commons.Specifications
{
    /// <summary>
    /// Represents a specification.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface ISpecification<TContext, TResult> : ISpecification
    {
        /// <summary>
        /// Builds the specification for the <typeparamref name="TContext"/>.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// An object representing the built specification.
        /// </returns>
        TResult Build(TContext context);
    }
}
