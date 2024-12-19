namespace bexio.net.Responses;

public record FileContentResponse
{
    public string? Name    { get; set; }
    public int?    Size    { get; set; }
    public string? Mime    { get; set; }
    public string? Content { get; set; } // base64 encoded file content / byte array
}