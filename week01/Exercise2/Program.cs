using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("What is your grade percentage? ");

        string answer = Console.ReadLine();

        int percent = int.Parse(answer);

        string letter = "";

        //prompt the user about the if statement

        if (percent >= 90)
        {
            letter = "A";
        }

        else if (percent >= 80)
        {
            letter = "B";
        }

        else if (percent >= 70)
        {
            letter = "c";
        }

        else if (percent >= 60)
        {
            letter = "D";
        }

        else
        {
            letter = "F";
        }

        //prompt the user to print program.

        Console.WriteLine($"Your grade is: {letter}");

        if (percent >= 70)
        {
            Console.WriteLine("You passed!");
        }

        else
        {
            Console.WriteLine("Better luck next time! ");
        }
    }
}