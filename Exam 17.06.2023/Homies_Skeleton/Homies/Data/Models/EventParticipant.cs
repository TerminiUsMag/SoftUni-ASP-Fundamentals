using Homies.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homies.Data.Models
{
    [Comment("Users who have joined an event.")]
    public class EventParticipant
    {
        [Comment("Joined user's ID")]
        public string HelperId { get; set; } = null!; // a  string, Primary Key, foreign key (required)

        [Comment("Joined user")]
        [ForeignKey(nameof(HelperId))]
        public IdentityUser Helper { get; set; } = null!; // IdentityUser

        [Comment("Event's ID")]
        public int EventId { get; set; } // an integer, Primary Key, foreign key (required)

        [Comment("Event")]
        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; } = null!; // Event
    }
}
//•	HelperId – a  string, Primary Key, foreign key (required)
//•	Helper – IdentityUser
//•	EventId – an integer, Primary Key, foreign key (required)
//•	Event – Event
