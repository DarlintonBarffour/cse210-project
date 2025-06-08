using System;
using System.Collections.Generic;

class Comment
{
    private string _commenterName;
    private string _commentText;

    public Comment(string commenterName, string commentText)
    {
        _commenterName = commenterName;
        _commentText = commentText;
    }

    public string GetCommenterName()
    {
        return _commenterName;
    }

    public string GetCommentText()
    {
        return _commentText;
    }
}

class Video
{
    private string _title;
    private string _author;
    private int _length;
    private List<Comment> _comments;

    public Video(string title, string author, int length)
    {
        _title = title;
        _author = author;
        _length = length;
        _comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return _comments.Count;
    }

    public string GetTitle()
    {
        return _title;
    }

    public string GetAuthor()
    {
        return _author;
    }

    public int GetLength()
    {
        return _length;
    }

    public List<Comment> GetComments()
    {
        return _comments;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Video 1
        Video video1 = new Video("Exploring the Ocean", "SeaDoc", 300);
        video1.AddComment(new Comment("Alice", "Amazing footage!"));
        video1.AddComment(new Comment("Bob", "I learned so much."));
        video1.AddComment(new Comment("Charlie", "I want to go diving now!"));
        videos.Add(video1);

        // Video 2
        Video video2 = new Video("How to Bake Bread", "ChefTime", 480);
        video2.AddComment(new Comment("Dana", "Looks delicious."));
        video2.AddComment(new Comment("Eli", "Tried it and it worked perfectly."));
        video2.AddComment(new Comment("Frank", "Can you do sourdough next?"));
        videos.Add(video2);

        // Video 3
        Video video3 = new Video("Guitar Basics", "StringMaster", 600);
        video3.AddComment(new Comment("Grace", "Very helpful tutorial."));
        video3.AddComment(new Comment("Hank", "Finally learned how to tune!"));
        video3.AddComment(new Comment("Ivy", "Can you cover barre chords next?"));
        videos.Add(video3);

        // Display video details
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length: {video.GetLength()} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"  {comment.GetCommenterName()}: {comment.GetCommentText()}");
            }

            Console.WriteLine(); // Blank line between videos
        }
    }
}
