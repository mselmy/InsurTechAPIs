using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Core.Entities
{
    public class RequestQuestion : BaseEntity
    {
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public int UserRequestId { get; set; }
        public virtual UserRequest UserRequest { get; set; }
        public string Answer { get; set; }
    }
}
