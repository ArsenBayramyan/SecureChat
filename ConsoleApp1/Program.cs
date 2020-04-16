using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateModel mod = new CreateModel("Ars");
            Console.WriteLine(mod.MyProperty);
        }
    }
}
