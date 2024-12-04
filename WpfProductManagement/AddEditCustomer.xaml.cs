using DataAccess.Models;
using DataAccess;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WpfProductManagement
{
    /// <summary>
    /// Interaction logic for AddEditCustomer.xaml
    /// </summary>
    public partial class AddEditCustomer : Window
    {
        private CustomerDataAccess customerDataAccess;
        private Customer editingCustomer;
        private bool editMode = false;

        public AddEditCustomer(CustomerDataAccess customerDtAccess)
        {
            InitializeComponent();
            customerDataAccess = customerDtAccess;
        }

        public AddEditCustomer(CustomerDataAccess customerDtAccess, Customer customer)
        {
            InitializeComponent();
            customerDataAccess = customerDtAccess;
            tbFirstName.Text = customer.FirstName;
            tbLastName.Text = customer.LastName;
            tbPhone.Text = customer.PhoneNumber.ToString();
            tbAddress.Text = customer.Address;
            editMode = true;
            editingCustomer = customer;


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (editMode)
            {
                Customer cst = new Customer()
                {
                    Id = editingCustomer.Id,
                    FirstName = tbFirstName.Text,
                    LastName = tbLastName.Text,
                    PhoneNumber = Convert.ToUInt64(tbPhone.Text),
                    Address = tbAddress.Text
                };
                customerDataAccess.EditCustomer(cst);

            }
            else
            {
                Customer cst = new Customer()
                {
                    Id = customerDataAccess.GetNextId(),
                    FirstName = tbFirstName.Text,
                    LastName = tbLastName.Text,
                    PhoneNumber = Convert.ToUInt64(tbPhone.Text),
                    Address = tbAddress.Text
                };
                customerDataAccess.AddCustomer(cst);
            }
            this.Close();
        }
    }
}
