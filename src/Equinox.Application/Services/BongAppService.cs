using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Commands;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Interfaces;
using Equinox.Infra.Data.Repository.EventSourcing;

namespace Equinox.Application.Services
{
    public class BongAppService : IBongAppService
    {
        private readonly IMapper _mapper;
        private readonly IBongRepository _BongRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public BongAppService(IMapper mapper,
                                  IBongRepository BongRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _BongRepository = BongRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<BongViewModel> GetAll()
        {
            return _BongRepository.GetAll().ProjectTo<BongViewModel>(_mapper.ConfigurationProvider);
        }

        public BongViewModel GetById(Guid id)
        {
            return _mapper.Map<BongViewModel>(_BongRepository.GetById(id));
        }

        public void Register(BongViewModel BongViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewBongCommand>(BongViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(BongViewModel BongViewModel)
        {
            var updateCommand = _mapper.Map<UpdateBongCommand>(BongViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveBongCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IList<BongHistoryData> GetAllHistory(Guid id)
        {
            return BongHistory.ToJavaScriptBongHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
