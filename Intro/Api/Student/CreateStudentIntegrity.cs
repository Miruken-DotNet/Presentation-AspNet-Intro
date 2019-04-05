using FluentValidation;

namespace Intro.Api.Student
{
    public class CreateStudentIntegrity : AbstractValidator<CreateStudent>
    {
        public CreateStudentIntegrity()
        {
            RuleFor(x => x.Student)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .SetValidator(new ValidateStudent());
        }

        private class ValidateStudent : AbstractValidator<StudentData>
        {
            public ValidateStudent()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
            }
        }
    }
}
