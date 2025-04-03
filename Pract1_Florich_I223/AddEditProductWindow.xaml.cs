using System;
using System.Windows;
using Pract1_Florich_I223.dbContext;
using Pract1_Florich_I223.Logic;

namespace Pract1_Florich_I223
{
    public partial class AddEditProductWindow : Window
    {
        private ShopDB_FlorichEntities _dbContext;
        private Products _product;

        public AddEditProductWindow(Products product, ShopDB_FlorichEntities dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
            _product = product;

            if (_product != null)
            {
                NameTextBox.Text = _product.ProductName;
                PriceTextBox.Text = _product.Price.ToString();
                DescriptionTextBox.Text = _product.Description;
            }
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(NameTextBox.Text))
                {
                    MessageBox.Show("Введите название товара.");
                    return;
                }

                if (!decimal.TryParse(PriceTextBox.Text, out decimal price))
                {
                    MessageBox.Show("Введите корректную цену товара.");
                    return;
                }

                if (_product == null)
                {
                    _product = new Products
                    {
                        ProductName = NameTextBox.Text,
                        Price = price,
                        Description = DescriptionTextBox.Text,
                    };

                    _dbContext.Products.Add(_product);
                    MessageBox.Show("Товар добавлен");
                }
                else
                {
                    _product.ProductName = NameTextBox.Text;
                    _product.Price = price;
                    _product.Description = DescriptionTextBox.Text;

                    MessageBox.Show("Товар обновлен");
                }

                _dbContext.SaveChanges();

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении/обновлении товара: {ex.Message}");
            }
        }

        private void AddProductButton_ClickExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
