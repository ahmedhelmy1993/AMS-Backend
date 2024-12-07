using AMS.AppService.Patient.Commands.AddPatient;
using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain._SharedKernel.Enum;
using AMS.Domain.Appointment.Repository;
using AMS.Domain.Patient.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.AppService.Appointment.Commands.AddAppointment
{
    internal class AddAppointmentCommandHandler : IRequestHandler<AddAppointmentCommand, ValidatableResponse<ApiResponse<bool>>>
    {
        #region Props
        private readonly IAppointmentRepository _appointmentRepository;
        #endregion

        #region CTRS
        public AddAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        #endregion

        public async Task<ValidatableResponse<ApiResponse<bool>>> Handle(AddAppointmentCommand request, CancellationToken cancellationToken)
        {
            ValidatableResponse<ApiResponse<bool>> response = new(new ApiResponse<bool>());

            Domain.Appointment.Entity.Appointment appointment = new Domain.Appointment.Entity.Appointment()
            {
                PatientId = request.PatientId,
                AppointmentDate = new DateTime(request.AppointmentDate.Year, request.AppointmentDate.Month, request.AppointmentDate.Day, request.AppointmentDate.Hour, 0, 0),
                AppointmentStatus = Domain.Appointment.Enum.AppointmentStatus.Scheduled,
                CreationDate = DateTime.Now,
                DeletedStatus = (int)DeletedStatusEnum.NotDeleted
            };


            _appointmentRepository.AddAppointment(appointment);

            if (await _appointmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken))
            {
                response.Result.ResponseData = true;
                response.Result.CommandMessage = "Appointment added Successfully ";
            }
            else
            {
                response.Result.CommandMessage = "Failed to add the new appointment. Try again shortly.";
            }
            return response;
        }

    }
}

