using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace AegisBorn
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();
                
                return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            _sessionFactory = Fluently.Configure()
              .Database(
                MySQLConfiguration.Standard
                .ConnectionString(cs => cs.Server("localhost")
                .Database("cjrgam5_ab")
                .Username("cjrgam5_ab")
                .Password("user_ab1!")))
              .Mappings(m =>
                m.FluentMappings.AddFromAssemblyOf<AegisBornApplication>())
              .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
