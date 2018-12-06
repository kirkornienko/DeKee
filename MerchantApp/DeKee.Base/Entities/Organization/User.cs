using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Organization
{
    public class OrganizationUser: BaseEntityGeneric<long>
    {
        public static class Statuses
        {
            public static readonly string Created = "Created";
            public static readonly string Blocked = "Blocked";
            public static readonly string Active = "Active";
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DateTime? CloseDate { get; set; }
        public bool IsLocked { get; set; }
        public bool IsNeedChangePassword { get; set; }
        public DateTime? LastSignInDate { get; set; }
        public DateTime? LastChangePassword { get; set; }

        public long PositionId { get; set; }
        [ForeignKey("PositionId")]
        public Position Position { get; set; }

        public long BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
