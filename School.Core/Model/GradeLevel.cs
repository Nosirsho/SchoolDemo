namespace School.Core.Model;

public class GradeLevel
{
    public GradeLevel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    public GradeLevel(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    
    public GradeLevel(Guid id,string name, DateTime date)
    {
        Id = id;
        Name = name;
        EntryYear = date;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime? EntryYear { get; set; }
    public List<Student>? Students { get; set; }
    public Teacher? Teacher { get; set; }
}