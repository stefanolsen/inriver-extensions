namespace InRiver.ImageAnalyzer.Models
{
    public class AnalyzeImageResponse
    {
        public Color Color { get; set; }

        public Description Description { get; set; }

        public TagItem[] Tags { get; set; }
    }
}
