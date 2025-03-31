using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pract1_Florich_I223.dbContext;
using Pract1_Florich_I223.Model;
using System.Data.Entity;
using Pract1_Florich_I223.Logic;

namespace Pract1_Florich_I223.Logic
{
    public class AuthService : IAuthService
    {
        private RoleService roleService;
        private ShopDB_FlorichEntities dbContext = new ShopDB_FlorichEntities();

        public bool CheckData(string Login, string Pass)
        {
            var user = dbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Login == Login && u.Pass == Pass);

            if (user != null && user.Pass == Pass)
            {
                ProductDataGrid dataGrid = new ProductDataGrid(user);
                dataGrid.Show();
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
