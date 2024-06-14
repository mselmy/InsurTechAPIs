using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Service.Models
{
    public class Message
    {
        public List<MailAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Message(IEnumerable<string> to , string subject , string content)
        {
            To = new List<MailAddress>();
            To.AddRange(to.Select(x => new MailAddress("email", x)));
            Subject = subject;
            Content = content;
        }

    }
}
