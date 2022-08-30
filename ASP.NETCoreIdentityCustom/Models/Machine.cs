using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NETCoreIdentityCustom.Models
{
    public class Machine:BaseModel
    {
        [Key]
        public int MachineId { get; set; }

        [Display(Name = "Machine Model")]
        public string MachineModel { get; set; }

        [Display(Name = "Machine Serial No")]
        public string MachineSN { get; set; }
        public bool colorType { get; set; }
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public  Project Project { get; set; }
        public IList<TonerConfig> TonerConfigs { get; set; }
        public IList<PaperUseage> PaperUseages { get; set; }

    }
}
