namespace FileDirectoryCW
{
    class Program
    {
        // Task 2
        static void CopyFile()
        {
            Console.Write("Enter source file path :: ");
            string source = Console.ReadLine();
            Console.Write("Enter destination file path :: ");
            string dest = Console.ReadLine();

            try
            {
                File.Copy(source, dest, true);
                Console.WriteLine("File copied successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        // Task 3
        static void MoveFile()
        {
            Console.Write("Enter source file path :: ");
            string source = Console.ReadLine();
            Console.Write("Enter destination file path :: ");
            string dest = Console.ReadLine();

            try
            {
                File.Move(source, dest);
                Console.WriteLine("File moved successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        // Task 4
        static void CopyFolder()
        {
            Console.Write("Enter source folder path :: ");
            string source = Console.ReadLine();
            Console.Write("Enter destination folder path :: ");
            string dest = Console.ReadLine();

            try
            {
                Directory.CreateDirectory(dest);

                foreach (string file in Directory.GetFiles(source))
                {
                    string fileName = Path.GetFileName(file);
                    File.Copy(file, Path.Combine(dest, fileName), true);
                }

                foreach (string folder in Directory.GetDirectories(source))
                {
                    string folderName = Path.GetFileName(folder);
                    CopyFolderRecursive(folder, Path.Combine(dest, folderName));
                }

                Console.WriteLine("Folder copied successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        static void CopyFolderRecursive(string source, string dest)
        {
            Directory.CreateDirectory(dest);

            foreach (string file in Directory.GetFiles(source))
            {
                string fileName = Path.GetFileName(file);
                File.Copy(file, Path.Combine(dest, fileName), true);
            }

            foreach (string folder in Directory.GetDirectories(source))
            {
                string folderName = Path.GetFileName(folder);
                CopyFolderRecursive(folder, Path.Combine(dest, folderName));
            }
        }

        // Task 5
        static void MoveFolder()
        {
            Console.Write("Enter source folder path :: ");
            string source = Console.ReadLine();
            Console.Write("Enter destination folder path :: ");
            string dest = Console.ReadLine();

            try
            {
                Directory.Move(source, dest);
                Console.WriteLine("Folder moved successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        static void Main()
        {
            Console.WriteLine("1 - Copy file");
            Console.WriteLine("2 - Move file");
            Console.WriteLine("3 - Copy folder");
            Console.WriteLine("4 - Move folder");
            Console.Write("Choose :: ");
            string choice = Console.ReadLine();

            if (choice == "1") CopyFile();
            else if (choice == "2") MoveFile();
            else if (choice == "3") CopyFolder();
            else if (choice == "4") MoveFolder();
            else Console.WriteLine("Invalid choice");
        }
    }
}