﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Pract1_Florich_I223.dbContext;
using Pract1_Florich_I223.Logic;
using Pract1_Florich_I223.Model;
using System.Windows;

namespace Pract1_Florich_I223.Logic
{
    public class RoleService
    {
        private ShopDB_FlorichEntities dbContext = new ShopDB_FlorichEntities();

        public Users CurrentUser { get; set; }

        public void SetCurrentUser(Users user)
        {
            CurrentUser = user;
        }

        public string GetUserRole(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Пользователь не найден");
            }

            using (var dbContext = new ShopDB_FlorichEntities())
            {
                var CurrentUser = dbContext.Users.Include("Role").FirstOrDefault(u => u.RoleID == user.RoleID);

                if (CurrentUser != null && CurrentUser.Role != null)
                {
                    return CurrentUser.Role.RoleName;
                }
                else
                {
                    return "Роль не определена";
                }
            }
        }
    }
}
