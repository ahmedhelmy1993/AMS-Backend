
using AMS.Domain.Patient.Repository;
using FluentValidation;

namespace AMS.AppService.Patient.Commands.AddPatient
{
    public class AddPatientCommandValidator : AbstractValidator<AddPatientCommand>
    {
        #region Props
        private readonly IPatientRepository _patientRepository;
        #endregion

        public AddPatientCommandValidator(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
            RuleFor(command => command.FullName).NotEmpty().WithMessage("Full name is empty").MaximumLength(100).WithMessage("Full name shuld not exceed 100 characters.");
            RuleFor(command => command.Address).NotEmpty().WithMessage("Address is empty.")
                .MaximumLength(500).WithMessage("Address shuld not exceed 100 characters.");
            RuleFor(command => command.PhoneNumber).NotEmpty().WithMessage("Phone number is empty.")
                .MaximumLength(20).WithMessage("Phone number shuld not exceed 200 characters.");
            RuleFor(command => command.Age).GreaterThan(0).WithMessage("Please enter valid age.");
            RuleFor(command => command.Email).NotEmpty().WithMessage("Email is empty").EmailAddress().WithMessage("Please enter valid email")
               .MaximumLength(200).WithMessage("Email shuld not exceed 200 characters."); 
            RuleFor(command => command).Must(IsPatientExists).WithMessage("Email already exists.");
        }

        private bool IsPatientExists(AddPatientCommand addPatientCommand)
        {
            return !(_patientRepository.CheckPatientExists(addPatientCommand.Email).Result);
        }

    }
}
