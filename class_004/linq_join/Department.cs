using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex_LINQ_Join
{
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public static List<Department> GetDepartments()
        {
            return new List<Department>()
            {
                new Department{ID=1,Name="It"},
                new Department{ID=2,Name="HR"},
                new Department{ID=3,Name="Admin"},
            };
        }
    }
}
