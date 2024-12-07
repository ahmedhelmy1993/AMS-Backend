using AMS.AppService.Patient.Commands.EditPatient;
using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain._SharedKernel.Enum;
using AMS.Domain.Patient.Repository;
using MediatR;


namespace AMS.AppService.Patient.Commands.DeletePatient
{
    internal class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, ValidatableResponse<ApiResponse<bool>>>
    {
        #region Props
        private readonly IPatientRepository _patientRepository;
        #endregion

        #region CTRS
        public DeletePatientCommandHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        #endregion

        public async Task<ValidatableResponse<ApiResponse<bool>>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            ValidatableResponse<ApiResponse<bool>> response = new(new ApiResponse<bool>());

            Domain.Patient.Entity.Patient patient = await _patientRepository.GetPatientById(request.Id);

            if (patient != null)
            {
                patient.DeletedStatus = (int)DeletedStatusEnum.Deleted;

                _patientRepository.UpdatePatient(patient);

                if (await _patientRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken))
                {
                    response.Result.ResponseData = true;
                    response.Result.CommandMessage = "Patient deleted successfully ";
                }
            }
            else
            {
                response.Result.CommandMessage = "No data found.";

            }
            return response;
        }

    }
}

