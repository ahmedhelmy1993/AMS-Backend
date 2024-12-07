using System.ComponentModel;

namespace AMS.Domain.Appointment.Enum
{
    public enum AppointmentCancellationReason
    {
        [Description("No show")]
        NoShow = 1,
        [Description("Based on patient request")]
        BasedOnPatientRequest =2,
        [Description("Physician apology")]
        PhysicianApology =3
    }
}
