namespace Zuravvski.DDD.Exceptions
{
    public class InvalidEntityIdException : DomainException
    {
        public override string Code => "invalid_aggregate_id";

        public InvalidEntityIdException() : base("Aggregate ID cannot be empty")
        {
        }
    }
}
