namespace API_piment.Models
{

    public record class Piments
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Picture { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ScovilleScale { get; set; } = string.Empty;

    }
    public record class PimentsRegisterDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Picture { get; set; }
        public string ScovilleScale { get; set; } = string.Empty;
    }
}
