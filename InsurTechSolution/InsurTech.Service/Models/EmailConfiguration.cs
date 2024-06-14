using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Service.Models
{
    public class EmailConfiguration
    {
        public string Form { get; set; } = null;
        public string smtpServer { get; set; } = null;
        public int Port { get; set; }
        public string UserName { get; set; } = null;
        public string Password { get; set; } = null;

    }
}
