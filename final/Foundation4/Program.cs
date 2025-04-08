using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>
        {
            new Running("03 Nov 2022", 30, 3.0),
            new Cycling("04 Nov 2022", 45, 15.0),
            new Swimming("05 Nov 2022", 40, 20)
        };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}

class Activity
{
    protected string date;
    protected int length; // in minutes

    public Activity(string date, int length)
    {
        this.date = date;
        this.length = length;
    }

    public virtual double GetDistance() => 0;
    public virtual double GetSpeed() => 0;
    public virtual double GetPace() => 0;

    public virtual string GetSummary()
    {
        return $"{date} Activity ({length} min)";
    }
}

class Running : Activity
{
    private double distance;

    public Running(string date, int length, double distance)
        : base(date, length)
    {
        this.distance = distance;
    }

    public override double GetDistance() => distance;
    public override double GetSpeed() => (distance / length) * 60;
    public override double GetPace() => length / distance;

    public override string GetSummary()
    {
        return $"{date} Running ({length} min): Distance {GetDistance()} miles, Speed {GetSpeed():0.0} mph, Pace: {GetPace():0.0} min per mile";
    }
}

class Cycling : Activity
{
    private double speed;

    public Cycling(string date, int length, double speed)
        : base(date, length)
    {
        this.speed = speed;
    }

    public override double GetDistance() => (speed * length) / 60;
    public override double GetSpeed() => speed;
    public override double GetPace() => 60 / speed;

    public override string GetSummary()
    {
        return $"{date} Cycling ({length} min): Distance {GetDistance():0.0} miles, Speed {speed} mph, Pace: {GetPace():0.0} min per mile";
    }
}

class Swimming : Activity
{
    private int laps;

    public Swimming(string date, int length, int laps)
        : base(date, length)
    {
        this.laps = laps;
    }

    public override double GetDistance()
    {
        return laps * 50 / 1000.0 * 0.62; // Convert to miles
    }

    public override double GetSpeed()
    {
        return (GetDistance() / length) * 60;
    }

    public override double GetPace()
    {
        return length / GetDistance();
    }

    public override string GetSummary()
    {
        return $"{date} Swimming ({length} min): Distance {GetDistance():0.0} miles, Speed {GetSpeed():0.0} mph, Pace: {GetPace():0.0} min per mile";
    }
}
