using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreIdentityCustom.Models
{
    public class TonerStock:BaseModel
    {
        [Key]
        public int TonerStockID { get; set; }
        public string TonerModel { get; set; }
        public int CurrentStock { get; set; }
        public int TotalToner { get; set; }
        public decimal CurrentPercentage { get; set; }
    }
}
