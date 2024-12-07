using AMS.Domain._SharedKernel.DTOs.Response;
using MediatR;

namespace AMS.AppService.Patient.Commands.AddPatient
{
    public class AddPatientCommand : IRequest<ValidatableResponse<ApiResponse<bool>>>
    {
        #region Props
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }

        #endregion

        #region CTRS
        public AddPatientCommand(string email, string fullName, string address, string phoneNumber, int age)
        {
            Email = email;
            FullName = fullName;
            Address = address;
            PhoneNumber = phoneNumber;
            Age = age;
        }
        #endregion
    }
}
