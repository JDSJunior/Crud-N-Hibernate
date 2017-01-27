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
    public class ContatoRepository : RepositoryCrudDao<Contato>
    {
        public bool validarContato(string nome)
        {
            using (ISession session = FluentySessionFactory.AbrirSession())
            {
                return (from e in session.Query<Contato>() where e.nome.Equals(nome) && e.status.Equals("A") select e).Count() > 0;
            }
        }
    }
}
