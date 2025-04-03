using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pract1_Florich_I223.dbContext;
using Pract1_Florich_I223.Model;
using System.Data.Entity;
using Pract1_Florich_I223.Logic;
using static Pract1_Florich_I223.Logic.RoleService;
using System.Windows;

namespace Pract1_Florich_I223.Logic
{
    public class CheckUserClick
    {
        private ShopDB_FlorichEntities dbContext = new ShopDB_FlorichEntities();

        public bool CheckUserClicks(string Login, string Pass)
        {
            var user = dbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Login == Login && u.Pass == Pass);

            if (user.Role != null && user.RoleID == 1)
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
