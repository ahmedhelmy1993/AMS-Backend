using AMS.Domain.Appointment.Repository;
using AMS.Domain.Patient.Repository;
using FluentValidation;

namespace AMS.AppService.Appointment.Commands.EditAppointment
{
    internal class EditAppointmentCommandValidator : AbstractValidator<EditAppointmentCommand>
    {
        #region Props
        private readonly IPatientRepository _patientRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        #endregion

        public EditAppointmentCommandValidator(IPatientRepository patientRepository, IAppointmentRepository appointmentRepository)
        {
            _patientRepository = patientRepository;
            _appointmentRepository = appointmentRepository;
            RuleFor(command => command).Must(IsAppointmentReservedExist).WithMessage("The appointment is already reserved.")
            .Must(CheckAppointmentDateGreaterThanCurrentHour)
            .WithMessage("The appointment date should be geater than the current hour.");
            RuleFor(command => command.PatientId).GreaterThan(0).WithMessage("Please enter patient id.");
            RuleFor(command => command).Must(IsPatientExist).WithMessage("Patient id not exist.");
        }

        private bool IsPatientExist(EditAppointmentCommand editAppointmentCommand)
        {
            return (_patientRepository.CheckPatientExistById(editAppointmentCommand.PatientId).Result);
        }
        private bool IsAppointmentReservedExist(EditAppointmentCommand editAppointmentCommand)
        {
            return !(_appointmentRepository.CheckAppointmentExists(editAppointmentCommand.AppointmentDate, editAppointmentCommand.Id).Result);
        }
        private bool CheckAppointmentDateGreaterThanCurrentHour(EditAppointmentCommand editAppointmentCommand)
        {
            DateTime now = DateTime.Now;
            return editAppointmentCommand.AppointmentDate > now.AddMinutes(-now.Minute).AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);
        }
    }
}
