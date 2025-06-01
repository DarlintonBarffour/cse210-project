using System;
using System.Collections.Generic;
using System.Linq;

// Enhancement: This version can be extended to load random scriptures from a file or library.

class Program
{
    static void Main()
    {
        // Example scripture
        Reference reference = new Reference("Proverbs", 3, 5, 6);
        string text = "Trust in the Lord with all thine heart and lean not unto thine own understanding.";
        Scripture scripture = new Scripture(reference, text);

        while (true)
        {
            scripture.Display();

            if (scripture.AllWordsHidden())
            {
                Console.WriteLine("\nAll words are hidden. Well done!");
                break;
            }

            Console.WriteLine("\nPress Enter to hide more words or type 'quit' to exit.");
            string input = Console.ReadLine();
            if (input.Trim().ToLower() == "quit")
                break;

            scripture.HideRandomWords();
        }
    }
}

class Reference
{
    public string Book { get; private set; }
    public int Chapter { get; private set; }
    public int StartVerse { get; private set; }
    public int? EndVerse { get; private set; }

    public Reference(string book, int chapter, int verse)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = verse;
        EndVerse = null;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        return EndVerse.HasValue
            ? $"{Book} {Chapter}:{StartVerse}-{EndVerse}"
            : $"{Book} {Chapter}:{StartVerse}";
    }
}

class Word
{
    public string Text { get; private set; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public override string ToString()
    {
        return IsHidden ? new string('_', Text.Length) : Text;
    }
}

class Scripture
{
    public Reference Reference { get; private set; }
    private List<Word> Words;

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ')
                    .Select(word => new Word(word))
                    .ToList();
    }

    public void HideRandomWords(int count = 3)
    {
        Random rand = new Random();
        var visibleWords = Words.Where(w => !w.IsHidden).ToList();

        for (int i = 0; i < count && visibleWords.Count > 0; i++)
        {
            int index = rand.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public bool AllWordsHidden()
    {
        return Words.All(w => w.IsHidden);
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine(Reference.ToString());
        Console.WriteLine();
        Console.WriteLine(string.Join(" ", Words));
    }
}
