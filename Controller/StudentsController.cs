using Microsoft.AspNetCore.Mvc;
using Week5_StudentApi.DTOs;
using Week5_StudentApi.Models;
using Week5_StudentApi.Services;
using Week5_StudentApi.Strategies;

namespace Week5_StudentApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService studentService;

    private readonly ILogger<StudentsController> logger;

    public StudentsController(
        IStudentService studentService,
        ILogger<StudentsController> logger)
    {
        this.studentService = studentService;
        this.logger = logger;
    }

    [HttpGet]
    public IActionResult GetAllStudents()
    {
        var students = studentService.GetAllStudents();

        var studentResponses = students.Select(student => new StudentReadDto
        {
            Id = student.Id,
            Name = student.Name,
            Age = student.Age,
            RollNumber = student.RollNumber,
            Email = student.Email
        }).ToList();

        return Ok(studentResponses);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        var student = studentService.GetStudentById(id);

        if (student == null)
        {
            logger.LogWarning(
                "Student not found. StudentId: {StudentId}",
                id);

            return NotFound();
        }
        var studentResponse = new StudentReadDto
        {
            Id = student.Id,
            Name = student.Name,
            Age = student.Age,
            RollNumber = student.RollNumber,
            Email = student.Email
        };

        return Ok(studentResponse);
    }

    [HttpPost]
    public IActionResult AddStudent(StudentCreateDto studentRequest)
    {
        var studentEntity = new Student
        {
            Id = DateTime.Now.Millisecond,
            Name = studentRequest.Name,
            Age = studentRequest.Age,
            RollNumber = studentRequest.RollNumber,
            Email = studentRequest.Email
        };

        studentService.AddStudent(studentEntity);
        logger.LogInformation(
    "Student created successfully. StudentId: {StudentId}",
    studentEntity.Id);

        var studentResponse = new StudentReadDto
        {
            Id = studentEntity.Id,
            Name = studentEntity.Name,
            Age = studentEntity.Age,
            RollNumber = studentEntity.RollNumber,
            Email = studentEntity.Email
        };

        return CreatedAtAction(
            nameof(GetStudentById),
            new { id = studentResponse.Id },
            studentResponse);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStudent(int id, Student student)
    {
        student.Id = id;

        var updated = studentService.UpdateStudent(student);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        var deleted = studentService.DeleteStudent(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
    [HttpGet("grade")]
    public IActionResult CalculateGrade(
    double score,
    string system = "percentage")
    {
        if (score < 0 || score > 100)
        {
            return BadRequest("Score must be between 0 and 100.");
        }

        var strategy = system.ToLower() switch
        {
            "percentage" => HttpContext.RequestServices
                .GetRequiredKeyedService<IGradeStrategy>("percentage"),

            "gpa" => HttpContext.RequestServices
                .GetRequiredKeyedService<IGradeStrategy>("gpa"),

            _ => null
        };

        if (strategy == null)
        {
            return BadRequest(
                "System must be 'percentage' or 'gpa'.");
        }

        var result = strategy.Calculate(score);

        return Ok(new
        {
            score,
            system,
            result
        });
    }
    [HttpGet("search")]
    public IActionResult SearchStudents([FromQuery] string name)
    {
        var students = studentService.SearchStudents(name);

        return Ok(students);
    }
}