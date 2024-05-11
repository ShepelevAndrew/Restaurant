namespace Restaurant.Domain.Users.Entities;

public class Permission
{
    public Permission(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }
}