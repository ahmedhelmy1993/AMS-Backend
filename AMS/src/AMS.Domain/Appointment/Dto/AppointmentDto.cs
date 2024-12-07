using AMS.Domain.Appointment.Enum;

namespace AMS.Domain.Appointment.Dto
{
    public class AppointmentDto
    {
        public long PatientId { get; set; }
        public string PatientFullName { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public string CancellationReason { get; set; }
    }
}
