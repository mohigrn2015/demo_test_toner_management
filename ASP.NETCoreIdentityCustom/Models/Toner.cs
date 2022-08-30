using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NETCoreIdentityCustom.Models
{
    public class Toner:BaseModel
    {
        [Key]
        public int TonarId { get; set; }
        public string TonarModel { get; set;}
        public string Description { get; set;} = String.Empty;
        public bool Type { get; set; }
        public IList<TonerConfig> TonerConfigs { get; set; }
    }
}
