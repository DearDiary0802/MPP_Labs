using System;

namespace MPP_Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            DynamicList<string> books = new DynamicList<string> { "1984", "Lolita", "Fahrenheit 451"};
            Console.WriteLine("Count: " + books.Count);

            books.Add("Animal Farm");
            books.Add("qwerty");
            Console.WriteLine("Count: " + books.Count);

            books.Remove("qwerty");
            foreach (string book in books)
            {
                Console.Write(book + "; ");
            }
            Console.WriteLine();

            books.RemoveAt(2);
            foreach (string book in books)
            {
                Console.Write(book + "; ");
            }
            Console.WriteLine();

            Console.WriteLine(books[2]);
            books[2] = "Three Comrades";
            Console.WriteLine(books[2]);

            books.Clear();
            Console.WriteLine("Count: " + books.Count);

            Console.ReadKey();
        }
    }
}
