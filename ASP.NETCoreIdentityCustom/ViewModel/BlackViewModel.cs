using ASP.NETCoreIdentityCustom.Models;
using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreIdentityCustom.ViewModel
{
    public class BlackViewModel
    {
        [Key]
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ProjectName { get; set; }
        public string TonarModel { get; set; }
        public string MachineModel { get; set; }
        public string MachineSN { get; set; }
        public int ProjectId { get; set; }
        public int[] MachineId { get; set; }
        public int TonarId { get; set; }
        public int PreviousUses { get; set; }
        public int[] CurrentUses { get; set; }
        public int Totaluses { get; set; }
        public string TonerUses { get; set; }
        public DateTime Date { get; set; }
        public int TotalCounter { get; set; }
        public decimal TotalPercentage { get; set; }
        public int CurrentStock { get; set; }
        public decimal CurrentPercentage { get; set; }
        public decimal TotalToner { get; set; }


        public string ProjectsName { get; set; }
        public string TonarsModel { get; set; }
        public string MachinesModel { get; set; }
        public string MachinesSN { get; set; }
        public int ProjectsId { get; set; }
        public int[] MachineIDs { get; set; }
        public int TonarsId { get; set; }
        public int PreviousesUses { get; set; }
        public int[] CurrentsUses { get; set; }
        public int Totalsuses { get; set; }
        public string[] TonersUses { get; set; }
        public int TotalsCounter { get; set; }
        public decimal TotalsPercentage { get; set; }

        public int CurrentsStock { get; set; }
        public decimal CurrentsPercentage { get; set; }
        public decimal TotalsToner { get; set; }
        public DateTime Dates { get; set; }
        public IList<Customer> Customers { get; set; }
        public IList<Project> Projects { get; set; }


        public List<BlackColorViewModel> blackColorViewModels { get; set; }
    }
}
