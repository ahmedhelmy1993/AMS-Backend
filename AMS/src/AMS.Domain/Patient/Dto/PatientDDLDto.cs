using AMS.Domain._SharedKernel.DTOs.Response;
using MediatR;

namespace AMS.Domain.Patient.Dto
{
    public class PatientDDLDto : IRequest<List<DropDownItem<long>>>
    {
    }
}
