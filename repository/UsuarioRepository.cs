using NHibernate;
using NHibernate.Linq;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UsuarioRepository : RepositoryCrudDao<Usuario>
    {
        public bool validarLogin(string login)
        {
            using (ISession session = FluentySessionFactory.AbrirSession())
            {
                return (from e in session.Query<Usuario>() where e.login.Equals(login) select e).Count() > 0;
            }
        }
    }
}
