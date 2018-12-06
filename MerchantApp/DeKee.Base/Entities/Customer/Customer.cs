using DeKee.Base.Entities.Address;
using DeKee.Base.Entities.Globalization;
using DeKee.Base.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Customer
{
    public class Customer : SupervisedEntityGeneric<long>
    {
        public string Code { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string ShortName { get; set; }
        public string TaxCode { get; set; }
        public bool IsTaxCode { get; set; }
        public Sex Sex { get; set; }

        public long CitizenshipId { get; set; }
        [ForeignKey("CitizenshipId")]
        public Country Citizenship { get; set; }

        public long IdentityDocumentId { get; set; }
        [ForeignKey("IdentityDocumentId")]
        public IdentityDocument IdentityDocument { get; set; }        

        public string Phone { get; set; }
        public string Email { get; set; }

        //public long? CustomerAddressId { get; set; }
        //[ForeignKey("CustomerAddressId")]
        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }
        public virtual ICollection<CustomerFile> CustomerFiles { get; set; }

        //public long CreateUserId { get; set; }
        //public long ModifyUserId { get; set; }
    }
}
