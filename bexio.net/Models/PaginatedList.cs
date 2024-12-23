namespace bexio.net.Models;

public record PaginatedList<TItem>
{
    public List<TItem> List       { get; }
    private int         Limit      { get; }
    private int         Offset     { get; }
    private int         TotalCount { get; }

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

    public int  CurrentMaxCount => Offset + List.Count;
    public bool HasMore         => Offset + List.Count < TotalCount;

    public override string ToString()
        => $"({Offset} to {Math.Min(Offset + List.Count, TotalCount)}) of {TotalCount} entries (Page size: {Limit})";
}