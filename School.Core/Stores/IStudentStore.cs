using School.Core.Model;

namespace School.Core.Stores;

public interface IStudentStore
{
    public Task<Student?> GetById(Guid id);
    public Task<IReadOnlyList<Student>> GetByFullname(string fullname);
    
    public Task<IReadOnlyList<Student>> GetAll();
    public Task<Student> Update(Student student);
    Task Add(Student student); 
}