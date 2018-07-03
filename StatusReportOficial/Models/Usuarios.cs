using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatusReportOficial.Models
{
    public class Usuarios
    {
        public int Id { get; set; }

       
        public String Nome { get; set; }

      
        public String Sobrenome { get; set; }

        public String Login { get; set; }
        public String Senha { get; set; }


        public String Email { get; set; }

        public bool Adm { get; set; }

        public bool ativo { get; set; }

        public virtual ICollection<Projetos> Projetos { get; set; }

        public Usuarios()
        {
            Projetos = new HashSet<Projetos>();
        }


    }
}