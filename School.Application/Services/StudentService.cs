using School.Core.Model;
using School.Core.Stores;

namespace School.Application.Services;

public class StudentService
{
    private readonly IStudentStore _studentStore;

    public StudentService(IStudentStore studentStore)
    {
        _studentStore = studentStore;
    }

    public async Task<Student?> GetById(Guid id)
    {
        return await _studentStore.GetById(id);
    }
    
    public async Task<IReadOnlyList<Student>> GetByFullname(string fullname)
    {
        return await _studentStore.GetByFullname(fullname);
    }

    public async Task Create(Student student)
    {
        
        //Loginig
        //Validation
        var existingStudent = await _studentStore.GetById(student.Id);
        if (existingStudent != null)
        {
            throw new Exception($"Already existing student with id: {student.Id}");
        }
        await _studentStore.Add(student);
    }

    public async Task<Student> Update(Student student)
    {
         var curStudent = await _studentStore.Update(student);
         return curStudent;
    }

    public async Task<IReadOnlyList<Student>> GetAll()
    {
        var students = await _studentStore.GetAll();
        return students;
    }
}