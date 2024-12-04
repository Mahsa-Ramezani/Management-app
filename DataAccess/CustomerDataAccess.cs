using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using System.Collections.ObjectModel;

namespace DataAccess
{
    public class CustomerDataAccess
    {
        public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();
        public CustomerDataAccess()
        {
            ReadCustomers();
        }

        private void ReadCustomers()
        {
            Customer cst1 = new Customer()
            {
                Id = 1,
                FirstName = "Reza",
                LastName = "Mashayekhi",
                PhoneNumber = 91212121212,
                Address = "Italy"
            };

            Customer cst2 = new Customer()
            {
                Id = 2,
                FirstName = "Akbar",
                LastName = "Karami",
                PhoneNumber = 91214444444,
                Address = "Booshehr"
            };

            Customers.Add(cst1);
            Customers.Add(cst2);
        }

        public void AddCustomer(Customer cst)
        {
            Customers.Add(cst);
        }

        public void RemoveCustomer(int id)
        {
            Customer temp = Customers.First(x => x.Id == id);
            Customers.Remove(temp);
        }

        public void EditCustomer(Customer cst)
        {
            Customer temp = Customers.First(x => x.Id == cst.Id);
            int index = Customers.IndexOf(temp);
            Customers[index] = cst;

        }

        public int GetNextId()
        {
            int index = Customers.Any() ? Customers.Max(x => x.Id) + 1 : 1;
            return index;
        }
    }
}
