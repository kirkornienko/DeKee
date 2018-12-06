namespace DeKee.Base.Entities.Organization
{
    public class BranchProperty : SupportEntityGeneric<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}