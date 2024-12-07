using FluentValidation;


namespace AMS.AppService.Appointment.Commands.DeleteAppointment
{
    public class DeleteAppointmentCommandValidator : AbstractValidator<DeleteAppointmentCommand>
    {
        public DeleteAppointmentCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0).WithMessage("Please enter valid appointment id.");
        }
    }
}
