namespace futblog.Models;
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navegación
    //public virtual ICollection<Post> Posts { get; set; }
}
