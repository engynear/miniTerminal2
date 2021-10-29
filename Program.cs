using System;
using System.IO;


namespace miniTerminal2
{

    class Program
    {
        private static DirectoryInfo dir;
        private static int globalDepth;
        private static string line = "    ";
        private static string lineStart = "└───";
        private static string fileName;

        static void printSpaces(int count)
        {
            for (int i = 0; i < count - 2; i++)
            {
                Console.Write(line);
            }
            if (count != 1) Console.Write(lineStart);
        }

        static void printingFiles(DirectoryInfo dir, int depth)
        {
            depth++;

            foreach (var dirInfo in dir.GetDirectories())
            {
                printSpaces(depth + 1);
                Console.WriteLine("[" + dirInfo.Name + "]");
                if (depth < globalDepth)
                {
                    printingFiles(dirInfo, depth);
                }
            }

            foreach (var fileInfo in dir.GetFiles())
            {
                printSpaces(depth + 1);
                Console.WriteLine(fileInfo.Name + " (" + (fileInfo.Length / 8) + " B)");
            }

        }

        static void Main(string[] args)
        {
            Console.Write("Enter directory path: ");
            dir = new DirectoryInfo(Console.ReadLine());
            if (!dir.Exists) throw new ArgumentException("Incorrect directory");
            Console.Write("Enter depth: ");
            globalDepth = (int)Int32.Parse(Console.ReadLine());
            Console.WriteLine(dir.Name);

            fileName = dir.Name + DateTime.Now.ToString();

            printingFiles(dir, 0);
        }
    }
}
