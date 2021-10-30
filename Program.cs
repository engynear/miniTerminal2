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
            output += lineStart;
        }

        static void printingFiles(DirectoryInfo dir, int depth)
        {
            depth++;

            foreach (var dirInfo in dir.GetDirectories())
            {
                printSpaces(depth + 1);
                try{
                    output += "[" + dirInfo.Name + "]" + "\n";
                    if (depth < globalDepth)
                    {
                        printingFiles(dirInfo, depth);
                    }
                }catch(System.UnauthorizedAccessException){
                    output += "└───[Access error]\n";
                }

            }

            foreach (var fileInfo in dir.GetFiles())
            {
                printSpaces(depth + 1);
                try{
                    output += fileInfo.Name + " (" + (fileInfo.Length / 8) + " B)" + "\n";
                }catch(System.IO.IOException){
                    output += "{File access error}\n";
                }
                
            }

        }


        static void printingFiles(int depth){
            depth++;

            foreach (var drive in DriveInfo.GetDrives())
            {
                printSpaces(depth + 1);
                try{
                    output += "[" + drive.Name + "]" + "\n";
                    if (depth < globalDepth)
                    {
                        DirectoryInfo dir = new DirectoryInfo(path: drive.Name);
                        printingFiles(dir, depth);
                    }
                }catch(System.UnauthorizedAccessException){
                    output += "└───[Access error]\n";
                }

            }
       }

        static void Main(string[] args)
        {
            Console.Write("Enter directory path: ");
            string inputDir = Console.ReadLine();
            if (inputDir != "\\") {
                    dir = new DirectoryInfo(inputDir);
                    if (!dir.Exists) throw new ArgumentException("Incorrect directory");
            }
            Console.Write("Enter depth: ");
            globalDepth = (int)Int32.Parse(Console.ReadLine());
            
            string dirName;
            if(inputDir == "\\") dirName = "{Your computer}";
            else dirName = dir.Name;
            output += dirName + "\n";

            fileName = dirName.Replace(":", "").Replace("/", "").Replace("\\", "") + " - (" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ")";
            
            if(inputDir == "\\") printingFiles(0); 
            else printingFiles(dir, 0);

            
            Console.WriteLine(output);

            string output_file = Directory.GetCurrentDirectory()+"/"+fileName+".txt";

            StreamWriter sr = new StreamWriter(output_file);
            sr.WriteLine(output);
            sr.Close();
        }
    }
}
