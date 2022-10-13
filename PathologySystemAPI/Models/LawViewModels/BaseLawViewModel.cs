namespace HarougeAPI.ViewModels.LawViewModels
{
    public class BaseLawViewModel
    {
        public int LawNum { get; set; }
        public int? IndexNum { get; set; }
        public string? LawContext { get; set; }
        public string LawPdfUrl { get; set; } = null!;
        public string LawName { get; set; } = null!;
        public string TypeId { get; set; } = null!;
    }
}
