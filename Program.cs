using Week5_StudentApi.Data;
using Week5_StudentApi.Models;
using Week5_StudentApi.Services;
using Week5_StudentApi.Strategies;

var builder = WebApplication.CreateBuilder(args);

// Add controller support.
builder.Services.AddControllers();

// Add Swagger/OpenAPI services.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRepository<Student>, InMemoryRepository<Student>>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddKeyedScoped<IGradeStrategy, PercentageGradeStrategy>("percentage");
builder.Services.AddKeyedScoped<IGradeStrategy, GpaGradeStrategy>("gpa");
builder.Services.AddSingleton<IRepository<Teacher>, InMemoryRepository<Teacher>>();
builder.Services.AddScoped<ITeacherService, TeacherService>();

var app = builder.Build();

/*
 Default middleware pipeline order:

 1. Swagger / Swagger UI - exposes API documentation in Development.
 2. HTTPS Redirection - redirects HTTP requests to HTTPS.
 3. Authorization - checks authorization when secured endpoints are added.
 4. MapControllers - maps controller routes to API endpoints.

 Middleware order matters because requests pass through
 the pipeline in the same order the middleware is registered.
*/

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();