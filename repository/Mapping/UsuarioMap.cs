using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//namespace da classe ClassMap para derivar
using FluentNHibernate.Mapping;
//namespace das classes para mapeamento
using Repository.Entities;


namespace Repository.Mapping
{
    public class UsuarioMap : ClassMap<Usuario>
    //classe de mapeamento do nhibernate derivando de classmap
    //recebendo o tipo da classe mapeada
    {
        //metodo de mapeamento
        public UsuarioMap()
        {
            Id(c => c.id);
            //propriedade que identifica o id

            Map(c => c.login);
            Map(c => c.nome);
            Map(c => c.senha);
            Map(c => c.status);
            //campos mapeados da classe Usuario

            Table("usuarios");
            //tabela do BD mapeada
        }
    }
}
