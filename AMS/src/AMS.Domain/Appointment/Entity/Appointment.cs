using AMS.Domain._SharedKernel.Entity;
using AMS.Domain.Appointment.Enum;
using System.ComponentModel.DataAnnotations.Schema;


namespace AMS.Domain.Appointment.Entity
{
    public class Appointment : BaseEntity<long>
    {
        
        public long PatientId { get; set; }
        public Domain.Patient.Entity.Patient Patient { get; set; }

        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus AppointmentStatus{ get; set; }
        public string CancellationReason { get; set; }
    }
}
