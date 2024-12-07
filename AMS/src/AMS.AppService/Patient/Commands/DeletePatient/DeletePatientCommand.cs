using AMS.Domain._SharedKernel.DTOs.Response;
using MediatR;


namespace AMS.AppService.Patient.Commands.DeletePatient
{
    public class DeletePatientCommand : IRequest<ValidatableResponse<ApiResponse<bool>>>
    {
        #region Props
        public long Id { get; set; }
        #endregion

        #region CTRS
        public DeletePatientCommand(long id)
        {
            Id = id;
        }
        #endregion
    }
}
