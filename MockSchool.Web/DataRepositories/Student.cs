using MockSchool.Web.DataRepositories.EnumTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MockSchool.Web.DataRepositories
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public MajorEnum Major { get; set; }

        public string Email { get; set; }

        public string PhotoPath { get; set; } = "noimage.pgn";
    }

}
