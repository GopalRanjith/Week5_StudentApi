using Week5_StudentApi.Models;

namespace Week5_StudentApi.Services;

public interface ITeacherService
{
    List<Teacher> GetAllTeachers();

    Teacher? GetTeacherById(int id);

    void AddTeacher(Teacher teacher);

    bool UpdateTeacher(Teacher teacher);

    bool DeleteTeacher(int id);
}