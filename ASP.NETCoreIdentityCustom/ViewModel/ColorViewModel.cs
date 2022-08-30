using ASP.NETCoreIdentityCustom.Models;

namespace ASP.NETCoreIdentityCustom.ViewModel
{
    public class ColorViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ProjectName { get; set; }
        public string TonarModel { get; set; }
        public string MachineSN { get; set; }
        public string MachineModel { get; set; }
        public int ProjectId { get; set; }
        public int MachineId { get; set; }
        public int[] MachineIDs { get; set; }
        public int TonarId { get; set; }
        public int PreviousUses { get; set; }
        public int[] CurrentUses { get; set; }
        public int Totaluses { get; set; }
        public string TonerUses { get; set; }
        public int TotalCounter { get; set; }
        public decimal TotalPercentage { get; set; }
        public int CurrentStock { get; set; }
        public decimal CurrentPercentage { get; set; }
        public decimal TotalToner { get; set; }
        public DateTime Date { get; set; }
        public IList<Customer> Customers { get; set; }
        public IList<Project> Projects { get; set; }

    }
    public class Combine
    {
        public List<ColorViewModel> comp { get; set; }
    }
}