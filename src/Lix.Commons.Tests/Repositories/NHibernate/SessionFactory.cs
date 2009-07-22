using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Lix.Commons.Tests.Repositories.NHibernate
{
    public class SessionFactory
    {
        public static ISessionFactory CreateSessionFactory()
        {
            var type = typeof(global::NHibernate.ByteCode.Castle.ProxyFactory);

            return Fluently
                .Configure()
                .Database(SQLiteConfiguration.Standard.InMemory().ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<SessionFactory>())
                .ExposeConfiguration(c => SavedConfig = c)
                .BuildSessionFactory();
        }

        private static Configuration SavedConfig;

        public static void BuildSchema(ISession session)
        {
            var export = new SchemaExport(SavedConfig);
            export.Execute(true, true, false, session.Connection, null);
        }
    }
}