using Miruken.Mediate.Api;

namespace Intro.Api.Student
{
    public class GetStudents : IRequest<StudentResult>
    {
        public GetStudents()
        {
        }

        public GetStudents(int[] ids)
        {
            Ids = ids;
        }

        public int[] Ids { get; set; }
    }
}
