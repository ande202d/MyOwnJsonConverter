using System;

namespace MyOwnJsonConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ReflectionWorker w = new ReflectionWorker();
            w.Start();

            Console.ReadKey(true);
        }
    }
}
