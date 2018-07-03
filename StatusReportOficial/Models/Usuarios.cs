using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StatusReportOficial.Models
{
    public class Usuarios
    {
        public int Id { get; set; }

        [Required]
        public String Nome { get; set; }

        [Required]
        public String Sobrenome { get; set; }

        [Required]
        public String Login { get; set; }

        [Required]
        public String Senha { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        public bool Adm { get; set; }

        [Required]
        public bool Ativo { get; set; }

        public virtual ICollection<Projetos> Projetos { get; set; }

        public Usuarios()
        {
            Projetos = new HashSet<Projetos>();
        }


    }
}