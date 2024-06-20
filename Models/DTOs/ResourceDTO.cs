namespace NannyNook.DTOs
{
    public class ResourceDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public DateTime? PublicationDate { get; set; }
    }
}
