using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain.Appointment.Dto;
using AMS.Domain.Appointment.Repository;
using AMS.Domain.Patient.Dto;
using MediatR;


namespace AMS.AppService.Appointment.Queries.ListAppointment
{
    public class ListAppointmentQueryHandler : IRequestHandler<SearchAppointmentDto, PageList<AppointmentDto>>
    {
        #region Props
        private readonly IAppointmentRepository _appointmentRepository;
        #endregion

        #region CTRS
        public ListAppointmentQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        #endregion

        public async Task<PageList<AppointmentDto>> Handle(SearchAppointmentDto request, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.GetAppointmentData(request);
        }
    }
}
