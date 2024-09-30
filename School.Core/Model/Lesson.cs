namespace School.Core.Model;

public class Lesson
{
    public Lesson(){}
    public Lesson(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    public Lesson(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Schedule? Schedule { get; set; }
}