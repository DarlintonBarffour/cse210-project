using System;

class Program
{
    static void Main(string[] args)
    {
        //Promt the user for their first name
        Console.Write("What is your first name? ");
        string first = Console.ReadLine();

        //prompt the user for their last name
        Console.Write("What is your last name? ");
        string last = Console.ReadLine();

        // Print the user's name in the specified format
        Console.WriteLine($"Your name is {last}, {first} {last}");

          
    }
}