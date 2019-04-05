namespace Intro.Api.Student
{
    public class StudentResult
    {
        public StudentResult(StudentData[] students)
        {
            Students = students;
        }

        public StudentData[] Students { get; set; }
    }
}