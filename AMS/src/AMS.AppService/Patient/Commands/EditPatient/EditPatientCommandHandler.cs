using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain.Patient.Repository;
using MediatR;


namespace AMS.AppService.Patient.Commands.EditPatient
{
    public class EditPatientCommandHandler : IRequestHandler<EditPatientCommand, ValidatableResponse<ApiResponse<bool>>>
    {
        #region Props
        private readonly IPatientRepository _patientRepository;
        #endregion

        #region CTRS
        public EditPatientCommandHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        #endregion

        public async Task<ValidatableResponse<ApiResponse<bool>>> Handle(EditPatientCommand request, CancellationToken cancellationToken)
        {
            ValidatableResponse<ApiResponse<bool>> response = new(new ApiResponse<bool>());

            Domain.Patient.Entity.Patient patient = await _patientRepository.GetPatientById(request.Id);

            if (patient != null)
            {
                patient.Address = request.Address;
                patient.Email = request.Email;
                patient.PhoneNumber = request.PhoneNumber;
                patient.Age = request.Age;

                _patientRepository.UpdatePatient(patient);

                if (await _patientRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken))
                {
                    response.Result.ResponseData = true;
                    response.Result.CommandMessage = "Patient updated successfully ";
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

