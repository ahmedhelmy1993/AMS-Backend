using AMS.Domain._SharedKernel.DTOs.Base;

namespace AMS.Domain.Patient.Dto
{
    public class PatientDto : BaseDto<long>
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
    }
}
