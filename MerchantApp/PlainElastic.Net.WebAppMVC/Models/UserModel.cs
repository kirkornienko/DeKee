using B4U.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlainElastic.Net.WebAppMVC.Models
{
    public class UserModel: IRepositoryEntity<string>
    {
        public bool IsPrimary { get; set; }
        public string PrimaryKey { get; set; }
        public string Login { get;  set; }
        public string Password { get;  set; }
        public bool Confirm { get; set; }
        public string Id { get;  set; }
        public MerchantDataModel Data { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string PhoneNumber { get; set; }
        public string B4UPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WalletAddress { get; set; }
        public List<string> MnemonicList { get; set; }
        public string PrivateKey { get; set; }
        public string Country { get;  set; }
        public string BrunchAddress { get; set; }
        public string BrunchNumber { get; set; }

        public void ComputeId()
        {
            var idBase = CreatedDate.Ticks;            
            Id = string.Format("{0}0{1}0{2}", idBase, Login.GetHashCode(), Password.GetHashCode());
        }
    }

    public class MerchantDataModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string AgreementNumber { get; set; }
        public string AgreementNumberDate { get; set; }
        public string CertificateNumber { get; set; }
        public string CertificateDate { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhoneNumber { get; set; }
    }
}