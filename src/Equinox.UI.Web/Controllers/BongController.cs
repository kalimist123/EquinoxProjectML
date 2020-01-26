using System;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.UI.Web.Controllers
{
    [Authorize]
    public class BongController : BaseController
    {
        private readonly IBongAppService _BongAppService;

        public BongController(IBongAppService BongAppService, 
                                  INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _BongAppService = BongAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Bong-management/list-all")]
        public IActionResult Index()
        {
            return View(_BongAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Bong-management/Bong-details/{id:guid}")]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var BongViewModel = _BongAppService.GetById(id.Value);

            if (BongViewModel == null)
            {
                return NotFound();
            }

            return View(BongViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "CanWriteBongData")]
        [Route("Bong-management/register-new")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "CanWriteBongData")]
        [Route("Bong-management/register-new")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BongViewModel BongViewModel)
        {
            if (!ModelState.IsValid) return View(BongViewModel);
            _BongAppService.Register(BongViewModel);

            if (IsValidOperation())
                ViewBag.Sucesso = "Bong Registered!";

            return View(BongViewModel);
        }
        
        [HttpGet]
        [Authorize(Policy = "CanWriteBongData")]
        [Route("Bong-management/edit-Bong/{id:guid}")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var BongViewModel = _BongAppService.GetById(id.Value);

            if (BongViewModel == null)
            {
                return NotFound();
            }

            return View(BongViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "CanWriteBongData")]
        [Route("Bong-management/edit-Bong/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BongViewModel BongViewModel)
        {
            if (!ModelState.IsValid) return View(BongViewModel);

            _BongAppService.Update(BongViewModel);

            if (IsValidOperation())
                ViewBag.Sucesso = "Bong Updated!";

            return View(BongViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "CanRemoveBongData")]
        [Route("Bong-management/remove-Bong/{id:guid}")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var BongViewModel = _BongAppService.GetById(id.Value);

            if (BongViewModel == null)
            {
                return NotFound();
            }

            return View(BongViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "CanRemoveBongData")]
        [Route("Bong-management/remove-Bong/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _BongAppService.Remove(id);

            if (!IsValidOperation()) return View(_BongAppService.GetById(id));

            ViewBag.Sucesso = "Bong Removed!";
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [Route("Bong-management/Bong-history/{id:guid}")]
        public JsonResult History(Guid id)
        {
            var BongHistoryData = _BongAppService.GetAllHistory(id);
            return Json(BongHistoryData);
        }
    }
}
