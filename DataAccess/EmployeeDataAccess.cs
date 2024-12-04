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
    public class EmployeeDataAccess
    {
        //csv => Products.csv
        //csv 1 database yani comma seprated hast
        private string path = @"./DemoDBEmployees.csv";
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public EmployeeDataAccess()
        {
            ReadEmployees();
        }

        private void ReadEmployees()
        {
            /*Employee emp1 = new Employee()
            {
                Id = 1,
                FirstName = "Heshmat",
                LastName = "Heshmatpoor",
                PhoneNumber = 91212121212,
                Address = "Tehran",
                Department = Department.Production,
                BaseSalary = 2500 
            };

            Employee emp2 = new Employee()
            {
                Id = 2,
                FirstName = "Mehran",
                LastName = "Modiri",
                PhoneNumber = 91214444444,
                Address = "Paris",
                Department = Department.Management,
                BaseSalary = 36000
            };

            Employees.Add(emp1);
            Employees.Add(emp2);*/

            using(var reader = new StreamReader(path))
            {
                Employees.Clear();
                while (!reader.EndOfStream)
                { 
                    string line = reader.ReadLine();
                    string[] values = line.Split(';');
                    Enum.TryParse(values[5], out Department dept);

                    Employee emp = new Employee()
                    {
                        Id = Convert.ToInt32(values[0]),
                        FirstName = values[1],
                        LastName = values[2],
                        PhoneNumber = Convert.ToUInt64(values[3]),
                        Address = values[4],
                        Department = dept,
                        BaseSalary = Convert.ToDecimal(values[6]),
                    };
                    Employees.Add(emp);
                }
            }
        }

        private void SaveEmployees()
        {
            using (var writer = new StreamWriter(path))
            {
                foreach (Employee emp in Employees)
                {
                    string Id = emp.Id.ToString();
                    string FirstName = emp.FirstName.ToString();
                    string LastName = emp.LastName.ToString();
                    string PhoneNumber = emp.PhoneNumber.ToString();
                    string Address = emp.Address;
                    string Department = emp.Department.ToString();
                    string BaseSalary = emp.BaseSalary.ToString();
                    string line = string.Format("{0};{1};{2};{3};{4};{5};{6}",
                        Id, FirstName, LastName, PhoneNumber, Address, Department, BaseSalary);
                    writer.WriteLine(line);


                }

            }
        }

        public void AddEmployee(Employee emp)
        {
            Employees.Add(emp);
            SaveEmployees();
        }

        public void RemoveEmployee(int id)
        {
            Employee temp = Employees.First(x => x.Id == id);
            Employees.Remove(temp);
            SaveEmployees();
        }

        public void EditEmployee(Employee emp)
        {
            Employee temp = Employees.First(x => x.Id == emp.Id);
            int index = Employees.IndexOf(temp);
            Employees[index] = emp;
            SaveEmployees();

        }

        public int GetNextId()
        {
            int index = Employees.Any() ? Employees.Max(x => x.Id) + 1 : 1;
            return index;
        }
    }
}
