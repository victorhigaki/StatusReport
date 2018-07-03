using StatusReportOficial.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StatusReportOficial.Context
{
    public class StatusReportOficialContext : DbContext
    {
        public StatusReportOficialContext()
            : base("StatusReportBDOficial")
        {

        }

        public DbSet<Usuarios> Usuarios{ get; set; }

        public DbSet<Projetos> Projetos { get; set; }

        public DbSet<Sprints> Sprints { get; set; }

        public DbSet<BurnDown> BurnDowns { get; set; }
    }
}