using AMS.Domain._SharedKernel.DTOs.Response;
using MediatR;

namespace AMS.AppService.Appointment.Commands.CancelAppointment
{
    public class CancelAppointmentCommand : IRequest<ValidatableResponse<ApiResponse<bool>>>
    {
        #region Props
        public long Id { get; set; }
        public string CancellationReason { get; set; }
        #endregion

        #region CTRS
        public CancelAppointmentCommand(long id, string cancellationReason)
        {
            Id = id;
            CancellationReason = cancellationReason;
        }
        #endregion
    }
}
