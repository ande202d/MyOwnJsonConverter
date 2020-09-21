using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class Clerk : Person
    {
        public Clerk(string name, int birthOfYear, List<String> skills) : base(name, birthOfYear)
        {
            Skills = skills;
        }

        public List<String> Skills { get; set; }
    }
}
