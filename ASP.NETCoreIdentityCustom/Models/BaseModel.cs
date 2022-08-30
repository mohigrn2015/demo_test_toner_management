namespace ASP.NETCoreIdentityCustom.Models
{
    public class BaseModel
    {
        public DateTime? DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool? IsRowDeleted { get; set; }

    }
}
