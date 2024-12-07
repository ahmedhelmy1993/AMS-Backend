using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain._SharedKernel.Enum;
using AMS.Domain.Patient.Repository;
using MediatR;

namespace AMS.AppService.Patient.Commands.AddPatient
{
    internal class AddPatientCommandHandler : IRequestHandler<AddPatientCommand, ValidatableResponse<ApiResponse<bool>>>
    {
        #region Props
        private readonly IPatientRepository _patientRepository;
        #endregion

        #region CTRS
        public AddPatientCommandHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        #endregion

        public async Task<ValidatableResponse<ApiResponse<bool>>> Handle(AddPatientCommand request, CancellationToken cancellationToken)
        {
            ValidatableResponse<ApiResponse<bool>> response = new(new ApiResponse<bool>());

            Domain.Patient.Entity.Patient patient = new Domain.Patient.Entity.Patient()
            {
                FullName = request.FullName,
                Email = request.Email,
                Age = request.Age,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                CreationDate = DateTime.Now,
                DeletedStatus = (int)DeletedStatusEnum.NotDeleted
            };
            

            _patientRepository.AddPatient(patient);

            if (await _patientRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken))
            {
                response.Result.ResponseData = true;
                response.Result.CommandMessage = "Patient added Successfully ";
            }
            else
            {
                response.Result.CommandMessage = "Failed to add the new patient. Try again shortly.";
            }
            return response;
        }

    }
}

