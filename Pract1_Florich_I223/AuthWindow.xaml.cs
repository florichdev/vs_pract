using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Pract1_Florich_I223.Logic;
using Pract1_Florich_I223.dbContext;

namespace Pract1_Florich_I223
{
    public partial class AuthWindow : Window
    {
        private IAuthService _authService;
        private ShopDB_FlorichEntities dbContext;

        public AuthWindow()
        {
            InitializeComponent();
            dbContext = new ShopDB_FlorichEntities();
            _authService = new AuthService();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            string Login = tbxlogin.Text;
            string Pass = tbxPass.Text;


            if (_authService.CheckData(Login,Pass))
            {
                
            }
            else
            {
                MessageBox.Show("Ошибка, проверьте правильность введённых данных в полях");
            }

        }
    }
}
