using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repository
{
    public class RepositoryCrudDao<T> : ICrudDao<T> where T : class
    // Repository herda de ICrudDao passando como parametro<T> Desde que T seja uma classe
    {

        public void Inserir(T entidade)
        {
            using (ISession session = FluentySessionFactory.AbrirSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(entidade);
                        transacao.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (!transacao.WasCommitted)
                        {
                            throw new Exception("Erro ao inserir" + ex.Message);
                        }
                    }
                }
            }
        }

        public void Alterar(T entidade)
        {
            using (ISession session = FluentySessionFactory.AbrirSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(entidade);
                        transacao.Commit();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao atualizar" + ex.Message);
                    }
                }
            }
        }

        public void Excluir(T entidade)
        {
            using (ISession session = FluentySessionFactory.AbrirSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(entidade);
                        transacao.Commit();
                    }
                    catch(Exception ex)
                    {
                        throw new Exception("Erro ao deletar"+ ex.Message);
                    }
                }
            }
        }

        public T RetornarPorId(int id)
        {
            using (ISession session = FluentySessionFactory.AbrirSession())
            {
                return session.Get<T>(id);
            }
        }

        public IList<T> Consultar()
        {
            using (ISession session = FluentySessionFactory.AbrirSession())
            {
                return (from e in session.Query<T>() select e).ToList();
            }
        }
    }
}
