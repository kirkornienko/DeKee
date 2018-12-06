using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Organization
{
    public class UserRole : SupportEntityGeneric<long>
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }

        [ForeignKey("UserId")]
        public OrganizationUser User { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
