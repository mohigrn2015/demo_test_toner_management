using ASP.NETCoreIdentityCustom.Models;

namespace ASP.NETCoreIdentityCustom.ViewModel
{
    public class TonarViewModel
    {
        public int? TonarId { get; set; }
        public int MachinID { get; set; } = 0;
        public int[] TonarIDs { get; set; } = Array.Empty<int>();
        public string TonarModel { get; set; } = String.Empty;
        public string errmsg { get; set; } = String.Empty;
        public TonerConfig tonerConfig { get; set; }
        public IList<Toner> Toners { get; set; }
        public IList<Machine> Machines { get; set; }

    }
}
