using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain.Patient.Dto;
using AMS.Domain.Patient.Repository;
using MediatR;


namespace AMS.AppService.Patient.Queries.GetPatient
{
    public class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, ValidatableResponse<ApiResponse<PatientDto>>>
    {
        #region Props
        private readonly IPatientRepository _patientRepository;
        #endregion

        #region CTRS
        public GetPatientQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        #endregion

        public async Task<ValidatableResponse<ApiResponse<PatientDto>>> Handle(GetPatientQuery request, CancellationToken cancellationToken)
        {
            ValidatableResponse<ApiResponse<PatientDto>> response = new(new ApiResponse<PatientDto>());

            PatientDto patient = await _patientRepository.GetById(request.Id);

            if (patient != null)
            {
                response.Result.ResponseData = patient;
            }
            else
            {
                response.Result.CommandMessage = "No data found.";

            }
            return response;
        }

    }
}

