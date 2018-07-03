using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StatusReportOficial.Models
{
    public class Projetos
    {
        public int Id { get; set; }

        [Required]
        public string nomeProjeto { get; set; }

        [Required]
        public string cliente { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string dataInicio { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string dataFim { get; set; }

        public virtual ICollection<Usuarios> Usuarios { get; set; }

        public virtual ICollection<Sprints> Sprints { get; set; }

        public Projetos()
        {
            Usuarios = new HashSet<Usuarios>();

            Sprints = new HashSet<Sprints>();

        }

    }
}