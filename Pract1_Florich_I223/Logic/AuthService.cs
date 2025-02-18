using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pract1_Florich_I223.dbContext;
using Pract1_Florich_I223.Model;

namespace Pract1_Florich_I223.Logic
{
    public class AuthService : IAuthService
    {
        private ShopDBEntities dbContext = new ShopDBEntities();

        public bool CheckData(string login, string pass)
        { 

            var user = dbContext.users.FirstOrDefault(u => u.login == login && u.password == pass);

            if (user != null && user.password == pass)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
