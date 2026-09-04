namespace Week5_StudentApi.DTOs;

public class StudentReadDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }

    public string RollNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}