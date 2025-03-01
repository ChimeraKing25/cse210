using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(new ScriptureReference("John", 3, 16),
                "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."),
            
            new Scripture(new ScriptureReference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.")
        };

        ScriptureMemorizer memorizer = new ScriptureMemorizer(scriptures);
        memorizer.Start();
    }
}

class ScriptureReference
{
    public string Book { get; }
    public int Chapter { get; }
    public int VerseStart { get; }
    public int? VerseEnd { get; }

    public ScriptureReference(string book, int chapter, int verseStart, int? verseEnd = null)
    {
        Book = book;
        Chapter = chapter;
        VerseStart = verseStart;
        VerseEnd = verseEnd;
    }

    public override string ToString()
    {
        return VerseEnd.HasValue 
            ? $"{Book} {Chapter}:{VerseStart}-{VerseEnd}" 
            : $"{Book} {Chapter}:{VerseStart}";
    }
}

class Word
{
    public string Text { get; }
    public bool Hidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        Hidden = false;
    }

    public void Hide()
    {
        Hidden = true;
    }

    public override string ToString()
    {
        return Hidden ? new string('_', Text.Length) : Text;
    }
}

class Scripture
{
    public ScriptureReference Reference { get; }
    private List<Word> Words { get; }

    public Scripture(ScriptureReference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine(Reference);
        Console.WriteLine(string.Join(" ", Words));
    }

    public bool HideRandomWords(int count = 2)
    {
        var visibleWords = Words.Where(word => !word.Hidden).ToList();
        if (visibleWords.Count == 0)
            return false;

        Random random = new Random();
        foreach (var word in visibleWords.OrderBy(x => random.Next()).Take(count))
        {
            word.Hide();
        }
        return true;
    }

    public bool IsFullyHidden()
    {
        return Words.All(word => word.Hidden);
    }
}

class ScriptureMemorizer
{
    private readonly List<Scripture> _scriptures;
    private readonly Random _random = new Random();

    public ScriptureMemorizer(List<Scripture> scriptures)
    {
        _scriptures = scriptures;
    }

    public void Start()
    {
        Scripture scripture = _scriptures[_random.Next(_scriptures.Count)];
        scripture.Display();

        while (!scripture.IsFullyHidden())
        {
            Console.Write("Press Enter to hide words or type 'quit' to exit: ");
            string input = Console.ReadLine()?.Trim().ToLower();

            if (input == "quit")
                break;

            scripture.HideRandomWords();
            scripture.Display();
        }

        Console.WriteLine("Well done! You have memorized the scripture.");
    }
}
