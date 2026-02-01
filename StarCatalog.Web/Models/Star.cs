namespace StarCatalog.Web.Models
{
    public class Star
    {
        public int Id { get; set; }
        public int? Hip { get; set; }
        public string? Proper { get; set; }
        public double RA { get; set; } // degrees
        public double Dec { get; set; } // degrees
        public double? Dist { get; set; }
        public double? Mag { get; set; }
        public double? AbsMag { get; set; }
        public string? Spect { get; set; }
        public double? CI { get; set; }
        public string? Bayer { get; set; }
        public int? Flam { get; set; }
        public string? Con { get; set; }
    }
}
