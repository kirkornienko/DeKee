using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.User
{
    public class User : BaseEntity
    {
        public bool IsPrimary { get; set; }
        public string PrimaryKey { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Confirm { get; set; }
        public string PhoneNumber { get; set; }
        public string B4UPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WalletAddress { get; set; }
        public List<string> MnemonicList { get; set; }
        public string PrivateKey { get; set; }

        public string AggreementId { get; set; }
        public string CertificateId{ get; set; }

        [ForeignKey("AggreementId")]
        public UserFile Aggreement { get; set; }
        [ForeignKey("CertificateId")]
        public UserFile Certificate { get; set; }
        public string ContactPerson { get; set; }
        public string AgreementNumber { get; set; }
        public string AgreementNumberDate { get; set; }
        public string CertificateNumber { get; set; }
        public string CertificateDate { get; set; }
        public string Country { get; set; }
        public string BrunchNumber { get; set; }
        public string BrunchAddress { get; set; }
    }
}
