using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchool.Web.DataRepositories
{
    public class SQLStudentRpository : IStudentRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public SQLStudentRpository(AppDbContext context, ILogger<SQLStudentRpository> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public Student Delete(int id)
        {
            var student = _context.Students.Find(id);

            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }

            return student;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            _logger.LogTrace("SQLStudentRpository TraceLog");
            _logger.LogDebug("SQLStudentRpository DebugLog");
            _logger.LogInformation("SQLStudentRpository InformationLog");
            _logger.LogWarning("SQLStudentRpository WraningLog");
            _logger.LogError("SQLStudentRpository ErrorLog");
            _logger.LogCritical("SQLStudentRpository CriticalLog");

            return _context.Students;
        }

        public Student GetStudentById(int id)
        {
            return _context.Students.Find(id);
        }

        public Student Insert(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();

            return student;

        }

        public Student Update(Student updateStudent)
        {
            var student = _context.Students.Attach(updateStudent);
            student.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return updateStudent;

        }
    }
}
