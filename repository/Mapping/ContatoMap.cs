using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//namespace da classe ClasseMap
using FluentNHibernate.Mapping;
//namespace das classe de mapeamento
using Repository.Entities;

namespace Repository.Mapping
{
    public class ContatoMap : ClassMap<Contato>
    //classe de mapeamento derivando de ClassMap passando o tipo da classe para ser mapeada
    {
        public ContatoMap()
        //metodo contrutor de mapeamento
        {
            Id(c => c.id);
            //propriedade que mapea o ID
            Map(c => c.nome);
            Map(c => c.endereco);
            Map(c => c.bairro);
            Map(c => c.cidade);
            Map(c => c.numero);
            Map(c => c.cep);
            Map(c => c.estado);
            Map(c => c.status);
            //tabela do bd a ser mapeada
            Table("contato");
        }
    }
}
