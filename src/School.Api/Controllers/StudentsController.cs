using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Api.Contracts;
using School.Api.Data;
using School.Api.Models;
using System;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StudentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // CREATE
    [HttpPost]
    public async Task<ActionResult<StudentResponse>> Create(CreateStudentRequest request)
    {
        var student = new Student
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            DateOfBirth = request.DateOfBirth
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = student.Id }, ToResponse(student));
    }

    // READ ALL
    [HttpGet]
    public async Task<List<StudentResponse>> GetAll()
    {
        return await _context.Students
            .AsNoTracking()
            .Select(s => ToResponse(s))
            .ToListAsync();
    }

    // READ BY ID
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<StudentResponse>> GetById(Guid id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            return NotFound();

        return ToResponse(student);
    }

    // UPDATE
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateStudentRequest request)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            return NotFound();

        student.FirstName = request.FirstName;
        student.LastName = request.LastName;
        student.Email = request.Email;
        student.DateOfBirth = request.DateOfBirth;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            return NotFound();

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static StudentResponse ToResponse(Student s)
        => new(s.Id, s.FirstName, s.LastName, s.Email, s.DateOfBirth);
}
