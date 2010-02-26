namespace Lix.Commons.Specifications
{
    /// <summary>
    /// Represents a specification.
    /// </summary>
    public interface ISpecification
    {
        /// <summary>
        /// Sets the context to use for the specification.
        /// </summary>
        /// <param name="context">The context to use.</param>
        void SetContext(object context);

        object Build();
    }
}