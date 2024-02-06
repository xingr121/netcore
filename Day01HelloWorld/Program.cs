using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
            Console.WriteLine("Hello World");
            Console.Write("What is your name?");
            string name=Console.ReadLine();
            Console.WriteLine("what is your age");
            string agestr=Console.ReadLine();
                int age = int.Parse(agestr); //ex FormatException, OverflowException
                int agetwo;
                if (int.TryParse(agestr, out agetwo))
                {
                    Console.WriteLine("hello {0}, you are {1} y/o", name, agetwo);
                }

            Console.WriteLine("hello {0}, you are {1} years old", name, age);
                string greeting = $"hello {name},you are {age} years old";
                Console.WriteLine(greeting);
                    int nameLen=name.Length;
                if (name.ToLower() == "santa")
                {
                    Console.WriteLine("santa, is you");
                }
            }catch(Exception ex) when(ex is FormatException ||ex is OverflowException)
            {
                Console.WriteLine($"Error:you must enter an integer number({ex.ToString()}).");
            }
            finally { Console.WriteLine("presss any key to finish");
            Console.ReadKey();
            }
           

        }
    }
}
