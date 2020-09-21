using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class Manager : Person
    {
        public Manager(string name, int birthOfYear) : base(name, birthOfYear)
        {
            Employees = new List<Person>();
        }

        public List<Person> Employees { get; set; }
    }
}
