using AMS.Domain._SharedKernel.Entity;

namespace AMS.Domain.Patient.Entity
{
    public class Patient : BaseEntity<long>
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public ICollection<AMS.Domain.Appointment.Entity.Appointment> PatientAppointments { get; set; }
    }
}
