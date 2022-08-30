using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreIdentityCustom.Models
{
    public class BlackToner:BaseModel
    {
        [Key]
        public int BlackTonerID { get; set; }
        public int MachineID { get; set; }
        public int MachineSN { get; set; }
        public string MachineModel { get; set; }
        public string TonarModel { get; set; }
        public long PreviousUsages { get; set; }
        public long CurrentUsages { get; set; }
        public long TotalUsages { get; set; }
        public long TonerUsages { get; set; }
    }
}
