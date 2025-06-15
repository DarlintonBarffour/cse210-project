using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessApp
{
    // Base Activity Class
    public abstract class Activity
    {
        protected string _name;
        protected string _description;
        protected int _duration;
        protected static int _totalActivitiesCompleted;
        protected static Dictionary<string, int> _activityCounts = new Dictionary<string, int>();

        protected void DisplayStartingMessage()
        {
            Console.Clear();
            Console.WriteLine($"Welcome to the {_name} Activity.");
            Console.WriteLine(_description);
            Console.Write("How long, in seconds, would you like for your session? ");
            _duration = int.Parse(Console.ReadLine());
            
            Console.WriteLine("Get ready...");
            ShowSpinner(3);
        }

        protected void DisplayEndingMessage()
        {
            Console.WriteLine("\nWell done!!");
            ShowSpinner(3);
            Console.WriteLine($"You have completed another {_duration} seconds of the {_name} Activity.");
            ShowSpinner(3);
            
            // Log the activity
            LogActivity();
        }

        protected void ShowSpinner(int seconds)
        {
            for (int i = 0; i < seconds * 2; i++)
            {
                Console.Write("/");
                Thread.Sleep(250);
                Console.Write("\b \b");
                Console.Write("-");
                Thread.Sleep(250);
                Console.Write("\b \b");
                Console.Write("\\");
                Thread.Sleep(250);
                Console.Write("\b \b");
                Console.Write("|");
                Thread.Sleep(250);
                Console.Write("\b \b");
            }
        }

        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }

        protected void ShowBreathingAnimation(int seconds, bool isInhale)
        {
            int steps = 20;
            for (int i = 1; i <= steps; i++)
            {
                Console.Clear();
                string animation = isInhale 
                    ? new string('O', i) 
                    : new string('O', steps - i + 1);
                
                Console.WriteLine(isInhale ? "Breathe in..." : "Breathe out...");
                Console.WriteLine(animation);
                Thread.Sleep(seconds * 1000 / steps);
            }
        }

        protected void LogActivity()
        {
            _totalActivitiesCompleted++;
            if (_activityCounts.ContainsKey(_name))
            {
                _activityCounts[_name]++;
            }
            else
            {
                _activityCounts.Add(_name, 1);
            }
        }

        public static void DisplayActivityLog()
        {
            Console.WriteLine("\nActivity Statistics:");
            Console.WriteLine($"Total activities completed: {_totalActivitiesCompleted}");
            foreach (var kvp in _activityCounts)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value} times");
            }
            Console.WriteLine();
            new Activity().ShowSpinner(3);
        }

        public abstract void Run();
    }

    // Breathing Activity
    public class BreathingActivity : Activity
    {
        public BreathingActivity()
        {
            _name = "Breathing";
            _description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
        }

        public override void Run()
        {
            DisplayStartingMessage();
            
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(_duration);
            
            while (DateTime.Now < endTime)
            {
                Console.Write("\nBreathe in...");
                ShowCountdown(4);
                Console.Write("\nBreathe out...");
                ShowCountdown(6);
                
                // Alternative breathing animation (uncomment to use)
                // ShowBreathingAnimation(4, true);
                // ShowBreathingAnimation(6, false);
            }
            
            DisplayEndingMessage();
        }
    }

    // Reflection Activity
    public class ReflectionActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };
        
        private List<string> _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };
        
        private List<string> _usedQuestions = new List<string>();

        public ReflectionActivity()
        {
            _name = "Reflection";
            _description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
        }

        public override void Run()
        {
            DisplayStartingMessage();
            
            Console.WriteLine("\nConsider the following prompt:");
            Console.WriteLine($"--- {GetRandomPrompt()} ---");
            Console.WriteLine("\nWhen you have something in mind, press enter to continue.");
            Console.ReadLine();
            
            Console.WriteLine("Now ponder on each of the following questions as they relate to this experience.");
            Console.Write("You may begin in: ");
            ShowCountdown(5);
            
            Console.Clear();
            
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(_duration);
            
            while (DateTime.Now < endTime)
            {
                Console.Write($"\n{GetRandomQuestion()} ");
                ShowSpinner(5);
            }
            
            DisplayEndingMessage();
        }

        private string GetRandomPrompt()
        {
            Random random = new Random();
            return _prompts[random.Next(_prompts.Count)];
        }

        private string GetRandomQuestion()
        {
            if (_usedQuestions.Count == _questions.Count)
            {
                _usedQuestions.Clear();
            }
            
            Random random = new Random();
            string question;
            do {
                question = _questions[random.Next(_questions.Count)];
            } while (_usedQuestions.Contains(question));
            
            _usedQuestions.Add(question);
            return question;
        }
    }

    // Listing Activity
    public class ListingActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };
        
        private int _count;

        public ListingActivity()
        {
            _name = "Listing";
            _description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
        }

        public override void Run()
        {
            DisplayStartingMessage();
            
            Console.WriteLine("\nList as many responses as you can to the following prompt:");
            Console.WriteLine($"--- {GetRandomPrompt()} ---");
            Console.Write("You may begin in: ");
            ShowCountdown(5);
            
            List<string> items = GetListFromUser();
            _count = items.Count;
            
            Console.WriteLine($"\nYou listed {_count} items!");
            
            DisplayEndingMessage();
        }

        private string GetRandomPrompt()
        {
            Random random = new Random();
            return _prompts[random.Next(_prompts.Count)];
        }

        private List<string> GetListFromUser()
        {
            List<string> items = new List<string>();
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(_duration);
            
            Console.WriteLine();
            while (DateTime.Now < endTime)
            {
                Console.Write("> ");
                items.Add(Console.ReadLine());
            }
            
            return items;
        }
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Activities Menu:");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. View Activity Statistics");
                Console.WriteLine("5. Quit");
                Console.Write("Select a choice from the menu: ");
                
                string choice = Console.ReadLine();
                
                Activity activity = null;
                
                switch (choice)
                {
                    case "1":
                        activity = new BreathingActivity();
                        break;
                    case "2":
                        activity = new ReflectionActivity();
                        break;
                    case "3":
                        activity = new ListingActivity();
                        break;
                    case "4":
                        Activity.DisplayActivityLog();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    case "5":
                        Console.WriteLine("Thank you for using the Mindfulness Program. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Thread.Sleep(1000);
                        continue;
                }
                
                activity.Run();
            }
        }
    }
}