namespace DeKee.Base.Entities.Organization
{
    public class Position : SupportEntityGeneric<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}