using Homies.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using Type = Homies.Data.Models.Type;

namespace Homies.Models
{
    public class AddEventViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(150, MinimumLength = 15)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public int TypeId { get; set; }


        public IEnumerable<Type> Types { get; set; } = new List<Type>();
    }
}
//•	Has Id – a unique integer, Primary Key
//•	Has Name – a string with min length 5 and max length 20 (required)
//•	Has Description – a string with min length 15 and max length 150 (required)
//•	Has CreatedOn – a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
//•	Has Start – a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
//•	Has End – a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
//•	Has TypeId – an integer, foreign key (required)
//•	Has Type – a Type (required)
//•	Has EventsParticipants – a collection of type EventParticipant

