namespace AMS.Domain._SharedKernel.Entity
{
    public abstract class BaseEntity<T>
    {
        #region Prop
        public virtual T Id { get; set; }

        public DateTime CreationDate { get; set; }

        public int DeletedStatus { get; set; }
        #endregion
    }
}