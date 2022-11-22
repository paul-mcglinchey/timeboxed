using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Timeboxed.Data.Converters
{
    public class DayOfWeekConverter : ValueConverter<DayOfWeek, int>
    {
        public DayOfWeekConverter()
            : base(
                  d => (int)d,
                  d => (DayOfWeek)d)
        {
        }
    }
}
