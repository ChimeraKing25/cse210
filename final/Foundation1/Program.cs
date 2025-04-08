using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create video list
        List<Video> videos = new List<Video>();

        // Video 1
        Video video1 = new Video("Top 10 Gadgets", "TechZone", 480);
        video1.AddComment(new Comment("Alice", "Great video!"));
        video1.AddComment(new Comment("Bob", "Love those gadgets."));
        video1.AddComment(new Comment("Charlie", "Where can I buy #3?"));
        videos.Add(video1);

        // Video 2
        Video video2 = new Video("How to Cook Pasta", "Chef Mario", 600);
        video2.AddComment(new Comment("Luna", "Yum! Trying this tonight."));
        video2.AddComment(new Comment("Max", "Simple and clear. Thanks!"));
        video2.AddComment(new Comment("Ella", "Love your accent."));
        videos.Add(video2);

        // Video 3
        Video video3 = new Video("DIY Bookshelf", "HandyManDan", 720);
        video3.AddComment(new Comment("Nina", "This was so helpful."));
        video3.AddComment(new Comment("Zack", "Looks easy enough!"));
        video3.AddComment(new Comment("Ivy", "Mine turned out great."));
        videos.Add(video3);

        // Display info for each video
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");

            foreach (Comment comment in video.Comments)
            {
                Console.WriteLine($" - {comment.Name}: {comment.Text}");
            }

            Console.WriteLine();
        }
    }
}

class Video
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int Length { get; private set; }
    public List<Comment> Comments { get; private set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }
}

class Comment
{
    public string Name { get; private set; }
    public string Text { get; private set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}
