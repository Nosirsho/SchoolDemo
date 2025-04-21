namespace School.Core.Model;

public class Lesson
{
    private Lesson(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    private Lesson(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Schedule? Schedule { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    public static Lesson Create(string name)
    {
        return new Lesson(name);
    }
    public static Lesson Create(Guid id, string name)
    {
        return new Lesson(id, name);
    }
}