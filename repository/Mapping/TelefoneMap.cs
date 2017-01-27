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
    public class TelefoneMap : ClassMap<Telefone>
    // classe Telefone sendo derivada da classe ClassMap passando tipo Classe Telefone
    {
        public TelefoneMap()
        // metodo contrutor que mapeia a classe tipo passada
        {
            Id(c => c.id);
            //propriedade que mapea o id da classe
            Map(c => c.id_contato);
            Map(c => c.site);
            Map(c => c.telefone);
            Map(c => c.email);
            Map(c => c.tipo);
            //tabela do BD que será mapeada
            Table("telefone");
        }
    }
}
