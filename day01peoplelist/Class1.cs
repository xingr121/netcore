using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day01peoplelist
{
    internal class Person
    {
        public Person(string name, int age, string city)
        {
            Name = name;
            Age = age;
            City = city;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            // Name 2-100 characters long, not containing semicolons
            set
            {
                if (value.Length < 2 || value.Length > 100 || value.Contains(";"))
                {
                    throw new ArgumentException("Name must be 2-100 characters long, no semicolons");
                }
                _name = value;
            }
        }

        private int _age;
        // Age 0-150
        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 0 || value > 150)
                {
                    throw new ArgumentException("Age must be 0-150");
                }
                _age = value;
            }
        }
        // City 2-100 characters long, not containing semicolons
        private string _city;
        public string City // City 2-100 characters long, not containing semicolons
        { // property
            get
            {
                return _city;
            }
            set
            {
                //if (value.Length < 2 || value.Length > 100 || value.Contains(";"))
                if (!Regex.IsMatch(value, @"^[^;]{2,100}$")) //, RegexOptions.IgnoreCase))
                {
                    throw new ArgumentException("Name must be 2-100 characters long, no semicolons");
                }
                _city = value;
            }
        }

        public override string ToString()
        {
            return $"{Name} is {Age} from {City}";
        }
    }
}
