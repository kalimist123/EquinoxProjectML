using System;
using System.Collections.Generic;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.ViewModels;

namespace Equinox.Application.Interfaces
{
    public interface IBongAppService : IDisposable
    {
        void Register(BongViewModel bongViewModel);
        IEnumerable<BongViewModel> GetAll();
        BongViewModel GetById(Guid id);
        void Update(BongViewModel bongViewModel);
        void Remove(Guid id);
        IList<BongHistoryData> GetAllHistory(Guid id);
    }
}
