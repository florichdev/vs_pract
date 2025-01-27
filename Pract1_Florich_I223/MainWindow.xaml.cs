using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Pract1_Florich_I223.Logic;

namespace Pract1_Florich_I223
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow mainWindow = new AuthWindow();
            mainWindow.Show();
        }

        private List<IProduct> _products = new List<IProduct>();

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            var product = new Product();

            product.Name = NameTextBox.Text;

            if (decimal.TryParse(PriceTextBox.Text, out decimal price))
            {
                product.Price = price;

                _products.Add(product);

                ProductListBox.ItemsSource = null; 
                ProductListBox.ItemsSource = _products;
            }
            else
            {
                MessageBox.Show("Введите цену");
            }
        }
    }
}
