using AMS.Domain._SharedKernel.DTOs.Response;
using MediatR;

namespace AMS.AppService.Appointment.Commands.DeleteAppointment
{
    public class DeleteAppointmentCommand : IRequest<ValidatableResponse<ApiResponse<bool>>>
    {
        #region Props
        public long Id { get; set; }
        #endregion

        #region CTRS
        public DeleteAppointmentCommand(long id)
        {
            Id = id;
        }
        #endregion
    }
}