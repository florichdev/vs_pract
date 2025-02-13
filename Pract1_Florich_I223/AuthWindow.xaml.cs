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
        private florich_usersEntities dbContext;

        public AuthWindow()
        {
            InitializeComponent();
            dbContext = new florich_usersEntities();
            _authService = new AuthService();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            string login = tbxlogin.Text;
            string pass = tbxPass.Text;

            string hashedPassword = HashPassword(pass);

            var user = dbContext.Users.FirstOrDefault(u => u.login_u == login && u.pass_u == hashedPassword);

            if (_authService.CheckData(login,pass))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Ошибка, проверьте правильность введённых данных в полях");
            }
        }
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
