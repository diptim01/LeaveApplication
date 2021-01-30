using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaijaFarmers.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
           {
           new Employee
           {
               Id =1,
               Name = "Dipo",
               Department = "HR",
               Email = "olas@yahoo.com"
           },
             new Employee
           {
               Id =2,
               Name = "DiShadepo",
               Department = "IT",
               Email = "Shade@yahoo.com"
           },
               new Employee
           {
               Id =3,
               Name = "Mike",
               Department = "Business",
               Email = "Mike@yahoo.com"
           }
            };
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(x => x.Id == id);
        }
    }
}
