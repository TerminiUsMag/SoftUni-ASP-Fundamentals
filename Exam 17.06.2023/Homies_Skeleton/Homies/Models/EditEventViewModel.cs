using Homies.Data.Models;
using System.ComponentModel.DataAnnotations;
using Type = Homies.Data.Models.Type;

namespace Homies.Models
{
    public class EditEventViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(150, MinimumLength = 15)]
        public string Description { get; set; } = null!;

        [Required]
        public string Start { get; set; }

        [Required]
        public string End { get; set; } = null!;

        [Required]
        public int TypeId { get; set; }


        public IEnumerable<Type> Types { get; set; } = new List<Type>();
    }
}
