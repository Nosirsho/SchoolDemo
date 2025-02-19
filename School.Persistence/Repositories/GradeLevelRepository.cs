using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School.Core.Model;
using School.Core.Stores;
using School.Persistence.Entities;

namespace School.Persistence.Repositories;

public class GradeLevelRepository : IGradeLevelStore
{
    private readonly IMapper _mapper;
    private readonly SchoolDbContext _schoolDbContext;

    public GradeLevelRepository(SchoolDbContext schoolDbContex, IMapper mapper)
    {
        _mapper = mapper;
        _schoolDbContext = schoolDbContex;
    }
    public async Task<GradeLevel?> GetById(Guid id)
    {
        var entity = await _schoolDbContext.GradeLevels.FindAsync(id);
        return _mapper.Map<GradeLevel>(entity);
    }

    public async Task<IReadOnlyList<GradeLevel>> GetAll()
    {
        var response =await _schoolDbContext.GradeLevels.ToListAsync();
        return _mapper.Map<List<GradeLevel>>(response) ;
    }

    public async Task<GradeLevel> Update(GradeLevel gradeLevel)
    {
        var curGradeLevel = await _schoolDbContext.GradeLevels.FindAsync(gradeLevel.Id);
        if (curGradeLevel==null) throw new NullReferenceException("GradeLevel not found");
        
        curGradeLevel.Id = gradeLevel.Id;
        curGradeLevel.Name = gradeLevel.Name;
        curGradeLevel.EntryYear = gradeLevel.EntryYear;
        await _schoolDbContext.SaveChangesAsync();
        return _mapper.Map<GradeLevel>(curGradeLevel);
    }

    public async Task Add(GradeLevel gradeLevel)
    {
        var gradeLevelEmtity = _mapper.Map<GradeLevelEntity>(gradeLevel);
        await _schoolDbContext.GradeLevels.AddAsync(gradeLevelEmtity);
        await _schoolDbContext.SaveChangesAsync();
    }
}