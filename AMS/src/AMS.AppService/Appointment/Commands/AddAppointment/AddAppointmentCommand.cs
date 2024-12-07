using AMS.Domain._SharedKernel.DTOs.Response;
using MediatR;

namespace AMS.AppService.Appointment.Commands.AddAppointment
{
    public class AddAppointmentCommand : IRequest<ValidatableResponse<ApiResponse<bool>>>
    {
        #region Props
        public long PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        #endregion

        #region CTRS
        public AddAppointmentCommand(long patientId, DateTime appointmentDate)
        {
            PatientId = patientId;
            AppointmentDate = appointmentDate;
        }
        #endregion
    }
}
