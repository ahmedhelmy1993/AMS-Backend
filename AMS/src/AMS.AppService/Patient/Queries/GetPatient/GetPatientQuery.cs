using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain.Patient.Dto;
using MediatR;


namespace AMS.AppService.Patient.Queries.GetPatient
{
    public class GetPatientQuery : IRequest<ValidatableResponse<ApiResponse<PatientDto>>>
    {
        #region Props
        public long Id { get; set; }
        #endregion

        #region CTRS
        public GetPatientQuery(long id)
        {
            Id = id;
        }
        #endregion
    }
}