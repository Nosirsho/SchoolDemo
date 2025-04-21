namespace School.Core.Model;

public class GradeLevel
{
    private GradeLevel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    private GradeLevel(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    
    private GradeLevel(Guid id,string name, DateTime date)
    {
        Id = id;
        Name = name;
        EntryYear = date;
    }
    
    private GradeLevel(Guid id,string name, int? year)
    {
        Id = id;
        Name = name;
        EntryYear = (year != null)? new DateTime((int)year, 1, 1): null;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime? EntryYear { get; set; }
    public List<Student>? Students { get; set; }
    public Teacher? Teacher { get; set; }
    public Schedule? Schedule { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    public static GradeLevel Create(string name)
    {
        return new GradeLevel(name);
    }
    public static GradeLevel Create(Guid id, string name)
    {
        return new GradeLevel(id, name);
    }
    public static GradeLevel Create(Guid id,string name, DateTime date)
    {
        return new GradeLevel(id, name, date);
    }
    public static GradeLevel Create(Guid id,string name, int? year)
    {
        return new GradeLevel(id, name, year);
    }
    
}