using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    // Base Goal Class
    public abstract class Goal
    {
        protected string _name;
        protected string _description;
        protected int _points;
        protected bool _isComplete;

        public Goal(string name, string description, int points)
        {
            _name = name;
            _description = description;
            _points = points;
            _isComplete = false;
        }

        public abstract int RecordEvent();
        public abstract string GetDetailsString();
        public abstract string GetStringRepresentation();

        public bool IsComplete() => _isComplete;
    }

    // Simple Goal (one-time completion)
    public class SimpleGoal : Goal
    {
        public SimpleGoal(string name, string description, int points) 
            : base(name, description, points) { }

        public override int RecordEvent()
        {
            _isComplete = true;
            return _points;
        }

        public override string GetDetailsString() => 
            $"[{(_isComplete ? "X" : " ")}] {_name}: {_description}";

        public override string GetStringRepresentation() => 
            $"SimpleGoal|{_name}|{_description}|{_points}|{_isComplete}";
    }

    // Eternal Goal (never completes)
    public class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points) 
            : base(name, description, points) { }

        public override int RecordEvent() => _points;

        public override string GetDetailsString() => 
            $"[ ] {_name}: {_description} (eternal)";

        public override string GetStringRepresentation() => 
            $"EternalGoal|{_name}|{_description}|{_points}";
    }

    // Checklist Goal (requires multiple completions)
    public class ChecklistGoal : Goal
    {
        private int _bonusPoints;
        private int _targetCount;
        private int _currentCount;

        public ChecklistGoal(string name, string description, int points, 
                            int targetCount, int bonusPoints) 
            : base(name, description, points)
        {
            _bonusPoints = bonusPoints;
            _targetCount = targetCount;
            _currentCount = 0;
        }

        public override int RecordEvent()
        {
            _currentCount++;
            if (_currentCount == _targetCount)
            {
                _isComplete = true;
                return _points + _bonusPoints;
            }
            return _points;
        }

        public override string GetDetailsString() => 
            $"[{(_isComplete ? "X" : " ")}] {_name}: {_description} " +
            $"(Completed {_currentCount}/{_targetCount})";

        public override string GetStringRepresentation() => 
            $"ChecklistGoal|{_name}|{_description}|{_points}|" +
            $"{_targetCount}|{_bonusPoints}|{_currentCount}";
    }

    // Manages all goals and score
    public class GoalManager
    {
        private List<Goal> _goals;
        private int _score;

        public GoalManager()
        {
            _goals = new List<Goal>();
            _score = 0;
        }

        public void AddGoal(Goal goal) => _goals.Add(goal);

        public void RecordGoalCompletion(int index)
        {
            if (index >= 0 && index < _goals.Count)
            {
                _score += _goals[index].RecordEvent();
                Console.WriteLine($"Recorded! You earned {_goals[index].RecordEvent()} points.");
            }
        }

        public void DisplayGoals()
        {
            Console.WriteLine("\nYour Goals:");
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
            }
        }

        public void DisplayScore() => Console.WriteLine($"\nCurrent Score: {_score}");

        public void SaveGoals(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(_score);
                foreach (Goal goal in _goals)
                {
                    writer.WriteLine(goal.GetStringRepresentation());
                }
            }
            Console.WriteLine($"Goals saved to {filename}!");
        }

        public void LoadGoals(string filename)
        {
            if (File.Exists(filename))
            {
                string[] lines = File.ReadAllLines(filename);
                _score = int.Parse(lines[0]);
                _goals.Clear();

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split('|');
                    switch (parts[0])
                    {
                        case "SimpleGoal":
                            var simpleGoal = new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]));
                            if (parts.Length > 4) simpleGoal.IsComplete(); // Handle completion status
                            _goals.Add(simpleGoal);
                            break;
                        case "EternalGoal":
                            _goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
                            break;
                        case "ChecklistGoal":
                            _goals.Add(new ChecklistGoal(
                                parts[1], parts[2], int.Parse(parts[3]),
                                int.Parse(parts[4]), int.Parse(parts[5])));
                            break;
                    }
                }
                Console.WriteLine($"Goals loaded from {filename}!");
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }

        internal int GetGoalsCount()
        {
            throw new NotImplementedException();
        }
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            GoalManager manager = new GoalManager();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== Eternal Quest - Goal Tracker ===");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. Record Goal Completion");
                Console.WriteLine("3. View Goals");
                Console.WriteLine("4. View Score");
                Console.WriteLine("5. Save Goals");
                Console.WriteLine("6. Load Goals");
                Console.WriteLine("7. Exit");

                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateNewGoal(manager);
                        break;
                    case "2":
                        RecordGoalCompletion(manager);
                        break;
                    case "3":
                        manager.DisplayGoals();
                        break;
                    case "4":
                        manager.DisplayScore();
                        break;
                    case "5":
                        Console.Write("Enter filename to save: ");
                        manager.SaveGoals(Console.ReadLine());
                        break;
                    case "6":
                        Console.Write("Enter filename to load: ");
                        manager.LoadGoals(Console.ReadLine());
                        break;
                    case "7":
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        static void CreateNewGoal(GoalManager manager)
        {
            Console.WriteLine("\nSelect Goal Type:");
            Console.WriteLine("1. Simple Goal (one-time)");
            Console.WriteLine("2. Eternal Goal (repeating)");
            Console.WriteLine("3. Checklist Goal (multiple times)");

            string typeChoice = Console.ReadLine();
            Console.Write("Enter goal name: ");
            string name = Console.ReadLine();
            Console.Write("Enter goal description: ");
            string description = Console.ReadLine();
            Console.Write("Enter points: ");
            int points = int.Parse(Console.ReadLine());

            switch (typeChoice)
            {
                case "1":
                    manager.AddGoal(new SimpleGoal(name, description, points));
                    Console.WriteLine("Simple goal added!");
                    break;
                case "2":
                    manager.AddGoal(new EternalGoal(name, description, points));
                    Console.WriteLine("Eternal goal added!");
                    break;
                case "3":
                {
                    Console.Write("Enter target count: ");
                    int target = int.Parse(Console.ReadLine());
                    Console.Write("Enter bonus points: ");
                    int bonus = int.Parse(Console.ReadLine());
                    manager.AddGoal(new ChecklistGoal(name, description, points, target, bonus));
                    Console.WriteLine("Checklist goal added!");
                    break;
                }
                default:
                    Console.WriteLine("Invalid goal type.");
                    break;
            }
        }

        static void RecordGoalCompletion(GoalManager manager)
        {
            manager.DisplayGoals();
            if (manager.GetGoalsCount() == 0) return;

            Console.Write("Enter goal number to record: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                manager.RecordGoalCompletion(index - 1);
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }
}