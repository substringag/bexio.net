using System.Collections.Generic;

namespace bexio.net.Models
{
    public class PaginatedList<TItem>
    {
        public List<TItem> List       { get; }
        public int         Limit      { get; }
        public int         Offset     { get; }
        public int         TotalCount { get; }

        public PaginatedList(List<TItem> list, int limit, int offset, int totalCount)
        {
            List       = list;
            Limit      = limit;
            Offset     = offset;
            TotalCount = totalCount;
        }

        /// <summary>
        /// Not a paginated list
        /// </summary>
        /// <param name="list"></param>
        public PaginatedList(List<TItem> list)
        {
            List   = list;
            Offset = 0;
            Limit  = TotalCount = List.Count;
        }

        public bool HasMore => Offset + List.Count < TotalCount;
    }
}
