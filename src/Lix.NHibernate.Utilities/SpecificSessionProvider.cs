using NHibernate;

namespace Lix.Commons
{
    /// <summary>
    /// Represents an implementation of an <see cref="ISessionProvider"/>.
    /// </summary>
    public class SpecificSessionProvider : ISessionProvider
    {
        private readonly ISession session;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecificSessionProvider"/> class.
        /// </summary>
        /// <param name="session">
        /// The session.
        /// </param>
        public SpecificSessionProvider(ISession session)
        {
            this.session = session;
        }

        /// <summary>
        /// Gets the <see cref="ISession"/>.
        /// </summary>
        /// <returns>
        /// Returns the <see cref="ISession"/>.
        /// </returns>
        public ISession GetSession()
        {
            return this.session;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
        }
    }
}