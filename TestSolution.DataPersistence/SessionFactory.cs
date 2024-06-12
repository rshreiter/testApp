using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace TestSolution.DataPersistence
{
    public class SessionFactory
    {
        private static ISessionFactory? _sessionFactory;

        public static ISession OpenSession()
        {
            return _sessionFactory?.OpenSession() ?? throw new InvalidOperationException();
        }

        public static void Init(string connectionString)
        {
            _sessionFactory = BuildSessionFactory(connectionString);
        }

        private static ISessionFactory? BuildSessionFactory(string connectionString)
        {
            var sFact = Fluently
                .Configure()
                .Database(PostgreSQLConfiguration.PostgreSQL81
                    .ConnectionString(c => c.Is(connectionString))
                    .ShowSql())
                .Mappings(m =>
                {
                    foreach (var classType in GetClassesFromNamespace())
                    {
                        m.FluentMappings.Add(classType);
                    }
                })
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                .BuildSessionFactory();

            return sFact;
        }

        private static List<Type> GetClassesFromNamespace()
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            var tps = ass.GetTypes();
            var types = ass.GetTypes()
                .Where(type => type.Namespace == "TestSolution.Models.Mappings" && type.FullName.EndsWith("Map"))
                .Select(type => type).ToList();

            return types;
        }
    }
}