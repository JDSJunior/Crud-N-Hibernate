﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Telefone
    {
        public virtual int id { get; set; }
        public virtual int id_contato { get; set; }
        public virtual string tipo { get; set; }
        public virtual string telefone { get; set; }
        public virtual string email { get; set; }
        public virtual string site { get; set; }
    }
}
