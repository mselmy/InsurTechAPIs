using InsurTech.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Core.Entities
{
    public class Notification : BaseEntity
    {
        public string Body { get; set; }
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
        public bool IsRead { get; set; }
    }
}
