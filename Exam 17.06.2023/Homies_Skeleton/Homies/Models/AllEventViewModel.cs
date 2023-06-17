using Homies.Data.Models;
using Type = Homies.Data.Models.Type;

namespace Homies.Models
{
    public class AllEventViewModel
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

        public string Type { get; set; }

        public string Organiser { get; set; }

    }
}
