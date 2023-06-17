using Homies.Models;

namespace Homies.Contracts
{
    public interface IEventService
    {
        Task AddEvent(AddEventViewModel model, string userId);
        Task<bool> AddEventToJoined(int id, string userId);
        Task<AddEventViewModel> CreateNewAddEventViewModel();
        Task<EditEventViewModel> GetEditEventViewModel(int id, string userId);
        Task<IEnumerable<AllEventViewModel?>> GetAllEventsAsync();
        Task<DetailsEventViewModel> GetEventDetailsAsync(int id);
        Task<IEnumerable<AllEventViewModel>> GetJoinedEventsAsync(string userId);
        Task LeaveEvent(int id, string userId);
        Task EditEvent(EditEventViewModel model, string userId, DateTime start, DateTime end);
    }
}
