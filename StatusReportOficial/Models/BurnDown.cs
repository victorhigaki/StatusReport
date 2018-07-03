using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatusReportOficial.Models
{
    public class BurnDown
    {
        public int id { get; set; }
        public int dia_sprint { get; set; }

        public Sprints FK_Id_Sprint{ get; set; }

        public int qtd_reazalidas { get; set; }

    }
}