using AMS.AppService.Patient.Queries.GetPatient;
using FluentValidation;

namespace AMS.AppService.Appointment.Queries.GetAppointment
{
    public class GetAppointmentQueryValidator : AbstractValidator<GetAppointmentQuery>
    {
        public GetAppointmentQueryValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0).WithMessage("Please enter valid appointment id");
        }
    }
}

