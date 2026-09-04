using Week5_StudentApi.Data;
using Week5_StudentApi.Models;

namespace Week5_StudentApi.Services;

public class StudentService : IStudentService
{
    private readonly IRepository<Student> repository;

    public StudentService(IRepository<Student> repository)
    {
        this.repository = repository;
    }

    public List<Student> GetAllStudents()
    {
        return repository.GetAll();
    }

    public Student? GetStudentById(int id)
    {
        return repository.GetById(id);
    }

    public void AddStudent(Student student)
    {
        repository.Add(student);
    }

    public bool UpdateStudent(Student student)
    {
        var existingStudent = repository.GetById(student.Id);

        if (existingStudent == null)
        {
            return false;
        }

        repository.Update(student);
        return true;
    }

    public bool DeleteStudent(int id)
    {
        var existingStudent = repository.GetById(id);

        if (existingStudent == null)
        {
            return false;
        }

        repository.Delete(id);
        return true;
    }
    public List<Student> SearchStudents(string name)
    {
        return repository
            .GetAll()
            .Where(student =>
                student.Name.Contains(
                    name,
                    StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}