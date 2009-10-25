using NHibernate;

namespace Lix.Commons.Specifications
{
    public abstract class DefaultNHibernateQuerySpecification : INHibernateQuerySpecification
    {
        public object Build(object context)
        {
            return this.Build(context as IQuery);
        }

        /// <summary>
        /// Builds the specification for the <see cref="ISession"/>.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// An object representing the built specification.
        /// </returns>
        public IQuery Build(ISession context)
        {
            var query = context.CreateQuery(this.Query());

            return this.Build(query);
        }

        /// <summary>
        /// Builds the specification for the <see name="ISession"/>.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// An object representing the built specification.
        /// </returns>
        public IQuery BuildCount(ISession context)
        {
            var query = context.CreateQuery(this.CountQuery());

            return this.Build(query);
        }

        /// <summary>
        /// Defines the hql query.
        /// </summary>
        /// <returns>
        /// A string representing the NHibernate HQL query.
        /// </returns>
        protected abstract string Query();

        /// <summary>
        /// Builds the hql query.
        /// </summary>
        /// <param name="query">The <see cref="IQuery"/>.</param>
        /// <returns>
        /// The <see cref="IQuery"/> of the specification.
        /// </returns>
        protected abstract IQuery Build(IQuery query);

        public virtual string CountQuery()
        {
            return string.Format("SELECT count(*) {0}", this.Query());
        }
    }
}