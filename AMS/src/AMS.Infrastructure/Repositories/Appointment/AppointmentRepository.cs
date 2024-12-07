using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain._SharedKernel.Enum;
using AMS.Domain._SharedKernel.UnitOfWork;
using AMS.Domain.Appointment.Dto;
using AMS.Domain.Appointment.Repository;
using AMS.Infrastructure.Context;
using AutoMapper;
using System.Linq.Expressions;


namespace AMS.Infrastructure.Repositories.Appointment
{
    internal class AppointmentRepository : Base.EntityRepository<Domain.Appointment.Entity.Appointment>, IAppointmentRepository
    {
        #region CTRS
        public IUnitOfWork UnitOfWork => AppDbContext;
        public AppointmentRepository(IMapper mapper, AMSContext context) : base(context, mapper)
        { }
        #endregion

        public async Task<PageList<AppointmentDto>> GetAppointmentData(SearchAppointmentDto appointmentPagingDto)
        {
            #region Declare Return Var with Intial Value
            PageList<AppointmentDto> appointmentListDto = new();
            DateTime todayDate = DateTime.Today;
            #endregion

            #region Preparing Filter 
            Expression<Func<Domain.Appointment.Entity.Appointment, bool>> filter = p => p.DeletedStatus == (int)DeletedStatusEnum.NotDeleted &&
             (
             ((appointmentPagingDto.Filter.AppointmentDate == null && p.AppointmentDate.Date == DateTime.Today && appointmentPagingDto.Filter.PatientId == default(int)))
           || ((appointmentPagingDto.Filter.AppointmentDate != null) && p.AppointmentDate.Date.Day == appointmentPagingDto.Filter.AppointmentDate.Value.Date.Day)
           || ((appointmentPagingDto.Filter.AppointmentDate == null) && appointmentPagingDto.Filter.PatientId > default(int))
           )
            && ((appointmentPagingDto.Filter.PatientId == default(int)) || p.PatientId == appointmentPagingDto.Filter.PatientId)
            && ((appointmentPagingDto.Filter.AppointmentStatus == default(int)) || p.AppointmentStatus == appointmentPagingDto.Filter.AppointmentStatus);

            #endregion


            List<Domain.Appointment.Entity.Appointment> appointmentList = appointmentPagingDto.Sorting.Column switch
            {
                "appointmentDate" => await GetPageAsyncWithoutQueryFilter(appointmentPagingDto.Paginator.Page, appointmentPagingDto.Paginator.PageSize, filter, x => x.AppointmentDate, (int)appointmentPagingDto.Sorting.SortingDirection, "Patient"),
                _ => await GetPageAsyncWithoutQueryFilter(appointmentPagingDto.Paginator.Page, appointmentPagingDto.Paginator.PageSize, filter, x => x.Id, (int)SortDirectionEnum.Descending, "Patient"),
            };
            if (appointmentList?.Count > default(int))
            {
                int totalCount = await GetCountAsyncWithoutQueryFilter(filter);
                appointmentListDto.SetResult(totalCount, Mapper.Map<List<Domain.Appointment.Entity.Appointment>, List<AppointmentDto>>(appointmentList));
            }
            return appointmentListDto;
        }




        public async Task<AppointmentDto> GetById(long id)
        {
            return Mapper.Map<AppointmentDto>(await FirstOrDefaultAsync(ll => ll.Id == id && ll.DeletedStatus == (int)DeletedStatusEnum.NotDeleted, "Patient"));
        }

        public async Task<Domain.Appointment.Entity.Appointment> GetAppointmentById(long id)
        {
            return await FirstOrDefaultAsync(ll => ll.Id == id);
        }

        public void AddAppointment(Domain.Appointment.Entity.Appointment appointment)
        {
            CreateAsyn(appointment);
        }

        public void UpdateAppointment(Domain.Appointment.Entity.Appointment appointment)
        {
            Update(appointment);
        }

        public void DeleteAppointment(Domain.Appointment.Entity.Appointment appointment)
        {
            Delete(appointment);
        }

        public async Task<bool> CheckAppointmentExists(DateTime appointmentDate, long id = default)
        {
            return await GetAnyAsync(a => a.AppointmentDate.Date == appointmentDate.Date
            && a.AppointmentDate.Hour == appointmentDate.Hour && a.AppointmentStatus == Domain.Appointment.Enum.AppointmentStatus.Scheduled
          && (id == default || a.Id != id));
        }



    }
}
