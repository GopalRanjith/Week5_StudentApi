using Week5_StudentApi.Data;
using Week5_StudentApi.Models;

namespace Week5_StudentApi.Services;

public class TeacherService : ITeacherService
{
    private readonly IRepository<Teacher> repository;

    public TeacherService(IRepository<Teacher> repository)
    {
        this.repository = repository;
    }

    public List<Teacher> GetAllTeachers()
    {
        return repository.GetAll();
    }

    public Teacher? GetTeacherById(int id)
    {
        return repository.GetById(id);
    }

    public void AddTeacher(Teacher teacher)
    {
        repository.Add(teacher);
    }

    public bool UpdateTeacher(Teacher teacher)
    {
        var existingTeacher = repository.GetById(teacher.Id);

        if (existingTeacher == null)
        {
            return false;
        }

        repository.Update(teacher);

        return true;
    }

    public bool DeleteTeacher(int id)
    {
        var existingTeacher = repository.GetById(id);

        if (existingTeacher == null)
        {
            return false;
        }

        repository.Delete(id);

        return true;
    }
}