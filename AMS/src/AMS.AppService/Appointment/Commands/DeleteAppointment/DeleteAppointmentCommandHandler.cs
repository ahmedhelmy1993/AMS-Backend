using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain._SharedKernel.Enum;
using AMS.Domain.Appointment.Repository;
using MediatR;

namespace AMS.AppService.Appointment.Commands.DeleteAppointment
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, ValidatableResponse<ApiResponse<bool>>>
    {
        #region Props
        private readonly IAppointmentRepository _appointmentRepository;
        #endregion

        #region CTRS
        public DeleteAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        #endregion

        public async Task<ValidatableResponse<ApiResponse<bool>>> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            ValidatableResponse<ApiResponse<bool>> response = new(new ApiResponse<bool>());

            Domain.Appointment.Entity.Appointment appointment = await _appointmentRepository.GetAppointmentById(request.Id);

            if (appointment != null)
            {
                appointment.DeletedStatus = (int)DeletedStatusEnum.Deleted;

                _appointmentRepository.UpdateAppointment(appointment);

                if (await _appointmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken))
                {
                    response.Result.ResponseData = true;
                    response.Result.CommandMessage = "Appointment deleted Successfully ";
                }
                else
                {
                    response.Result.CommandMessage = "Failed to delete the appointment. Try again shortly.";
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

