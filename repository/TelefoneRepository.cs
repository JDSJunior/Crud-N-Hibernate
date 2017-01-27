using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;

namespace Repository
{
    public class TelefoneRepository : RepositoryCrudDao<Telefone>
    {
        public bool validarTelefone(int telefone)
        {
            using (ISession sesseion = FluentySessionFactory.AbrirSession())
            {
                return (from e in sesseion.Query<Telefone>() where e.telefone.Equals(telefone) select e).Count() > 0;
            }
        }

        public List<Telefone> retornarPorContato(int contato)
        {
            using (ISession session = FluentySessionFactory.AbrirSession())
            {
                return (from e in session.Query<Telefone>() where e.id_contato == contato select e).ToList();
            }
        }
    }
}
