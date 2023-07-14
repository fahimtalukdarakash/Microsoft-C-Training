using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex_LINQ_Join
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Query Expression
            var result = from e in Employee.GetEmployees()
                         join d in Department.GetDepartments() on e.DepartmentId equals d.ID
                         select new
                         {
                             EmployeeName= e.Name,
                             DepartmentName= d.Name
                         };
            foreach (var emp in result) {
                Console.WriteLine(emp.EmployeeName+"\t"+emp.DepartmentName);
            }
            Console.WriteLine("*****************************");
            Console.WriteLine();
            //Lamda Expression
            var result2 = Employee.GetEmployees().Join(Department.GetDepartments(),
                e=>e.DepartmentId,d=>d.ID,(employee, department)=> new
                {
                    EmployeeName = employee.Name,
                    DepartmentName= department.Name
                });
            foreach (var emp in result2)
            {
                Console.WriteLine(emp.EmployeeName + "\t" + emp.DepartmentName);
            }
            Console.ReadKey();
        }
    }
}
