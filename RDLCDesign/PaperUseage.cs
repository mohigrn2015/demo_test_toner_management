namespace RDLCDesign
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PaperUseage")]
    public partial class PaperUseage
    {
        public int PaperUseageID { get; set; }

        public int MachineId { get; set; }

        public int CurrentUses { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateCreated { get; set; }

        public Guid? CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateModified { get; set; }

        public Guid? ModifiedBy { get; set; }

        public bool? IsRowDeleted { get; set; }
    }
}
