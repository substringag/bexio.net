namespace bexio.net.Models
{
    public class SendMailRequest
    {
        public string RecipientEmail { get; set; } = "";
        public string Subject        { get; set; } = "";
        public string Message        { get; set; } = "";
        public bool   MarkAsOpen     { get; set; }
    }
}
