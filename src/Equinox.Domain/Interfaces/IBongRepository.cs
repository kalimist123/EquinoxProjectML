using Equinox.Domain.Models;

namespace Equinox.Domain.Interfaces
{
    public interface IBongRepository : IRepository<Bong>
    {
        Bong GetByReferenceNo(string referenceNo);
    }
}