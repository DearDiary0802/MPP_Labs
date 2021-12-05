using System;
using System.Diagnostics;
using System.Threading;

namespace MPP_Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Mutex mutex = new Mutex();
            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(() =>
                {
                    mutex.Lock();
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} thread is writing");
                    try
                    {
                        mutex.Unlock();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });
                thread.Start();
            }

            Process doc = new Process();
            doc.StartInfo.FileName = "C:\\Users\\User\\Desktop\\СПП\\MPP_Labs\\MPP_Lab1\\MPP\\bin\\Debug\\MPP.exe";
            doc.Start();

            Console.ReadLine();
            OSHandle handle = new OSHandle(doc.Handle);
            handle.Dispose();

            doc.Kill();
            Console.ReadLine();
        }
    }
}
