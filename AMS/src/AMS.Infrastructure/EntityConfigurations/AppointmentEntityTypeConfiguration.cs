using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AMS.Domain._SharedKernel.Enum;

namespace AMS.Infrastructure.EntityConfigurations
{
    class AppointmentEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Appointment.Entity.Appointment>
    {
        public void Configure(EntityTypeBuilder<Domain.Appointment.Entity.Appointment> Appointment)
        {
            Appointment.ToTable("Appointment");
            Appointment.HasKey(o => o.Id);

            Appointment.Property(l => l.AppointmentDate).HasColumnType("datetime").IsRequired();
            Appointment.Property(l => l.AppointmentStatus).IsRequired();
            Appointment.Property(l => l.CancellationReason).IsRequired(false);
            Appointment.Property(l => l.DeletedStatus).IsRequired();
            Appointment.Property(l => l.CreationDate).HasColumnType("datetime").IsRequired();

            Appointment.HasOne(a => a.Patient)
                  .WithMany(p => p.PatientAppointments)
                  .HasForeignKey(a => a.PatientId)
                  .OnDelete(DeleteBehavior.Cascade);

            Appointment.HasQueryFilter(c => c.DeletedStatus == (int)DeletedStatusEnum.NotDeleted);
        }
    }
}
