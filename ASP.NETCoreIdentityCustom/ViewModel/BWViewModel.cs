namespace ASP.NETCoreIdentityCustom.ViewModel
{
    public class BWViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ProjectName { get; set; }
        public string TonarModel { get; set; }
        public string MachineModel { get; set; }
        public int ProjectId { get; set; }
        public int[] MachineId { get; set; }
        public int TonarId { get; set; }
        public int PreviousCounter { get; set; }
        public int[] CurrentCounter { get; set; }
        //public int TotalCounter { get; set; }
        public int MonthlyTotalCounter { get; set; }
        public double TonarPercentage { get; set; }
        public int TotalCounter { get; set; }
        public decimal TotalPercentage { get; set; }
        public int CurrentStock { get; set; }
        public decimal CurrentPercentage { get; set; }
        public decimal TotalToner { get; set; }
        public IList<TonarViewModel> tonarViewModels { get; set; }

    }

    public class BWAViewModel
    {
        public List<BlackColorViewModel> bwViewModels { get; set; }
    }
}
