using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain.Appointment.Enum;
using AMS.Domain.Appointment.Repository;
using MediatR;

namespace AMS.AppService.Appointment.Commands.CancelAppointment
{
    public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, ValidatableResponse<ApiResponse<bool>>>
    {
        #region Props
        private readonly IAppointmentRepository _appointmentRepository;
        #endregion

        #region CTRS
        public CancelAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        #endregion

        public async Task<ValidatableResponse<ApiResponse<bool>>> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            ValidatableResponse<ApiResponse<bool>> response = new(new ApiResponse<bool>());

            Domain.Appointment.Entity.Appointment appointment = await _appointmentRepository.GetAppointmentById(request.Id);

            if (appointment != null)
            {
                appointment.AppointmentStatus = AppointmentStatus.Cancelled;
                appointment.CancellationReason = request.CancellationReason;

                _appointmentRepository.UpdateAppointment(appointment);

                if (await _appointmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken))
                {
                    response.Result.ResponseData = true;
                    response.Result.CommandMessage = "Appointment canceled Successfully ";
                }
                else
                {
                    response.Result.CommandMessage = "Failed to cancel the appointment. Try again shortly.";
                }
            }
            else
            {
                response.Result.CommandMessage = "No data found.";

            }
            return response;
        }

    }
}

