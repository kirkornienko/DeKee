using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeKee.Base.Entities
{
    public abstract class SupportEntityGeneric<KType> : IRepositoryEntity<KType>
    {
        public KType Id { get; set; }
    }
    public abstract class BaseEntityGeneric<KType> : IRepositoryEntity<KType>
    {
        public KType Id { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DateCreated { get; set; }
    }
    public abstract class BaseEntity : BaseEntityGeneric<string>
    {
        
    }

    public interface IRepositoryEntity<KType>
    {
        KType Id { get; set; }
    }

    public abstract class SupervisedEntityGeneric<KType>: BaseEntityGeneric<KType>
    {
        public long? CreateUserId { get; set; }
        [ForeignKey("CreateUserId")]
        public virtual Organization.OrganizationUser CreateUser { get; set; }

        public long? ModifyUsedId { get; set; }
        [ForeignKey("ModifyUsedId")]
        public virtual Organization.OrganizationUser ModifyUser { get; set; }
    }
}
