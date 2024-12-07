using AMS.Domain._SharedKernel.DTOs.Response;
using MediatR;

namespace AMS.AppService.Patient.Commands.EditPatient
{
    public class EditPatientCommand : IRequest<ValidatableResponse<ApiResponse<bool>>>
    {
        #region Props
        public long Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }

        #endregion

        #region CTRS
        public EditPatientCommand(long id, string email, string fullName, string address, string phoneNumber, int age)
        {
            Id = id;
            Email = email;
            FullName = fullName;
            Address = address;
            PhoneNumber = phoneNumber;
            Age = age;
        }
        #endregion
    }
}
