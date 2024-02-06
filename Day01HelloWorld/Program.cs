using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day01HelloWorld
{
    internal class Program
    {
        static List<Person> people = new List<Person>();
        const string filePath = @"..\..\people.txt";

        static void Main(string[] args)
        {
            /*  try
              {
                  Console.WriteLine("Hello World");
                  Console.Write("What is your name?");
                  string name = Console.ReadLine();
                  Console.WriteLine("what is your age");
                  string agestr = Console.ReadLine();
                  int age = int.Parse(agestr); //ex FormatException, OverflowException
                  int agetwo;
                  if (int.TryParse(agestr, out agetwo))
                  {
                      Console.WriteLine("hello {0}, you are {1} y/o", name, agetwo);
                  }

                  Console.WriteLine("hello {0}, you are {1} years old", name, age);
                  string greeting = $"hello {name},you are {age} years old";
                  Console.WriteLine(greeting);
                  int nameLen = name.Length;
                  if (name.ToLower() == "santa")
                  {
                      Console.WriteLine("santa, is you");
                  }
              }
              catch (Exception ex) when (ex is FormatException || ex is OverflowException)
              {
                  Console.WriteLine($"Error:you must enter an integer number({ex.ToString()}).");
              }
              finally
              {
                  Console.WriteLine("presss any key to finish");
                  Console.ReadKey();
              }
            */
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File not found. Exiting program.");
                    Environment.Exit(1);
                }

            }
            catch (SystemException ex)
            {
                Console.WriteLine("Error reading from file: " + ex.Message);
            }


            ReadAllPeopleFromFile();

            int choice;
            do
            {
                displayMenu();
                string ch = Console.ReadLine();
                if (int.TryParse(ch, out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddPersonInfo();
                            break;
                        case 2:
                            ListAllPersonsInfo();
                            break;
                        case 3:
                            FindPersonByName();
                            break;
                        case 4:
                            FindPersonYoungerThan();
                            break;
                        case 0:
                            Console.WriteLine("Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Invalid input, try again");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("invalid choice, try again");
                }


            } while (choice != 0);


            SaveAllPeopleToFile();


        }
        static void displayMenu()
        {
            Console.WriteLine("What do you want to do?\r\n" + "1. Add person info\r\n"
                    + "2. List persons info\r\n"
                    + "3. Find a person by name\r\n"
                    + "4. Find all persons younger than age\r\n" + "0. Exit\r\n"
                    + "Please enter your choice: ");
        }
        static void AddPersonInfo()
        {
            Console.WriteLine("How many persons do you want to add?");
            string numStr = Console.ReadLine();
            int num;
            if (int.TryParse(numStr, out num))
            {
                for (int j = 1; j <= num; j++)
                {
                    Person person = new Person();
                    string name;
                    do
                    {
                        Console.WriteLine("person" + j + " Enter the name: ");
                        name = Console.ReadLine().Trim();
                        if (string.IsNullOrEmpty(name))
                        {
                            Console.WriteLine("Name cannot be empty. Please enter a valid name.");
                        }
                    } while (string.IsNullOrEmpty(name));


                    person.Name = name;
                    string ageStr;
                    int age;
                    do
                    {
                        Console.WriteLine("person" + j + " Enter the age: ");
                        ageStr = Console.ReadLine();

                        if (!int.TryParse(ageStr, out age) || age < 0)
                        {
                            Console.WriteLine("Invalid age.");
                        }
                    } while (!int.TryParse(ageStr, out age) || age < 0);

                    person.Age = age;

                    string city;
                    do
                    {
                        Console.WriteLine("person" + j + " Enter the city: ");
                        city = Console.ReadLine().Trim();
                        if (string.IsNullOrEmpty(city))
                        {
                            Console.WriteLine("City cannot be empty. Please enter a valid city name.");
                        }
                    } while (string.IsNullOrEmpty(city));
                    person.City = city;
                    people.Add(person);
                }
            }
            else
            {

                Console.WriteLine("invalid input");
            }
            Console.WriteLine("Succesfully added" + num + "person(s)");
            return;
        }
        static void ListAllPersonsInfo()
        {
            Console.WriteLine("all persons info as following:");
            foreach (var person in people)
            {
                Console.WriteLine(person);
            }
        }
        static void FindPersonByName()
        {
            Console.WriteLine("what is the name are you searching for?");
            string pName = Console.ReadLine();
            foreach (var person in people)
            {
                if (person.Name.Contains(pName))
                {
                    Console.WriteLine(person);
                }
            }
        }
        static void FindPersonYoungerThan()
        {
            Console.WriteLine("Enter the age limit for your search");
            string ageStr = Console.ReadLine();
            int age;
            if (int.TryParse(ageStr, out age))
            {
                foreach (var person in people)
                {
                    if (person.Age < age)
                    {
                        Console.WriteLine(person);
                    }
                }
            }

        }
        static void ReadAllPeopleFromFile()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File not found.");
                    return;
                }

                string[] readText = File.ReadAllLines(filePath);
                foreach (string line in readText)
                {
                    string[] parts = line.Split(';');
                    if (parts.Length == 3)
                    {
                        string name = parts[0];
                        string city = parts[2];
                        if (int.TryParse(parts[1], out int age))
                        {
                            Person person = new Person { Name = name, Age = age, City = city, };
                            people.Add(person);
                        }
                        else
                        {
                            Console.WriteLine($"Invalid age for person: {line}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Invalid data format: {line}");
                    }
                }
            }
            catch (SystemException ex)
            {
                Console.WriteLine("Error reading to file:" + ex.Message);
            }

        }
        static void SaveAllPeopleToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var person in people)
                    {
                        writer.WriteLine($"{person.Name};{person.Age};{person.City}");
                    }
                }
            }
            catch (SystemException ex)
            {
                Console.WriteLine("Error writing to file:" + ex.Message);
            }


        }
    }
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

        public Person()
        {
        }

        public Person(string name, int age, string city)
        {
            this.Name = name;
            this.Age = age;
            this.City = city;
        }

        public override string ToString()
        {
            return $"{Name} is {Age} from {City}";
        }
    }

}
