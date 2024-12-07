using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain.Appointment.Dto;
using MediatR;


namespace AMS.AppService.Appointment.Queries.GetAppointment
{
    public class GetAppointmentQuery : IRequest<ValidatableResponse<ApiResponse<AppointmentDto>>>
    {
        #region Props
        public long Id { get; set; }
        #endregion

        #region CTRS
        public GetAppointmentQuery(long id)
        {
            Id = id;
        }
        #endregion
    }
}