using FluentValidation;

namespace AMS.AppService.Patient.Commands.DeletePatient
{
    public class DeletePatientCommandValidator : AbstractValidator<DeletePatientCommand>
    {
        public DeletePatientCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0).WithMessage("Please enter valid patient id");
        }
    }
}
