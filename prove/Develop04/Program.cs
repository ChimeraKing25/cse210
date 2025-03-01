using System;
using System.Collections.Generic;
using System.Threading;

abstract class MindfulnessActivity
{
    protected int duration;
    
    public void Start()
    {
        Console.Clear();
        DisplayStartingMessage();
        PrepareToBegin();
        PerformActivity();
        DisplayEndingMessage();
    }
    
    protected virtual void DisplayStartingMessage()
    {
        Console.WriteLine("Welcome to the Mindfulness Activity!");
        Console.Write("Enter duration in seconds: ");
        duration = int.Parse(Console.ReadLine());
    }
    
    protected void PrepareToBegin()
    {
        Console.WriteLine("Get ready...");
        ShowCountdown(3);
    }
    
    protected abstract void PerformActivity();
    
    protected virtual void DisplayEndingMessage()
    {
        Console.WriteLine("Great job! You have completed the activity.");
        Console.WriteLine($"You spent {duration} seconds in this activity.");
        ShowCountdown(3);
    }
    
    protected void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

class BreathingActivity : MindfulnessActivity
{
    protected override void DisplayStartingMessage()
    {
        Console.WriteLine("Breathing Activity: This activity will help you relax by guiding deep breathing.");
        base.DisplayStartingMessage();
    }
    
    protected override void PerformActivity()
    {
        int elapsed = 0;
        while (elapsed < duration)
        {
            Console.WriteLine("Breathe in...");
            ShowCountdown(3);
            Console.WriteLine("Breathe out...");
            ShowCountdown(3);
            elapsed += 6;
        }
    }
}

class ReflectionActivity : MindfulnessActivity
{
    private static readonly List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };
    
    private static readonly List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?"
    };
    
    protected override void DisplayStartingMessage()
    {
        Console.WriteLine("Reflection Activity: Think deeply about positive moments in your life.");
        base.DisplayStartingMessage();
    }
    
    protected override void PerformActivity()
    {
        Random rand = new Random();
        Console.WriteLine(prompts[rand.Next(prompts.Count)]);
        ShowCountdown(3);
        
        int elapsed = 0;
        while (elapsed < duration)
        {
            Console.WriteLine(questions[rand.Next(questions.Count)]);
            ShowCountdown(5);
            elapsed += 5;
        }
    }
}

class ListingActivity : MindfulnessActivity
{
    private static readonly List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?"
    };
    
    protected override void DisplayStartingMessage()
    {
        Console.WriteLine("Listing Activity: List as many things as possible in a positive area.");
        base.DisplayStartingMessage();
    }
    
    protected override void PerformActivity()
    {
        Random rand = new Random();
        Console.WriteLine(prompts[rand.Next(prompts.Count)]);
        ShowCountdown(3);
        
        int count = 0;
        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            Console.Write("Enter an item: ");
            Console.ReadLine();
            count++;
        }
        Console.WriteLine($"You listed {count} items!");
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Activities");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            
            string choice = Console.ReadLine();
            MindfulnessActivity activity = choice switch
            {
                "1" => new BreathingActivity(),
                "2" => new ReflectionActivity(),
                "3" => new ListingActivity(),
                "4" => null,
                _ => throw new InvalidOperationException("Invalid choice.")
            };
            
            if (activity == null)
                break;
            
            activity.Start();
        }
    }
}
