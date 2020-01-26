using System;
using System.Threading;
using System.Threading.Tasks;
using Equinox.Domain.Commands;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Domain.Events;
using Equinox.Domain.Interfaces;
using Equinox.Domain.Models;
using MediatR;

namespace Equinox.Domain.CommandHandlers
{
    public class BongCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewBongCommand, bool>,
        IRequestHandler<UpdateBongCommand, bool>,
        IRequestHandler<RemoveBongCommand, bool>
    {
        private readonly IBongRepository _BongRepository;
        private readonly IMediatorHandler Bus;

        public BongCommandHandler(IBongRepository BongRepository, 
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) :base(uow, bus, notifications)
        {
            _BongRepository = BongRepository;
            Bus = bus;
        }

        public Task<bool> Handle(RegisterNewBongCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var Bong = new Bong(Guid.NewGuid(), message.Name, message.ReferenceNo, message.ArrivingInStock);

            if (_BongRepository.GetByReferenceNo(Bong.ReferenceNo) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The Bong e-mail has already been taken."));
                return Task.FromResult(false);
            }
            
            _BongRepository.Add(Bong);

            if (Commit())
            {
                Bus.RaiseEvent(new BongRegisteredEvent(Bong.Id, Bong.Name, Bong.ReferenceNo, Bong.ArrivingInStock));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateBongCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var Bong = new Bong(message.Id, message.Name, message.ReferenceNo, message.ArrivingInStock);
            var existingBong = _BongRepository.GetByReferenceNo(Bong.ReferenceNo);

            if (existingBong != null && existingBong.Id != Bong.Id)
            {
                if (!existingBong.Equals(Bong))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType,"The Bong ref no has already been taken."));
                    return Task.FromResult(false);
                }
            }

            _BongRepository.Update(Bong);

            if (Commit())
            {
                Bus.RaiseEvent(new BongUpdatedEvent(Bong.Id, Bong.Name, Bong.ReferenceNo, Bong.ArrivingInStock));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveBongCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            _BongRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new BongRemovedEvent(message.Id));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _BongRepository.Dispose();
        }
    }
}