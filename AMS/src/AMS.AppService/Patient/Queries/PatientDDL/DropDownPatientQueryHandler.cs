using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain.Patient.Dto;
using AMS.Domain.Patient.Repository;
using MediatR;


namespace AMS.AppService.Patient.Queries.PatientDDL
{
    public class DropDownPatientQueryHandler : IRequestHandler<PatientDDLDto, List<DropDownItem<long>>>
    {
        #region Props
        private readonly IPatientRepository _patientRepository;
        #endregion

        #region CTRS
        public DropDownPatientQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        #endregion

        public async Task<List<DropDownItem<long>>> Handle(PatientDDLDto request, CancellationToken cancellationToken)
        {
            return await _patientRepository.GetPatientDropDown();
        }

    }
}