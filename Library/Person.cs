using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public abstract class Person
    {
        public String Name { get; set; }
        public int BirthOfYear { get; set; }

        protected Person(string name, int birthOfYear)
        {
            Name = name;
            BirthOfYear = birthOfYear;
        }

        public int Age
        {
            get { return DateTime.Now.Year - BirthOfYear; }
        }
    }
}
