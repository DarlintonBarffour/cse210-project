using System;

class Program
{
    static void Main()
    {
        // Step 1: Generate a random magic number
        Random random = new Random();
        int magicNumber = random.Next(1, 101);

        int guess;
        do
        {
            // Step 2: User makes a guess
            Console.Write("Enter your guess: ");
            guess = int.Parse(Console.ReadLine());

            // Step 3: Provide feedback
            if (guess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (guess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }
        } while (guess != magicNumber);
    }
}
