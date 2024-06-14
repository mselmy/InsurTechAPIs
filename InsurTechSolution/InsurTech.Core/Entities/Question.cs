using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Core.Entities
{
    public class Question : BaseEntity
    {
        public string Body { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<RequestQuestion> RequestQuestions { get; set; } = new List<RequestQuestion>();
    }
}
