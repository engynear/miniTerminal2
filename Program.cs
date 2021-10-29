using System;
using System.IO;


namespace miniTerminal2
{
    
    class Program
    {
        private static DirectoryInfo dir;
        private static int depth;
        static void Main(string[] args)
        {
            Console.WriteLine("Enter directory path: ");
            dir = new DirectoryInfo(Console.ReadLine());
            if (!dir.Exists) throw new ArgumentException("Incorrect directory");
            Console.WriteLine("Enter depth: ");
            depth = (int) Int32.Parse(Console.ReadLine());

            
            foreach (var file in dir.GetFiles())
            {
                Console.WriteLine(file.Name);
                //file.Length;
            }
            Console.WriteLine("Hello World!");
        }
    }
}
