using System.ComponentModel.DataAnnotations.Schema;

namespace Timeboxed.Domain.Models
{
    public class UserPreferences
    {
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public Guid? DefaultGroup { get; set; }
    }
}
