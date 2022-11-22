namespace Timeboxed.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> list, int pageNumber, int pageSize) => list.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}
