namespace StarCatalog.Web.Contracts
{
    public class StarQuery
    {
        public string? Search { get; set; }
        public string? Constellation { get; set; }
        public double? MagMin { get; set; }
        public double? MagMax { get; set; }
        public double? DistMin { get; set; }
        public double? DistMax { get; set; }
        public bool OnlyWithHrData { get; set; }
        public string SortBy { get; set; } = "mag";
        public bool Desc { get; set; } = false;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
