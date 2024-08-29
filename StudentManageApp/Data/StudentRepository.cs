using StudentManageApp.Models;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
using System.Text.Json;
// using System.Threading.Tasks;

namespace StudentManageApp.Data
{
    public class StudentRepository
    {
        private readonly string _filePath;

        public StudentRepository(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Student>();
            }

            var jsonData = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Student>>(jsonData) ?? new List<Student>();
        }

        public async Task<Student> GetStudentAsync(int id)
        {
            var students = await GetStudentsAsync();
            return students.FirstOrDefault(s => s.Id == id) ?? new Student();
        }

        public async Task AddStudentAsync(Student student)
        {
            var students = (await GetStudentsAsync()).ToList();
            student.Id = students.Any() ? students.Max(s => s.Id) + 1 : 1;
            students.Add(student);
            var jsonData = JsonSerializer.Serialize(students);
            await File.WriteAllTextAsync(_filePath, jsonData);
        }

        public async Task UpdateStudentAsync(Student updatedStudent)
        {
            var students = (await GetStudentsAsync()).ToList();
            var studentIndex = students.FindIndex(s => s.Id == updatedStudent.Id);
            if (studentIndex >= 0)
            {
                students[studentIndex] = updatedStudent;
                var jsonData = JsonSerializer.Serialize(students);
                await File.WriteAllTextAsync(_filePath, jsonData);
            }
        }

        public async Task DeleteStudentAsync(int id)
        {
            var students = (await GetStudentsAsync()).ToList();
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                students.Remove(student);
                var jsonData = JsonSerializer.Serialize(students);
                await File.WriteAllTextAsync(_filePath, jsonData);
            }
        }
    }
}
