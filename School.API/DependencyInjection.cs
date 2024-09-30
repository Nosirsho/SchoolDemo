﻿using FluentValidation;
using School.API.Contracts.GradeLevel;
using School.API.Contracts.Lesson;
using School.API.Contracts.Parent;
using School.API.Contracts.Schedule;
using School.API.Contracts.Student;
using School.API.Contracts.Teacher;
using School.API.Validations.Schedule;
using School.API.Validations.GradeLevel;
using School.API.Validations.Lesson;
using School.API.Validations.Parent;
using School.API.Validations.Student;
using School.API.Validations.Teacher;
using School.Application.Services;
using School.Core.Stores;
using School.Persistence;
using School.Persistence.Repositories;

namespace School.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<SchoolDbContext>();
        
        services.AddScoped<StudentService>();
        services.AddScoped<ParentService>();
        services.AddScoped<TeacherService>();
        services.AddScoped<GradeLevelService>();
        services.AddScoped<LessonService>();
        services.AddScoped<ScheduleService>();
        
        services.AddScoped<IStudentStore, StudentRepository>();
        services.AddScoped<IParentStore, ParentRepository>();
        services.AddScoped<ITeacherStore, TeacherRepository>();
        services.AddScoped<IGradeLevelStore, GradeLevelRepository>();
        services.AddScoped<ILessonStore, LessonRepository>();
        services.AddScoped<IScheduleStore, ScheduleRepository>();
        
        services.AddScoped<IValidator<CreateStudentRequest>, CreateStudentValidator>();
        services.AddScoped<IValidator<UpdateStudentRequest>, UpdateStudentValidator>();
        services.AddScoped<IValidator<CreateTeacherRequest>, CreateTeacherValidator>();
        services.AddScoped<IValidator<UpdateTeacherRequest>, UpdateTeacherValidator>();
        services.AddScoped<IValidator<CreateParentRequest>, CreateParentValidator>();
        services.AddScoped<IValidator<UpdateParentRequest>, UpdateParentValidator>();
        services.AddScoped<IValidator<CreateGradeLevelRequest>, CreateGradeLevelValidator>();
        services.AddScoped<IValidator<UpdateGradeLevelRequest>, UpdateGradeLevelValidator>();
        services.AddScoped<IValidator<CreateLessonRequest>, CreateLessonValidator>();
        services.AddScoped<IValidator<UpdateLessonRequest>, UpdateLessonValidator>();
        services.AddScoped<IValidator<CreateScheduleRequest>, CreateScheduleValidator>();
        services.AddScoped<IValidator<UpdateScheduleRequest>, UpdateScheduleValidator>();
       
        return services;
    }
}