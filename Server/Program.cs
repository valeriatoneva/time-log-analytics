using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Wow, it's working...");
        var initializer = new DatabaseInitializer();
        initializer.Initialize();

        Console.WriteLine("DB is working too...wow");
    }
}
