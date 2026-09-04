using System.ComponentModel.DataAnnotations;

namespace Week5_StudentApi.DTOs;

public class StudentCreateDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Range(5, 100)]
    public int Age { get; set; }

    [Required]
    public string RollNumber { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}