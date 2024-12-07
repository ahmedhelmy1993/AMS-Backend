using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain.Appointment.Enum;
using AutoMapper;


namespace AMS.Infrastructure.AutoMapper
{
    public class AMSProfile : Profile
    {
        public AMSProfile()
        {
            PatientMapping();
            AppointmentMapping();
        }
        private void PatientMapping()
        {
            CreateMap<Domain.Patient.Entity.Patient, Domain.Patient.Dto.PatientDto>();
            CreateMap<Domain.Patient.Entity.Patient, DropDownItem<long>>().ConvertUsing(patient => new DropDownItem<long>()
            {
                Id = patient.Id,
                Name = patient.FullName
            });

        }
        private void AppointmentMapping()
        {
            CreateMap<Domain.Appointment.Entity.Appointment, Domain.Appointment.Dto.AppointmentDto>().ConvertUsing(appointment => new Domain.Appointment.Dto.AppointmentDto()
            {
                PatientId = appointment.Id,
                AppointmentDate = appointment.AppointmentDate,
                PatientFullName = appointment.Patient.FullName,
                CancellationReason = appointment.CancellationReason ?? string.Empty,
                AppointmentStatus = (appointment.AppointmentDate > DateTime.Today && appointment.AppointmentStatus != AppointmentStatus.Cancelled) ? appointment.AppointmentStatus : AppointmentStatus.Completed,
            });


        }
    }
}