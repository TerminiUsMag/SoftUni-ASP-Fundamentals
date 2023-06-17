using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Homies.Data.Models
{
    [Comment("Types are the categories of the events.")]
    public class Type
    {
        [Comment("Primary key")]
        [Key]
        public int Id { get; set; } // a unique integer, Primary Key

        [Comment("Name of the type")]
        [Required]
        [MaxLength(15)]
        public string Name { get; set; } = null!; // a string with min length 5 and max length 15 (required)

        [Comment("Collection of events of the type")]
        public ICollection<Event> Events { get; set; } = new List<Event>(); // a collection of type Event
    }
}
//•	Has Id – a unique integer, Primary Key
//•	Has Name – a string with min length 5 and max length 15 (required)
//•	Has Events – a collection of type Event

