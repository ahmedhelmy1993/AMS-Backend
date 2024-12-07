using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AMS.Domain._SharedKernel.Enum;

namespace AMS.Infrastructure.EntityConfigurations
{
    class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Patient.Entity.Patient>
    {
        public void Configure(EntityTypeBuilder<Domain.Patient.Entity.Patient> Patient)
        {
            Patient.ToTable("Patient");
            Patient.HasKey(p => p.Id);

            Patient.Property(p => p.FullName).IsRequired().HasMaxLength(100);
            Patient.Property(p => p.Email).IsRequired().HasMaxLength(200);
            Patient.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(20);
            Patient.Property(p => p.Age).IsRequired();
            Patient.Property(p => p.Address).IsRequired().HasMaxLength(500);
            Patient.Property(p => p.DeletedStatus).IsRequired();
            Patient.Property(p => p.CreationDate).HasColumnType("datetime").IsRequired();

            Patient.HasMany<Domain.Appointment.Entity.Appointment>("PatientAppointments").WithOne(a => a.Patient)
                  .HasForeignKey(a => a.PatientId);

            Patient.HasQueryFilter(p => p.DeletedStatus == (int)DeletedStatusEnum.NotDeleted);
        }
    }
}
