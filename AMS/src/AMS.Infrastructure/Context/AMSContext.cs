using AMS.Domain._SharedKernel.UnitOfWork;
using AMS.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace AMS.Infrastructure.Context
{
    public class AMSContext : DbContext, IUnitOfWork
    {

        #region DBSet
        public DbSet<Domain.Patient.Entity.Patient> Patient { get; set; }
        public DbSet<Domain.Appointment.Entity.Appointment> Appointment { get; set; }
        #endregion 

        #region CTRS
        public AMSContext(DbContextOptions<AMSContext> options) : base(options)
        {
        }


        #endregion

        #region Model Creation
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PatientEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentEntityTypeConfiguration());
        }
        #endregion

        #region Save Object
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await base.SaveChangesAsync(cancellationToken) > default(int);

            return result;
        }
        #endregion

    }
}
