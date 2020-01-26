using System.Threading;
using System.Threading.Tasks;
using Equinox.Domain.Events;
using MediatR;

namespace Equinox.Domain.EventHandlers
{
    public class BongEventHandler :
        INotificationHandler<BongRegisteredEvent>,
        INotificationHandler<BongUpdatedEvent>,
        INotificationHandler<BongRemovedEvent>
    {
        public Task Handle(BongUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(BongRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(BongRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}