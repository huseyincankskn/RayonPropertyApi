namespace Core.Entities.Concrete
{
    public class BaseConstantEntity<T> : IEntity
    {
        public T Id { get; set; }
    }
}