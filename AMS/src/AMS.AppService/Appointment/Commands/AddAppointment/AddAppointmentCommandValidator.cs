using AMS.Domain.Appointment.Repository;
using AMS.Domain.Patient.Repository;
using FluentValidation;

namespace AMS.AppService.Appointment.Commands.AddAppointment
{
    public class AddAppointmentCommandValidator : AbstractValidator<AddAppointmentCommand>
    {
        #region Props
        private readonly IPatientRepository _patientRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        #endregion

        public AddAppointmentCommandValidator(IPatientRepository patientRepository, IAppointmentRepository appointmentRepository)
        {
            _patientRepository = patientRepository;
            _appointmentRepository = appointmentRepository;
            RuleFor(command => command).Must(IsAppointmentReservedExist).WithMessage("The appointment is already reserved.")
            .Must(CheckAppointmentDateGreaterThanCurrentHour)
            .WithMessage("The appointment date should be geater than the current hour.");
            RuleFor(command => command.PatientId).GreaterThan(0).WithMessage("Please enter patient id.");
            RuleFor(command => command).Must(IsPatientExist).WithMessage("Patient id not exist.");
        }

        private bool IsPatientExist(AddAppointmentCommand addAppointmentCommand)
        {
            return (_patientRepository.CheckPatientExistById(addAppointmentCommand.PatientId).Result);
        }
        private bool IsAppointmentReservedExist(AddAppointmentCommand addAppointmentCommand)
        {
            return !(_appointmentRepository.CheckAppointmentExists(addAppointmentCommand.AppointmentDate).Result);
        }
        private bool CheckAppointmentDateGreaterThanCurrentHour(AddAppointmentCommand addAppointmentCommand)
        {
            DateTime now = DateTime.Now;
            return addAppointmentCommand.AppointmentDate > now.AddMinutes(-now.Minute).AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);
        }
    }
}
