using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace listgridview
{
    internal class Person
    {
        public Person()
        {
        }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; set; }
        public int Age { get; set; }

        public static bool IsNameValid(string name, out string error)
        {
            error = null;

            if (string.IsNullOrEmpty(name))
            {
                error = "Name cannot be empty.";
                return false;
            }

            Regex regex = new Regex(@"^[^\s;]{2,50}$"); // Match 2-50 characters without semicolons
            if (!regex.IsMatch(name))
            {
                error = "Name must be between 2 and 50 characters long and cannot contain semicolons (;).";
                return false;
            }

            return true;
        }
        public static bool IsAgeValid(int age, out string error)
        {
            error = null;

            if (age < 0 || age > 150)
            {
                error = "Age must be between 0 and 150.";
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return $"{Name} is {Age} y/o";
        }
    }
}
