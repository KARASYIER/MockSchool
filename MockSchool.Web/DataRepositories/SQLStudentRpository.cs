using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchool.Web.DataRepositories
{
    public class SQLStudentRpository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public SQLStudentRpository(AppDbContext context)
        {
            this._context = context;
        }

        public Student Delete(int id)
        {
            var student = _context.Students.Find(id);

            if(student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }

            return student;
        }

        public IEnumerable<Student> GetAllStudents()
        {
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
