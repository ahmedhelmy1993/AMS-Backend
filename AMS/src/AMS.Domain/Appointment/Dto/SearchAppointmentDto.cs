using AMS.Domain._SharedKernel.DTOs.Request;
using AMS.Domain._SharedKernel.DTOs.Response;
using MediatR;

namespace AMS.Domain.Appointment.Dto
{
    public class SearchAppointmentDto : SearchDto<AppointmentDto>, IRequest<PageList<AppointmentDto>>
    {
    }
}
