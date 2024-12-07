using AMS.Domain._SharedKernel.DTOs.Response;
using MediatR;

namespace AMS.Domain.Appointment.Dto
{
    public class AppointmentStatusDDLDto : IRequest<List<DropDownItem<long>>>
    {
    }
}
