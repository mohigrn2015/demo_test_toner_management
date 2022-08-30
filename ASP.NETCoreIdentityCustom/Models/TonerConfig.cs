using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NETCoreIdentityCustom.Models
{
    public class TonerConfig : BaseModel
    
    {
        [Key]
        public int TConfligID { get; set; }
        public int MachinID { get; set; }
        public int TonarID { get; set; }
        [ForeignKey("MachinID")]
        public virtual Machine Machine { get; set; }
        [ForeignKey("TonarID")]
        public virtual Toner Toner { get; set; }
    }
}
