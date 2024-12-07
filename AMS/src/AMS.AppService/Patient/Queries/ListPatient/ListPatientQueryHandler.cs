using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain.Patient.Dto;
using AMS.Domain.Patient.Repository;
using MediatR;


namespace AMS.AppService.Patient.Queries.ListPatient
{
    public class ListPatientQueryHandler : IRequestHandler<SearchPatientDto, PageList<PatientDto>>
    {
        #region Props
        private readonly IPatientRepository _patientRepository;
        #endregion

        #region CTRS
        public ListPatientQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        #endregion

        public async Task<PageList<PatientDto>> Handle(SearchPatientDto request, CancellationToken cancellationToken)
        {
            return await _patientRepository.GetPatientData(request);
        }
    }
}
