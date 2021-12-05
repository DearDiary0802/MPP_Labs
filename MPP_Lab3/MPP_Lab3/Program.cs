using System;
using System.Linq;
using System.Reflection;

namespace MPP_Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Task1(args);
            Task2();
            Console.ReadLine();
        }

        static void Task1(string[] args)
        {
            string pathToDll;
            if (args.Length < 1)
            {
                pathToDll = "C:\\Users\\User\\Desktop\\СПП\\MPP_Labs\\DLL_Lab3\\DLL_Lab3\\bin\\Debug\\net5.0\\DLL_Lab3.dll";
                //pathToDll = "C:\\Users\\User\\Desktop\\СПП\\MPP_Labs\\EXE_Lab3\\EXE_Lab3\\bin\\Debug\\net5.0\\EXE_Lab3.dll";
            }
            else
            {
                pathToDll = args[0];
            }

            try
            {
                Assembly assembly = Assembly.LoadFrom(pathToDll);
                var publicTypes = assembly.GetTypes().Where(type => (type.IsPublic) || (type.IsNestedPublic)).
                    OrderBy(type => type.Namespace).ThenBy(type => type.FullName);
                int i = 1;
                foreach (var type in publicTypes)
                {
                    Console.WriteLine(i + ") Namespace: " + type.Namespace + " Full name: " + type.FullName);
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Task2()
        {
            LogBuffer logBuffer = new LogBuffer();

            logBuffer.Add("Low battery", TMessageType.Warning, DateTime.Now);
            logBuffer.Add("File doesn't exist", TMessageType.Error, DateTime.Now);
            logBuffer.Add("File has been appended", TMessageType.Info, DateTime.Now);
            logBuffer.Add("There is no electricity", TMessageType.Fatal, DateTime.Now);
        }
    }
}
