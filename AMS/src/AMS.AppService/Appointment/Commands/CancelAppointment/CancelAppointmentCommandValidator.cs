using FluentValidation;


namespace AMS.AppService.Appointment.Commands.CancelAppointment
{
    public class CancelAppointmentCommandValidator : AbstractValidator<CancelAppointmentCommand>
    {
        public CancelAppointmentCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0).WithMessage("Please enter valid appointment id.");
            RuleFor(command => command.CancellationReason).NotEmpty().WithMessage("Please enter tha cancellation reason.");
        }
    }
}
