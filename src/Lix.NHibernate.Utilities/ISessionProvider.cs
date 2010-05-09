using System;
using NHibernate;

namespace Lix.Commons
{
    /// <summary>
    /// Provides an interface for getting an NHibernate <see cref="ISession"/>.
    /// </summary>
    public interface ISessionProvider : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="ISession"/>.
        /// </summary>
        /// <returns>
        /// Returns the <see cref="ISession"/>.
        /// </returns>
        ISession GetSession();
    }
}