namespace bexio.net.Models.Sales.Positions
{
    public class PositionArticleExtended : PositionCustomExtended
    {
        public override string? Type { get; set; } = PositionTypes.Article;

        public int? ArticleId { get; set; }

        public override string ToString()
            => $"{base.ToString()} ({ArticleId})";
    }
}
