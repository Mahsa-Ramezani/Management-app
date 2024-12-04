using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using DataAccess;
using DataAccess.Models;

namespace WpfProductManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EmployeeDataAccess employeeDataAccess = new EmployeeDataAccess();
        CustomerDataAccess customerDataAccess = new CustomerDataAccess();
        ProductDataAccess productDataAccess = new ProductDataAccess();
        
        ObservableCollection<Employee> Employees = new ObservableCollection<Employee>();
        ObservableCollection<Customer> Customers = new ObservableCollection<Customer>();
        ObservableCollection<Product> Products = new ObservableCollection<Product>();


        public Employee currentEmployee { get; set; } = new Employee();
        public Customer currentCustomer { get; set; } = new Customer();
        public Product currentProduct { get; set; } = new Product();


        public MainWindow()
        {
            InitializeComponent();
            fillData();
            EmployeesGrid.ItemsSource = Employees;
            ProductsGrid.ItemsSource = Products;
            CustomersGrid.ItemsSource = Customers;
        }

        private void fillData()
        {
            Employees = employeeDataAccess.Employees;
            Customers = customerDataAccess.Customers;
            Products = productDataAccess.Products;
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            HomePanel.Visibility = Visibility.Visible;
            EmployeesPanel.Visibility = Visibility.Collapsed;
            CustomersPanel.Visibility = Visibility.Collapsed;
            ProductsPanel.Visibility = Visibility.Collapsed;
        }

        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            HomePanel.Visibility = Visibility.Collapsed;
            EmployeesPanel.Visibility = Visibility.Visible;
            CustomersPanel.Visibility = Visibility.Collapsed;
            ProductsPanel.Visibility = Visibility.Collapsed;
        }

        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            HomePanel.Visibility = Visibility.Collapsed;
            EmployeesPanel.Visibility = Visibility.Collapsed;
            CustomersPanel.Visibility = Visibility.Visible;
            ProductsPanel.Visibility = Visibility.Collapsed;

        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            HomePanel.Visibility = Visibility.Collapsed;
            EmployeesPanel.Visibility = Visibility.Collapsed;
            CustomersPanel.Visibility = Visibility.Collapsed;
            ProductsPanel.Visibility = Visibility.Visible;

        }

        private void EmployeesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(EmployeesGrid.SelectedIndex >= 0)
            {
                currentEmployee = EmployeesGrid.SelectedItem as Employee;
                EmployeeLabel.Content = currentEmployee.GetBasicInfo();
            }
        }

        private void ProductsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductsGrid.SelectedIndex >= 0)
            {
                currentProduct = ProductsGrid.SelectedItem as Product;
                ProductLabel.Content = currentProduct.GetBasicInfo();
            }

        }


        private void CustomersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomersGrid.SelectedIndex >= 0)
            {
                currentCustomer = CustomersGrid.SelectedItem as Customer;
                CustomerLabel.Content = currentCustomer.GetBasicInfo();
            }

        }
        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddEditEmployee addWindow = new AddEditEmployee(employeeDataAccess);
            addWindow.ShowDialog();
        }

        private void btnEditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedIndex >= 0)
            {
                currentEmployee = EmployeesGrid.SelectedItem as Employee;
                AddEditEmployee addWindow = new AddEditEmployee(employeeDataAccess, currentEmployee);
                addWindow.ShowDialog();
            }
        }

        private void btnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedIndex >= 0)
            {
                currentEmployee = EmployeesGrid.SelectedItem as Employee;
                employeeDataAccess.RemoveEmployee(currentEmployee.Id);
                EmployeeLabel.Content = "---";
            }
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddEditProduct addWindow = new AddEditProduct(productDataAccess);
            addWindow.ShowDialog();

        }

        private void btnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedIndex >= 0)
            {
                currentProduct = ProductsGrid.SelectedItem as Product;
                AddEditProduct addWindow = new AddEditProduct(productDataAccess, currentProduct);
                addWindow.ShowDialog();
            }
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {

            if (ProductsGrid.SelectedIndex >= 0)
            {
                currentProduct = ProductsGrid.SelectedItem as Product;
                productDataAccess.RemoveProduct(currentProduct.Id);
                ProductLabel.Content = "---";
            }
        }


        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddEditCustomer addWindow = new AddEditCustomer(customerDataAccess);
            addWindow.ShowDialog();
        }

        private void btnEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersGrid.SelectedIndex >= 0)
            {
                currentCustomer = CustomersGrid.SelectedItem as Customer;
                AddEditCustomer addWindow = new AddEditCustomer(customerDataAccess, currentCustomer);
                addWindow.ShowDialog();
            }

        }

        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersGrid.SelectedIndex >= 0)
            {
                currentCustomer = CustomersGrid.SelectedItem as Customer;
                customerDataAccess.RemoveCustomer(currentCustomer.Id);
                CustomerLabel.Content = "---";
            }
        }

    }
}
