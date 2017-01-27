using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using Npgsql;

namespace Repository
{
    public class FluentySessionFactory
    {
        //string de conexao com o banco de dados
        private static string ConnectionString = "Server=localhost; Port=5432; User Id=postgres; Password=123123; Database=pessoas";
        //variavel de conexao
        private static ISessionFactory session;

        public static ISessionFactory CriarSession()
        {
            if (session != null)
                return session;

            IPersistenceConfigurer configDB = PostgreSQLConfiguration.PostgreSQL82.ConnectionString(ConnectionString);
            //interface de configuracao do postgre
            var configMap = Fluently.Configure().Database(configDB).Mappings(c => c.FluentMappings.AddFromAssemblyOf<Mapping.UsuarioMap>());
            //variavel onde passamos a configuracao do postgre e passamos o assembly das classes
            session = configMap.BuildSessionFactory();
            //sessao configurada
            return session;
        }

        public static ISession AbrirSession()
        {
            return CriarSession().OpenSession();
        }
    }
}

