﻿using School.Core.Model;

namespace School.Core.Stores;

public interface IStudentStore
{
    public Task<Student?> GetById(Guid id);
    public Task<IReadOnlyList<Student>> GetByFullname(string fullname);
    
    Task<IReadOnlyList<Student>> Search(string text);
    
    public Task<IReadOnlyList<Student>> GetAll();
    public Task<IReadOnlyList<Student>> GetByGrade(Guid gradeId);
    public Task<Student> Update(Student student);
    Task Add(Student student); 
    Task<Guid> Delete(Guid id);
}