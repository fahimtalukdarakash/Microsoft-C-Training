using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex_LINQ_Join
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public static List<Employee> GetEmployees()
        {
            return new List<Employee>()
            {
                new Employee{ID=1,Name="emp1",DepartmentId=1},
                new Employee{ID=2,Name="emp2",DepartmentId=2},
                new Employee{ID=3,Name="emp3",DepartmentId=3},
                new Employee{ID=4,Name="emp4",DepartmentId=4},
                new Employee{ID=5,Name="emp5",DepartmentId=1},
                new Employee{ID=6,Name="emp6",DepartmentId=2}
            };
        }
    }
}
