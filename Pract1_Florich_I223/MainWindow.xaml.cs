using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
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
    public partial class MainWindow : Window
    {
        private ShopDB_FlorichEntities dbContext = new ShopDB_FlorichEntities();

        private Products _product;

        public MainWindow(Products products = null)
        {
            InitializeComponent();
            LoadData();

            _product = products;

            if (products != null) {
                NameTextBox.Text = _product.ProductName;
                PriceTextBox.Text = _product.Price.ToString();
                DescriptionTextBox.Text = _product.Description;
            };
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
                    ProductName = productName,
                    Price = productPrice,
                    Description = productDescription
                };


                dbContext.Products.Add(newProduct);
                dbContext.SaveChanges();

                NameTextBox.Clear();
                PriceTextBox.Clear();
                DescriptionTextBox.Clear();
                MessageBox.Show($"Товар добавлен");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении товара: {ex.Message}");
            }
            decimal price;
            if (decimal.TryParse(PriceTextBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out price))
            {
                _product = new Products()
                {
                    ProductName = NameTextBox.Text,
                    Price = price,
                    Description = DescriptionTextBox.Text
                };
            }
        }
        private void LoadData()
        {

            try
            {
                var products = dbContext.Products.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}