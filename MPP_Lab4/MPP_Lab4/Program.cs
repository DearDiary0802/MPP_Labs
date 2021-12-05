using System;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace MPP_Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathToDll;
            if (args.Length < 1)
            {
                pathToDll = "C:\\Users\\User\\Desktop\\СПП\\MPP_Labs\\DLL_Lab3\\DLL_Lab3\\bin\\Debug\\net5.0\\DLL_Lab3.dll";
            }
            else
            {
                pathToDll = args[0];
            }
            Task1();
            Task2(pathToDll);
            Console.ReadLine();
        }

        static void Task1()
        {
            const int ACTIONS_COUNT = 100;
            Action[] actions = new Action[ACTIONS_COUNT];
            for (int i = 0; i < ACTIONS_COUNT; i++)
            {
                actions[i] = Write;
            }

            Parallel.WaitAll(actions);

            Console.WriteLine("That's all");
        }

        static void Write()
        {
            Console.WriteLine($"The {Thread.CurrentThread.ManagedThreadId} thread is writing");
        }

        static void Task2(string pathToDll)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(pathToDll);
                var publicTypes = assembly.GetTypes().Where(type => ((type.IsPublic) || (type.IsNestedPublic)) && (type.GetCustomAttribute<ExportClassAttribute>() != null)).OrderBy(type => type.Namespace).ThenBy(type => type.FullName);
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
    }
}
