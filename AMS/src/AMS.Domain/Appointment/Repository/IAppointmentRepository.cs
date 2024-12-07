using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain._SharedKernel.Repository;
using AMS.Domain.Appointment.Dto;

namespace AMS.Domain.Appointment.Repository
{
    public interface IAppointmentRepository : IRepository<Entity.Appointment>
    {
        Task<PageList<AppointmentDto>> GetAppointmentData(SearchAppointmentDto searchAppointmentDto);
        Task<AppointmentDto> GetById(long id);
        Task<Entity.Appointment> GetAppointmentById(long id);
        Task<bool> CheckAppointmentExists(DateTime appointmentDate, long id = default);
        void AddAppointment(Entity.Appointment appointment);
        void UpdateAppointment(Entity.Appointment appointment);
        void DeleteAppointment(Entity.Appointment appointment);
    }
}
