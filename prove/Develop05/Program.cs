using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetStatus();
    public abstract string SaveData();
    public string GetName() => _name;
}

class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points, bool isComplete = false) : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return _points;
        }
        return 0;
    }

    public override bool IsComplete() => _isComplete;
    public override string GetStatus() => _isComplete ? "[X]" : "[ ]";
    public override string SaveData() => $"Simple|{_name}|{_description}|{_points}|{_isComplete}";
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points) { }
    public override int RecordEvent() => _points;
    public override bool IsComplete() => false;
    public override string GetStatus() => "[~]";
    public override string SaveData() => $"Eternal|{_name}|{_description}|{_points}";
}

class ChecklistGoal : Goal
{
    private int _timesCompleted;
    private int _targetCount;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus, int timesCompleted = 0)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _bonus = bonus;
        _timesCompleted = timesCompleted;
    }

    public override int RecordEvent()
    {
        _timesCompleted++;
        if (_timesCompleted == _targetCount)
            return _points + _bonus;
        else if (_timesCompleted < _targetCount)
            return _points;
        return 0;
    }

    public override bool IsComplete() => _timesCompleted >= _targetCount;
    public override string GetStatus() => IsComplete() ? $"[X] Completed {_timesCompleted}/{_targetCount}" : $"[ ] Completed {_timesCompleted}/{_targetCount}";
    public override string SaveData() => $"Checklist|{_name}|{_description}|{_points}|{_targetCount}|{_bonus}|{_timesCompleted}";
}

class EternalQuest
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void Run()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\nEternal Quest Menu:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Display Score");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Load Goals");
            Console.WriteLine("7. Quit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": CreateGoal(); break;
                case "2": ListGoals(); break;
                case "3": RecordEvent(); break;
                case "4": Console.WriteLine($"\nCurrent Score: {_score}"); break;
                case "5": SaveGoals(); break;
                case "6": LoadGoals(); break;
                case "7": running = false; break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    private void CreateGoal()
    {
        Console.WriteLine("Select goal type: 1. Simple 2. Eternal 3. Checklist");
        string type = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter description: ");
        string desc = Console.ReadLine();
        Console.Write("Enter points: ");
        int points = int.Parse(Console.ReadLine());

        switch (type)
        {
            case "1":
                _goals.Add(new SimpleGoal(name, desc, points));
                break;
            case "2":
                _goals.Add(new EternalGoal(name, desc, points));
                break;
            case "3":
                Console.Write("Enter number of completions: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus: ");
                int bonus = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                break;
        }
    }

    private void ListGoals()
    {
        Console.WriteLine("\nYour Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetStatus()} {_goals[i].GetName()}");
        }
    }

    private void RecordEvent()
    {
        ListGoals();
        Console.Write("Select goal number to record: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index >= 0 && index < _goals.Count)
        {
            int earned = _goals[index].RecordEvent();
            _score += earned;
            Console.WriteLine($"You earned {earned} points!");
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }

    private void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            writer.WriteLine(_score);
            foreach (Goal goal in _goals)
            {
                writer.WriteLine(goal.SaveData());
            }
        }
        Console.WriteLine("Goals saved.");
    }

    private void LoadGoals()
    {
        if (!File.Exists("goals.txt"))
        {
            Console.WriteLine("No saved file found.");
            return;
        }

        _goals.Clear();
        string[] lines = File.ReadAllLines("goals.txt");
        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');
            switch (parts[0])
            {
                case "Simple":
                    _goals.Add(new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]), bool.Parse(parts[4])));
                    break;
                case "Eternal":
                    _goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
                    break;
                case "Checklist":
                    _goals.Add(new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6])));
                    break;
            }
        }
        Console.WriteLine("Goals loaded.");
    }

    static void Main(string[] args)
    {
        new EternalQuest().Run();
    }
}
