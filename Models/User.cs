namespace futblog.Models;
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }

    // Navegación
    public virtual ICollection<Post> Posts { get; set; }
}

public class UserRegisterDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}


public class UserLoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

