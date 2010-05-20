using NHibernate;

namespace Lix.Commons.Specifications
{
    /// <summary>
    /// Represents an NHibernate <see cref="IQuery"/> specification.
    /// </summary>
    public interface INHibernateQuerySpecification : INHibernateSpecification<IQuery>
    {
        /// <summary>
        /// Builds the specification for the <see cref="ISession"/>.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// An object representing the built specification.
        /// </returns>
        IQuery BuildCount(ISession context);
    }
}