using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract1_Florich_I223.Model
{
    public class User
    {
        public string Login { get; set; }
        public string Pass { get; set; }

        public User(string login, string pass)
        {
            Login = login;
            Pass = pass;
        }

    }
}