using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using Homies.Data.Models;

namespace Homies.Data.Models
{
    [Comment("Events are the main part of the app, created by users and joined by other users.")]
    public class Event
    {
        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Name of the event")]
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = null!; // min length 5 and max length 20 (required)

        [Comment("Description of the event")]
        [Required]
        [MaxLength(150)]
        public string Description { get; set; } = null!; // min length 15 and max length 150 (required)

        [Comment("Id of the user who created the event")]
        public string OrganiserId { get; set; } = null!; // an string (required)

        [Comment("User who created the event")]
        [ForeignKey(nameof(OrganiserId))]
        public IdentityUser Organiser { get; set; } = null!;// an IdentityUser (required)

        [Comment("Date and time when the event was created")]
        [Required]
        public DateTime CreatedOn { get; set; } // a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)

        [Comment("Date and time when the event starts")]
        [Required]
        public DateTime Start { get; set; } // a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)

        [Comment("Date and time when the event ends")]
        [Required]
        public DateTime End { get; set; } // a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)

        [Comment("Id of the type of the event")]
        public int TypeId { get; set; } // an integer, foreign key (required)

        [Comment("Type of the event")]
        [ForeignKey(nameof(TypeId))]
        public Type Type { get; set; } = null!; // a Type (required)

        [Comment("Collection of participants in the event")]
        public List<EventParticipant> EventsParticipants { get; set; } = new List<EventParticipant>(); // a collection of type EventParticipant
    }
}

