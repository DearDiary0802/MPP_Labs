using System;
using System.Collections.Generic;
using System.IO;

namespace MPP
{
    class Program
    {
        private static int copiedFilesCount = 0;
        private static int errorCount = 0;
        private static object _synch = new object();
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Incorrect input params");
                Console.WriteLine("The example of correct input params: ");
                Console.WriteLine("PathFrom PathTo ThreadsQuantity");
            }
            else
            {
                string pathFrom = args[0];
                string pathTo = args[1];
                int threadsQuantity = 0;
                int.TryParse(args[2], out threadsQuantity);

                bool isPathFromExists = Directory.Exists(pathFrom);
                bool isCorrectThreadsQuantity = threadsQuantity > 0;
                if (isPathFromExists && isCorrectThreadsQuantity)
                {
                    TaskQueue taskQueue = new TaskQueue(threadsQuantity);
                    bool isPathToExists = Directory.Exists(pathTo);
                    if (!isPathToExists)
                    {
                        Directory.CreateDirectory(pathTo);
                    }
                    List<string> filesList = new List<string>();
                    filesList.AddRange(Directory.GetFiles(pathFrom, "*", SearchOption.AllDirectories));
                    filesList.ForEach((fileItem) => {
                        string pathToWithFilename = pathTo + fileItem.Substring(fileItem.LastIndexOf('\\'), fileItem.Length - fileItem.LastIndexOf('\\'));
                        taskQueue.EnqueueTask(() => { FileCopy(pathToWithFilename, fileItem); }); 
                    });
                    taskQueue.Wait();
                    Console.Write("The number of copied files: ");
                    lock (_synch)
                    {
                        Console.WriteLine(copiedFilesCount);
                    }
                    Console.Write("The number of errors in coping files: ");
                    lock (_synch)
                    {
                        Console.WriteLine(errorCount);
                    }
            }
                else if (!isPathFromExists)
                {
                    Console.WriteLine("The path to copy from does not exist");
                }
            }
            Console.ReadLine();
        }

        private static void FileCopy(string pathTo, string fileItem)
        {
            try
            {
                File.Copy(fileItem, pathTo);
                lock (_synch)
                {
                    copiedFilesCount++;
                }
            }
            catch (Exception e) {
                lock (_synch)
                {
                    errorCount++;
                }
                Console.WriteLine("ERROR: " + e.Message);
            }
        }
    }
}
