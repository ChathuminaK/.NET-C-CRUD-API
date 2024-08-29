// using Microsoft.AspNetCore.Builder;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
using StudentManageApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Read configuration
var configuration = builder.Configuration;

// Register services
builder.Services.AddSingleton<StudentRepository>(sp =>
{
    var filePath = configuration["FilePath"];
    return new StudentRepository(filePath!);
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();