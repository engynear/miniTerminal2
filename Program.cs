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
        private static string output = "";

        static void printSpaces(int count)
        {
            for (int i = 0; i < count - 2; i++)
            {
                output += line;
            }
            if (count != 1) output += lineStart;
        }

        static void printingFiles(DirectoryInfo dir, int depth)
        {
            depth++;

            foreach (var dirInfo in dir.GetDirectories())
            {
                printSpaces(depth + 1);
                output += "[" + dirInfo.Name + "]" + "\n";
                if (depth < globalDepth)
                {
                    printingFiles(dirInfo, depth);
                }
            }

            foreach (var fileInfo in dir.GetFiles())
            {
                printSpaces(depth + 1);
                output += fileInfo.Name + " (" + (fileInfo.Length / 8) + " B)" + "\n";
            }

        }

        static void Main(string[] args)
        {
            Console.Write("Enter directory path: ");
            dir = new DirectoryInfo(Console.ReadLine());
            if (!dir.Exists) throw new ArgumentException("Incorrect directory");
            Console.Write("Enter depth: ");
            globalDepth = (int)Int32.Parse(Console.ReadLine());
            output += dir.Name + "\n";

            fileName = dir.Name + " - (" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ")";
            
            printingFiles(dir, 0);
            Console.WriteLine(output);

            string output_file = Directory.GetCurrentDirectory()+"/"+fileName+".txt";

            StreamWriter sr = new StreamWriter(output_file);
            sr.WriteLineAsync(output);
            sr.Close();
        }
    }
}
