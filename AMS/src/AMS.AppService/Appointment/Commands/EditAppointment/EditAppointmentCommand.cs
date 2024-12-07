using AMS.Domain._SharedKernel.DTOs.Response;
using MediatR;


namespace AMS.AppService.Appointment.Commands.EditAppointment
{
    public class EditAppointmentCommand : IRequest<ValidatableResponse<ApiResponse<bool>>>
    {
        #region Props
        public long Id { get; set; }
        public long PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        #endregion

        #region CTRS
        public EditAppointmentCommand(long id ,long patientId, DateTime appointmentDate)
        {
            Id = id;
            PatientId = patientId;
            AppointmentDate = appointmentDate;
        }
        #endregion
    }
}
