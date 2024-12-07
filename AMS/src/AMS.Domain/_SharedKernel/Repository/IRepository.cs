using AMS.Domain._SharedKernel.UnitOfWork;
namespace AMS.Domain._SharedKernel.Repository
{
    public interface IRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
