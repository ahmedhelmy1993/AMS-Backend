using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain.Appointment.Repository;
using MediatR;

namespace AMS.AppService.Appointment.Commands.EditAppointment
{
    internal class EditAppointmentCommandHandler : IRequestHandler<EditAppointmentCommand, ValidatableResponse<ApiResponse<bool>>>
    {
        #region Props
        private readonly IAppointmentRepository _appointmentRepository;
        #endregion

        #region CTRS
        public EditAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        #endregion

        public async Task<ValidatableResponse<ApiResponse<bool>>> Handle(EditAppointmentCommand request, CancellationToken cancellationToken)
        {
            ValidatableResponse<ApiResponse<bool>> response = new(new ApiResponse<bool>());

            Domain.Appointment.Entity.Appointment appointment = await _appointmentRepository.GetAppointmentById(request.Id);

            if (appointment != null)
            {
                appointment.PatientId = request.PatientId;
                appointment.AppointmentDate = new DateTime(request.AppointmentDate.Year, request.AppointmentDate.Month, request.AppointmentDate.Day, request.AppointmentDate.Hour, 0, 0);
                appointment.AppointmentStatus = Domain.Appointment.Enum.AppointmentStatus.Scheduled;

                _appointmentRepository.UpdateAppointment(appointment);

                if (await _appointmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken))
                {
                    response.Result.ResponseData = true;
                    response.Result.CommandMessage = "Appointment updated Successfully ";
                }
                else
                {
                    response.Result.CommandMessage = "Failed to update the appointment. Try again shortly.";
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

