using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain.Appointment.Dto;
using AMS.Domain.Appointment.Enum;
using AMS.Domain.Patient.Dto;
using AMS.Domain.Patient.Repository;
using MediatR;
using System;
using System.ComponentModel;
using System.Reflection;


namespace AMS.AppService.Appointment.Queries.AppointmentStatusDDL
{
    public class DropDownAppointmentStatusQueryHandler : IRequestHandler<AppointmentStatusDDLDto, List<DropDownItem<long>>>
    {

        #region CTRS
        public DropDownAppointmentStatusQueryHandler()
        {
        }
        #endregion

        public async Task<List<DropDownItem<long>>> Handle(AppointmentStatusDDLDto request, CancellationToken cancellationToken)
        {
            return Enum.GetValues(typeof(AppointmentStatus))
                .Cast<AppointmentStatus>()
                .Select(e => new DropDownItem<long>
                {
                    Id = (long)(e),
                    Name = e.ToString()
                }).ToList();
        }

    }
}