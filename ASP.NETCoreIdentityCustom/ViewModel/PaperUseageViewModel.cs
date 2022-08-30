using ASP.NETCoreIdentityCustom.Models;

namespace ASP.NETCoreIdentityCustom.ViewModel
{
    public class PaperUseageViewModel
    {
        public int MachineId { get; set; }
        public string MachineModel { get; set; }
        public string MachineSN { get; set; }
        public bool colorType { get; set; }
        public int ProjectId { get; set; }
        public int CurrentUses { get; set; }
        public int TotalCounter { get; set; }
        public decimal TotalPercentage { get; set; }
        public int CurrentStock { get; set; }
        public decimal CurrentPercentage { get; set; }
        public int TotalToner { get; set; }
        public DateTime? Date { get; set; }
        public IList<TonerConfig> TonerConfigs { get; set; }
    }
}
