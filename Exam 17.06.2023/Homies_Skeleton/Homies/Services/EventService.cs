using Homies.Contracts;
using Homies.Data;
using Homies.Data.Models;
using Homies.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Globalization;

namespace Homies.Services
{
    public class EventService : IEventService
    {
        private readonly HomiesDbContext dbContext;

        public EventService(HomiesDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task AddEvent(AddEventViewModel model, string UserId)
        {
            await dbContext.Events.AddAsync(new Event
            {
                Name = model.Name,
                Start = model.Start,
                TypeId = model.TypeId,
                OrganiserId = UserId,
                CreatedOn = DateTime.Now,
                Description = model.Description,
                End = model.End,
                //Organiser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == UserId),
                //Type = await dbContext.Types.FirstOrDefaultAsync(t => t.Id == model.TypeId),
            });

            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> AddEventToJoined(int id, string userId)
        {
            if (await dbContext.EventParticipants.AnyAsync(ep => ep.EventId == id && ep.HelperId == userId))
            {
                return false;
            }
            await dbContext.EventParticipants.AddAsync(new EventParticipant
            {
                EventId = id,
                HelperId = userId
            });
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<AddEventViewModel> CreateNewAddEventViewModel()
        {
            var model = new AddEventViewModel();
            model.Types = await dbContext.Types.ToListAsync();

            return model;
        }

        public async Task<EditEventViewModel?> GetEditEventViewModel(int id, string userId)
        {
            return await dbContext.Events
                .Where(e => e.Id == id && e.OrganiserId == userId)
                .Select(e => new EditEventViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Start = e.Start.ToString(),
                    End = e.End.ToString(),
                    TypeId = e.TypeId,
                    Types = dbContext.Types.ToList(),
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AllEventViewModel?>> GetAllEventsAsync()
        {
            return await dbContext.Events
                .Select(e => new AllEventViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start,
                    Type = e.Type.Name,
                    Organiser = e.Organiser.UserName
                })
                .ToListAsync();
        }

        public async Task<DetailsEventViewModel?> GetEventDetailsAsync(int id)
        {
            return await dbContext.Events
                .Where(e => e.Id == id)
                .Select(e => new DetailsEventViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start,
                    Type = e.Type.Name,
                    Organiser = e.Organiser.UserName,
                    Description = e.Description,
                    End = e.End,
                    CreatedOn = e.CreatedOn,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AllEventViewModel>> GetJoinedEventsAsync(string userId)
        {
            return await dbContext.EventParticipants
                .Where(ep => ep.HelperId == userId)
                .Select(ep => new AllEventViewModel
                {
                    Id = ep.EventId,
                    Name = ep.Event.Name,
                    Start = ep.Event.Start,
                    Type = ep.Event.Type.Name,
                    Organiser = ep.Event.Organiser.UserName
                }).ToListAsync();
        }

        public async Task LeaveEvent(int id, string userId)
        {
            var eventParticipant = await dbContext.EventParticipants.FirstOrDefaultAsync(ep => ep.EventId == id && ep.HelperId == userId);
            if (eventParticipant is null)
            {
                return;
            }
            dbContext.EventParticipants.Remove(eventParticipant);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditEvent(EditEventViewModel model, string userId, DateTime start, DateTime end)
        {
            var e = await dbContext.Events
                .Where(e => e.Id == model.Id && e.OrganiserId == userId)
                .FirstOrDefaultAsync();
                //.ForEachAsync(e =>
                //{
                //    e.Name = model.Name;
                //    e.Description = model.Description;
                //    e.Start = DateTime.ParseExact(model.Start, "dd/MM/yyyy H:mm");
                //    e.End = DateTime.Parse(model.End);
                //    e.TypeId = model.TypeId;
                //});
                e.Name = model.Name;
            e.Description = model.Description;
            e.Start = start;
            e.End = end;
            e.TypeId = model.TypeId;
            await dbContext.SaveChangesAsync();
        }
    }
}
