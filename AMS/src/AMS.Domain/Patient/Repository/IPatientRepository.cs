using AMS.Domain._SharedKernel.DTOs.Response;
using AMS.Domain._SharedKernel.Repository;
using AMS.Domain.Patient.Dto;

namespace AMS.Domain.Patient.Repository
{
    public interface IPatientRepository : IRepository<Entity.Patient>
    {
        Task<PageList<PatientDto>> GetPatientData(SearchPatientDto searchPatientDto);
        Task<PatientDto> GetById(long id);
        Task<Entity.Patient> GetPatientById(long id);
        Task<bool> CheckPatientExists(string email, long id = default);
        Task<bool> CheckPatientExistById(long id);
        void AddPatient(Entity.Patient patient);
        void UpdatePatient(Entity.Patient patient);
        void DeletePatient(Entity.Patient patient);
        Task<List<DropDownItem<long>>> GetPatientDropDown();

    }
}
