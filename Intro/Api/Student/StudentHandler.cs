using System.Collections.Generic;
using Miruken.Callback;

namespace Intro.Api.Student
{
    public class StudentHandler : Handler
    {
        private static readonly List<StudentData> _students
            = new List<StudentData>{new StudentData
            {
                Id        = 1,
                FirstName = "Dalinar",
                LastName  = "Kholin"
            }};

        [Handles]
        public StudentResult GetStudents(GetStudents request)
        {
            return new StudentResult(_students.ToArray());
        }
    }
}
