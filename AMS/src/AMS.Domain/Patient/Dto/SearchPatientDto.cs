using AMS.Domain._SharedKernel.DTOs.Request;
using AMS.Domain._SharedKernel.DTOs.Response;
using MediatR;

namespace AMS.Domain.Patient.Dto
{
    public class SearchPatientDto : SearchDto<PatientDto>, IRequest<PageList<PatientDto>>
    {
    }
}
