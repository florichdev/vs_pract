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
using Pract1_Florich_I223;
using Pract1_Florich_I223.dbContext;
using Pract1_Florich_I223.Logic;

namespace Pract1_Florich_I223
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private florich_usersEntities dbContext;

        public MainWindow()
        {
            InitializeComponent();
            dbContext = new florich_usersEntities();
            LoadProducts();
        }

        private void LoadProducts()
        {
            ProductListBox.ItemsSource = dbContext.Products.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string productName = NameTextBox.Text;
                if (string.IsNullOrEmpty(productName))
                {
                    MessageBox.Show("Введите название товара.");
                    return;
                }

                if (!decimal.TryParse(PriceTextBox.Text, out decimal productPrice))
                {
                    MessageBox.Show("Введите корректную цену товара.");
                    return;
                }

                string productDescription = DescriptionTextBox.Text;

                var newProduct = new Pract1_Florich_I223.dbContext.Products
                {
                    name_prod = productName,
                    price_prod = productPrice,
                    description_prod = productDescription
                };


                dbContext.Products.Add(newProduct);
                dbContext.SaveChanges();

                LoadProducts();
                NameTextBox.Clear();
                PriceTextBox.Clear();
                DescriptionTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении товара: {ex.Message}");
            }
        }
    }
}

/*namespace Pract1_Florich_I223
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
}*/
