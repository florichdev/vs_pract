using System;
using System.Linq;
using System.Windows;
using Pract1_Florich_I223.Logic;
using Pract1_Florich_I223.dbContext;
using System.IO;
using System.Data;
using Pract1_Florich_I223.Model;
using static Pract1_Florich_I223.Logic.RoleService;
using System.Collections.Generic;

namespace Pract1_Florich_I223
{
    public partial class ProductDataGrid : Window
    {
        private RoleService roleService;
        private ShopDB_FlorichEntities _dbContext = new ShopDB_FlorichEntities();
        private Users _currentUser;

        public ProductDataGrid(Users user)
        {
            InitializeComponent();
            _currentUser = user;
            roleService = new RoleService();
            LoadUserData();
            LoadData();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = ProductsDataGrid.SelectedItem as Products;

            if (selectedProduct != null)
            {
                var editWindow = new AddEditProductWindow(selectedProduct, _dbContext);

                if (editWindow.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = ProductsDataGrid.SelectedItem as Products;

            if (selectedProduct != null)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить этот товар?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _dbContext.Products.Remove(selectedProduct);
                        _dbContext.SaveChanges();

                        LoadData();
                        MessageBox.Show("Товар удален");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении товара: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new AddEditProductWindow(null, _dbContext);

            if (editWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            try
            {
                var products = _dbContext.Products.ToList();
                ProductsDataGrid.ItemsSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public class UserInfo
        {
            public string Login { get; set; }
            public string RoleName { get; set; }
        }

        private void LoadUserData()
        {
            try
            {
                var userInfo = new List<UserInfo>
            {
                new UserInfo
                {
                    Login = _currentUser.Login,
                    RoleName = roleService.GetUserRole(_currentUser)
                }
            };

                UsersDataGrid.ItemsSource = userInfo;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportToCsvSimpleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                var time = DateTime.Today.ToShortDateString();

                string filePath = $"{time} report.csv";

                var data = ProductsDataGrid.ItemsSource.Cast<dbContext.Products>().ToList();

                var csv = new System.Text.StringBuilder();

                csv.AppendLine(string.Join(",", new string[] {"Название", "Цена", "Описание" }));

                foreach (var item in data)
                {
                    string[] fields =
                    {
                        item.ProductName,
                        item.Price.ToString(),
                        item.Description
                    };
                    csv.AppendLine(string.Join(",", fields));
                }

                File.WriteAllText(filePath, csv.ToString(), System.Text.Encoding.UTF8);

                MessageBox.Show("Данные успешно экспортированы в CSV!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте в CSV: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
