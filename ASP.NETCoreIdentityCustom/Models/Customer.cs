using Microsoft.Build.Evaluation;
using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreIdentityCustom.Models
{
    public class Customer:BaseModel
    {

        [Key]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
        public IList<Project> Projects { get; set; }
    }
}
