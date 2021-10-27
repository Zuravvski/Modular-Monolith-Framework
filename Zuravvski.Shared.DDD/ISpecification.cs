namespace Zuravvski.DDD
{
    public interface ISpecification<T>
    {
        public bool IsSatisfiedBy(T entity);
    }
}
