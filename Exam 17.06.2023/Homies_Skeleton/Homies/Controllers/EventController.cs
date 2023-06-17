using Homies.Contracts;
using Homies.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homies.Controllers
{
    public class EventController : BaseController
    {
        private readonly IEventService eventService;
        public EventController(IEventService _eventService)
        {
            this.eventService = _eventService;
        }
        public async Task<IActionResult> All()
        {
            var model = await eventService.GetAllEventsAsync();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddEventViewModel model = await eventService.CreateNewAddEventViewModel();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = GetUserId();
            await eventService.AddEvent(model, userId);

            return RedirectToAction("All");
        }

        public async Task<IActionResult> Join(int id)
        {
            var userId = GetUserId();
            if (!await eventService.AddEventToJoined(id, userId))
            {
                return RedirectToAction("All");
            }

            return RedirectToAction("Joined");
        }

        public async Task<IActionResult> Joined()
        {
            var userId = GetUserId();
            IEnumerable<AllEventViewModel> model = await eventService.GetJoinedEventsAsync(userId);

            return View(model);
        }

        public async Task<IActionResult> Leave(int id)
        {
            var userId = GetUserId();
            await eventService.LeaveEvent(id, userId);

            return RedirectToAction("All", "Event");
        }

        public async Task<IActionResult> Details(int id)
        {
            DetailsEventViewModel model = await eventService.GetEventDetailsAsync(id);

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetUserId();
            EditEventViewModel model = await eventService.GetEditEventViewModel(id, userId);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditEventViewModel model)
        {
            if (!DateTime.TryParse(model.Start, out DateTime start))
            {
                ModelState.AddModelError("Start", "Invalid date format");
                return View(await eventService.GetEditEventViewModel(model.Id,GetUserId()));
            }
            if (!DateTime.TryParse(model.End, out DateTime end))
            {
                ModelState.AddModelError("End", "Invalid date format");
                return View(await eventService.GetEditEventViewModel(model.Id,GetUserId()));
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = GetUserId();
            await eventService.EditEvent(model, userId, start, end);

            return RedirectToAction("All");
        }
    }
}
