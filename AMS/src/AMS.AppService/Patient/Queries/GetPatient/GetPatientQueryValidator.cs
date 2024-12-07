using FluentValidation;


namespace AMS.AppService.Patient.Queries.GetPatient
{
    public class GetPatientQueryValidator : AbstractValidator<GetPatientQuery>
    {
        public GetPatientQueryValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0).WithMessage("Please enter valid patient id");
        }
    }
}

