using Microsoft.AspNetCore.Mvc;
using Week5_StudentApi.DTOs;
using Week5_StudentApi.Models;
using Week5_StudentApi.Services;

namespace Week5_StudentApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeachersController : ControllerBase
{
    private readonly ITeacherService teacherService;

    public TeachersController(ITeacherService teacherService)
    {
        this.teacherService = teacherService;
    }

    [HttpGet]
    public IActionResult GetAllTeachers()
    {
        var teachers = teacherService.GetAllTeachers();

        var teacherResponses = teachers.Select(teacher => new TeacherReadDto
        {
            Id = teacher.Id,
            Name = teacher.Name,
            Email = teacher.Email,
            Designation = teacher.Designation
        }).ToList();

        return Ok(teacherResponses);
    }

    [HttpGet("{id}")]
    public IActionResult GetTeacherById(int id)
    {
        var teacher = teacherService.GetTeacherById(id);

        if (teacher == null)
        {
            return NotFound();
        }

        var teacherResponse = new TeacherReadDto
        {
            Id = teacher.Id,
            Name = teacher.Name,
            Email = teacher.Email,
            Designation = teacher.Designation
        };

        return Ok(teacherResponse);
    }

    [HttpPost]
    public IActionResult AddTeacher(TeacherCreateDto teacherRequest)
    {
        var teacherEntity = new Teacher
        {
            Id = DateTime.Now.Millisecond,
            Name = teacherRequest.Name,
            Email = teacherRequest.Email,
            Designation = teacherRequest.Designation
        };

        teacherService.AddTeacher(teacherEntity);

        var teacherResponse = new TeacherReadDto
        {
            Id = teacherEntity.Id,
            Name = teacherEntity.Name,
            Email = teacherEntity.Email,
            Designation = teacherEntity.Designation
        };

        return CreatedAtAction(
            nameof(GetTeacherById),
            new { id = teacherResponse.Id },
            teacherResponse);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTeacher(
        int id,
        TeacherCreateDto teacherRequest)
    {
        var teacherEntity = new Teacher
        {
            Id = id,
            Name = teacherRequest.Name,
            Email = teacherRequest.Email,
            Designation = teacherRequest.Designation
        };

        var updated = teacherService.UpdateTeacher(teacherEntity);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTeacher(int id)
    {
        var deleted = teacherService.DeleteTeacher(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}