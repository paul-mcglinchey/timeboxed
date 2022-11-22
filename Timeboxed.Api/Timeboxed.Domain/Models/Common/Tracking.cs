using System.ComponentModel.DataAnnotations.Schema;

namespace Timeboxed.Domain.Models.Common
{
    public class Tracking
    {
        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UpdatedBy { get; set; }

        [ForeignKey(nameof(User))]
        public Guid CreatedBy { get; set; }

        public void AddTracking(Guid userId, Boolean creating = false)
        {
            if (creating)
            {
                this.CreatedAt = DateTime.Now;
                this.CreatedBy = userId;
            }

            this.UpdatedAt = DateTime.Now;
            this.UpdatedBy = userId;
        }
    }
}
