using NHibernate;

namespace Lix.Commons.Specifications.NHibernate
{
    /// <summary>
    /// Represents an NHibernate specification.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface INHibernateSpecification<TResult> : ISpecification<ISession, TResult>
    {
    }
}