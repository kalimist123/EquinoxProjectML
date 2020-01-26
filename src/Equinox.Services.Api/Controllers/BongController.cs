using System;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.Services.Api.Controllers
{
    [Authorize]
    public class BongController : ApiController
    {
        private readonly IBongAppService _BongAppService;

        public BongController(
            IBongAppService BongAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _BongAppService = BongAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Bong-management")]
        public IActionResult Get()
        {
            return Response(_BongAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Bong-management/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var BongViewModel = _BongAppService.GetById(id);

            return Response(BongViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "CanWriteBongData")]
        [Route("Bong-management")]
        public IActionResult Post([FromBody]BongViewModel BongViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(BongViewModel);
            }

            _BongAppService.Register(BongViewModel);

            return Response(BongViewModel);
        }

        [HttpPut]
        [Authorize(Policy = "CanWriteBongData")]
        [Route("Bong-management")]
        public IActionResult Put([FromBody]BongViewModel BongViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(BongViewModel);
            }

            _BongAppService.Update(BongViewModel);

            return Response(BongViewModel);
        }

        [HttpDelete]
        [Authorize(Policy = "CanRemoveBongData")]
        [Route("Bong-management")]
        public IActionResult Delete(Guid id)
        {
            _BongAppService.Remove(id);

            return Response();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Bong-management/bong-history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var BongHistoryData = _BongAppService.GetAllHistory(id);
            return Response(BongHistoryData);
        }
    }
}
