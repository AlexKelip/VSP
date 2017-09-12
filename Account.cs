using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Login_System_Tut_ecnrypted;

namespace Login_System_Tut_ecnrypted
{
    public class Account
    {
        public Account()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
