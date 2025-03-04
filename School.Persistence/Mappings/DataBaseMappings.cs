using AutoMapper;
using School.Core.Constants;
using School.Core.Enums;
using School.Core.Model;
using School.Persistence.Entities;
using System.Collections.Generic;

namespace School.Persistence.Mappings;

public class DataBaseMappings : Profile
{
    public DataBaseMappings()
    {
        CreateMap<GradeLevelEntity, GradeLevel>();
        CreateMap<GradeLevelEntity, GradeLevel>().ReverseMap();
        CreateMap<ParentEntity, Parent>();
        CreateMap<ParentEntity, Parent>().ReverseMap();
        CreateMap<LessonEntity, Lesson>();
        CreateMap<LessonEntity, Lesson>().ReverseMap();
        CreateMap<StudentEntity, Student>();
        CreateMap<StudentEntity, Student>().ReverseMap();
        CreateMap<TeacherEntity, Teacher>();
        CreateMap<TeacherEntity, Teacher>().ReverseMap();
        CreateMap<GradeBookEntity, GradeBook>();
            //.ForMember(dest=>dest.StudentId, opt=>opt.MapFrom(src=>src.Student.Id));
        CreateMap<GradeBookEntity, GradeBook>().ReverseMap();
        CreateMap<SysSettingEntity, SysSetting>()
            .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.SysSettingTypeId));
        CreateMap<SysSettingEntity, SysSetting>().ReverseMap();
        CreateMap<SysSettingTypeEntity, SysSettingType>();
        CreateMap<SysSettingTypeEntity, SysSettingType>().ReverseMap();
        CreateMap<ScheduleEntity, Schedule>();
        CreateMap<ScheduleEntity, Schedule>().ReverseMap();
    }
}