namespace futblog.Models;
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set; }

    // Navegación
    public virtual Category Category { get; set; }
    public virtual User User { get; set; }
}

public class PostDTO
{
    public string Title { get; set; }
    public string Body { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set; }
}