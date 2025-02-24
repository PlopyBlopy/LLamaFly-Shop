namespace Core.Models
{
    public abstract class ModelBase
    {
        public Guid Id { get; }

        public ModelBase(Guid id)
        {
            Id = id;
        }
    }
}