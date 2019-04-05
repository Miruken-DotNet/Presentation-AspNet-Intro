using Miruken.Mediate.Api;

namespace Intro.Api.Student
{
    public class CreateStudent : IRequest<StudentData>
    {
        public CreateStudent()
        {
        }

        public CreateStudent(StudentData student)
        {
            Student = student;
        }

        public StudentData Student { get; set; }
    }
}
