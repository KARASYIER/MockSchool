﻿using MockSchool.Web.DataRepositories.EnumTypes;

namespace MockSchool.Web.DataRepositories
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public MajorEnum? Major { get; set; }

        public string Email { get; set; }

        public string PhohtPath { get; set; }
    }

}
