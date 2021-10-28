using System.Collections.Generic;
using System.Threading.Tasks;
using Zuravvski.DDD;

namespace Zuravvski.Infrastructure.Integration.EventProcessor
{
    public interface IEventProcessor
    {
        public Task Process(IEnumerable<DomainEvent> events);
    }
}
