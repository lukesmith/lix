namespace Lix.Commons.Specifications
{
    /// <summary>
    /// Represents a specification.
    /// </summary>
    public interface ISpecification
    {
        /// <summary>
        /// Builds the specification for the <paramref name="context"/>.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// An object representing the built specification.
        /// </returns>
        object Build(object context);
    }
}