using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StatusReportOficial.Models
{
    public class Sprints
    {
        public int Id { get; set; }

        [Required]
        public string nome { get; set; }

        [Required]
        public int qtd_tarefas { get; set; }

        [Required]
        public int dias { get; set; }

        [Required]
        public string data_inicio { get; set; }

        [Required]
        public string data_fim { get; set; }

        public int Fk_Projeto_Id { get; set; }

        [ForeignKey("Fk_Projeto_Id")]
        public virtual Projetos projeto { get; set; }

        public virtual ICollection<BurnDown> BurnDown { get; set; }

        public Sprints()
        {
            BurnDown = new HashSet<BurnDown>();
        }


    }
}