using Week5_StudentApi.Models;

namespace Week5_StudentApi.Services;

public interface IStudentService
{
    List<Student> GetAllStudents();
    Student? GetStudentById(int id);
    void AddStudent(Student student);
    bool UpdateStudent(Student student);
    bool DeleteStudent(int id);

    List<Student> SearchStudents(string name);
}