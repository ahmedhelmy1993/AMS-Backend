using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain._SharedKernel.Enum;
using AMS.Domain._SharedKernel.UnitOfWork;
using AMS.Domain.Patient.Dto;
using AMS.Domain.Patient.Repository;
using AMS.Infrastructure.Context;
using AutoMapper;
using System.Linq.Expressions;

namespace AMS.Infrastructure.Repositories.Patient
{
    public class PatientRepository : Base.EntityRepository<Domain.Patient.Entity.Patient>, IPatientRepository
    {
        #region CTRS
        public IUnitOfWork UnitOfWork => AppDbContext;
        public PatientRepository(IMapper mapper, AMSContext context) : base(context, mapper)
        { }
        #endregion

        public async Task<PageList<PatientDto>> GetPatientData(SearchPatientDto patientPagingDto)
        {
            #region Declare Return Var with Intial Value
            PageList<PatientDto> patientListDto = new();

            #endregion

            #region Preparing Filter 
            Expression<Func<Domain.Patient.Entity.Patient, bool>> filter = p => p.DeletedStatus == (int)DeletedStatusEnum.NotDeleted &&
            (string.IsNullOrEmpty(patientPagingDto.Filter.PhoneNumber) || p.PhoneNumber.ToLower().Contains(patientPagingDto.Filter.PhoneNumber))
            && (string.IsNullOrEmpty(patientPagingDto.Filter.FullName) || p.PhoneNumber.ToLower().Contains(patientPagingDto.Filter.FullName))
            && (string.IsNullOrEmpty(patientPagingDto.Filter.Email) || p.PhoneNumber.ToLower().Contains(patientPagingDto.Filter.Email));
            #endregion


            List<Domain.Patient.Entity.Patient> patientList = patientPagingDto.Sorting.Column switch
            {
                "fullName" => await GetPageAsyncWithoutQueryFilter(patientPagingDto.Paginator.Page, patientPagingDto.Paginator.PageSize, filter, x => x.FullName, (int)patientPagingDto.Sorting.SortingDirection),
                "email" => await GetPageAsyncWithoutQueryFilter(patientPagingDto.Paginator.Page, patientPagingDto.Paginator.PageSize, filter, x => x.Email, (int)patientPagingDto.Sorting.SortingDirection),
                "age" => await GetPageAsyncWithoutQueryFilter(patientPagingDto.Paginator.Page, patientPagingDto.Paginator.PageSize, filter, x => x.Age, (int)patientPagingDto.Sorting.SortingDirection),
                _ => await GetPageAsyncWithoutQueryFilter(patientPagingDto.Paginator.Page, patientPagingDto.Paginator.PageSize, filter, x => x.Id, (int)SortDirectionEnum.Descending),
            };
            if (patientList?.Count > default(int))
            {
                int totalCount = await GetCountAsyncWithoutQueryFilter(filter);
                patientListDto.SetResult(totalCount, Mapper.Map<List<Domain.Patient.Entity.Patient>, List<PatientDto>>(patientList));
            }
            return patientListDto;
        }




        public async Task<PatientDto> GetById(long id)
        {
            return Mapper.Map<PatientDto>(await FirstOrDefaultNoTrackingAsync(ll => ll.Id == id && ll.DeletedStatus == (int)DeletedStatusEnum.NotDeleted));
        }

        public async Task<Domain.Patient.Entity.Patient> GetPatientById(long id)
        {
            return await FirstOrDefaultAsync(ll => ll.Id == id);
        }

        public void AddPatient(Domain.Patient.Entity.Patient patient)
        {
            CreateAsyn(patient);
        }

        public void UpdatePatient(Domain.Patient.Entity.Patient patient)
        {
            Update(patient);
        }

        public void DeletePatient(Domain.Patient.Entity.Patient patient)
        {
            Delete(patient);
        }

        public async Task<bool> CheckPatientExists(string email, long id = default)
        {
            return await GetAnyAsync(a => a.Email == email
          && (id == default || a.Id != id));
        }
        public async Task<bool> CheckPatientExistById(long id)
        {
            return await GetAnyAsync(a => a.Id == id);
        }
        public async Task<List<DropDownItem<long>>> GetPatientDropDown()
        {
            return Mapper.Map<List<DropDownItem<long>>>(await GetAllAsync());
        }

    }
}
