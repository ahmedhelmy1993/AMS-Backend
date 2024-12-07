using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain.Appointment.Dto;
using AMS.Domain.Appointment.Repository;
using MediatR;

namespace AMS.AppService.Appointment.Queries.GetAppointment
{
    public class GetAppointmentQueryHandler : IRequestHandler<GetAppointmentQuery, ValidatableResponse<ApiResponse<AppointmentDto>>>
    {
        #region Props
        private readonly IAppointmentRepository _appointmentRepository;
        #endregion

        #region CTRS
        public GetAppointmentQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        #endregion

        public async Task<ValidatableResponse<ApiResponse<AppointmentDto>>> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
        {
            ValidatableResponse<ApiResponse<AppointmentDto>> response = new(new ApiResponse<AppointmentDto>());

            AppointmentDto appointment = await _appointmentRepository.GetById(request.Id);

            if (appointment != null)
            {
                response.Result.ResponseData = appointment;
            }
            else
            {
                response.Result.CommandMessage = "No data found.";

            }
            return response;
        }

    }
}

