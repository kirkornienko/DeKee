namespace DeKee.Base.Entities.Organization
{
    public class BranchType:SupportEntityGeneric<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}