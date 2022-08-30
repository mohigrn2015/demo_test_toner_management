using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace RDLCDesign
{
    public partial class Model : DbContext
    {
        public Model()
            : base("name=ASPNETKeyoceraDb1")
        {
        }

        public virtual DbSet<PaperUseage> PaperUseages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
