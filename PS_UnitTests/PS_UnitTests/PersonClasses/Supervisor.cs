using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_UnitTests.PersonClasses
{
    public class Supervisor : Person
    {
        public List<Employee> Employees { get; set; }
    }
}
