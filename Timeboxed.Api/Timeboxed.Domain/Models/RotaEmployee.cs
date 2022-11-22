using System.ComponentModel.DataAnnotations.Schema;

namespace Timeboxed.Domain.Models
{
    public class RotaEmployee
    {
        public Guid Id { get; set; }

        [ForeignKey("Rota")]
        public Guid RotaId { get; set; }

        public virtual Rota Rota { get; set; }

        [ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
