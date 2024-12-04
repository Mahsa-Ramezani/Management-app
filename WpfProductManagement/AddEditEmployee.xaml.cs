using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace WpfProductManagement
{
    /// <summary>
    /// Interaction logic for AddEditEmployee.xaml
    /// </summary>
    public partial class AddEditEmployee : Window
    {
        private EmployeeDataAccess employeeDataAccess;
        private Employee editingEmployee;
        private bool isEdit = false;
        public AddEditEmployee(EmployeeDataAccess empDataAccess)
        {
            InitializeComponent();
            employeeDataAccess = empDataAccess;
        }

        public AddEditEmployee(EmployeeDataAccess empDataAccess, Employee emp)
        {
            InitializeComponent();
            employeeDataAccess = empDataAccess;
            editingEmployee = emp;
            isEdit = true;
            tbFirstName.Text =editingEmployee.FirstName;
            tbLastName.Text =editingEmployee.LastName;
            tbPhoneNumber.Text = editingEmployee.PhoneNumber.ToString();
            tbAddress.Text = editingEmployee.Address;
            tbSalary.Text = editingEmployee.BaseSalary.ToString();
            comboDepartment.SelectedIndex = (int)editingEmployee.Department;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true;
            isValid = CheckEmployeeValidity();

            if (isValid)
            {
                if (isEdit)
                {
                    Employee emp = new Employee()
                    {
                        Id = editingEmployee.Id,
                        FirstName = tbFirstName.Text,
                        LastName = tbLastName.Text,
                        PhoneNumber = Convert.ToUInt64(tbPhoneNumber.Text),
                        Address = tbAddress.Text,
                        BaseSalary = Convert.ToDecimal(tbSalary.Text),
                        Department = (Department)comboDepartment.SelectedIndex
                    };
                    employeeDataAccess.EditEmployee(emp);

                }
                else
                {
                    Employee emp = new Employee()
                    {
                        Id = employeeDataAccess.GetNextId(),
                        FirstName = tbFirstName.Text,
                        LastName = tbLastName.Text,
                        PhoneNumber = Convert.ToUInt64(tbPhoneNumber.Text),
                        Address = tbAddress.Text,
                        BaseSalary = Convert.ToDecimal(tbSalary.Text),
                        Department = (Department)comboDepartment.SelectedIndex
                    };
                    employeeDataAccess.AddEmployee(emp);
                }
                this.Close();
            }
        }

        private bool CheckEmployeeValidity()
        {
            
            bool isValid = true;
            string firstName = tbFirstName.Text.Trim().ToLower();
            string lastName = tbLastName.Text.Trim().ToLower();
            string phoneNumber = tbPhoneNumber.Text.Trim().ToLower();
            string address = tbAddress.Text.Trim().ToLower();
            int department = comboDepartment.SelectedIndex;
            string baseSalary = tbSalary.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(firstName))
            {
                isValid = false;
                lblError.Content = "**First Name is invalid!";
            }
            else if (string.IsNullOrEmpty(lastName))
            {
                isValid = false;
                lblError.Content = "**Last Name is invalid!";
            }
            else if (!UInt64.TryParse(phoneNumber, out ulong p))
            {
                isValid = false;
                lblError.Content = "**Phone Number is invalid!";
            }
            else if (address.Contains("paris"))
            {
                isValid = false;
                lblError.Content = "**Paris is not accepted!";
            }
            else if (department < 0)
            {
                isValid = false;
                lblError.Content = "**Please select a Department!";
            }
            else if (!decimal.TryParse(baseSalary, out decimal b) || b > 4000)
            {
                isValid = false;
                lblError.Content = "**Salary is incorrect!";
            }
            else
            {
                lblError.Content = "";
            }

            return isValid;

        }

        private void tbPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            //lblError dar lahze vared krdn meghdar txtbox nemone
            string phoneNumber = tbPhoneNumber.Text.Trim().ToLower();
            if (!UInt64.TryParse(phoneNumber, out ulong p))
            {
                lblError.Content = "**Phone Number is invalid!";
            }
            else
            {
                lblError.Content = "";
            } 
        }
    }
}
