namespace InRiver.ImageAnalyzer
{
    public class AnalyzeImageResponse
    {
        public Adult Adult { get; set; }

        public Color Color { get; set; }

        public Description Description { get; set; }

        public TagItem[] Tags { get; set; }
    }

    public class Adult
    {
        public bool IsAdultContent { get; set; }

        public bool IsRacyContent { get; set; }

        public decimal AdultScore { get; set; }

        public decimal RacyScore { get; set; }
    }

    public class Description
    {
        public string[] Tags { get; set; }

        public Caption[] Captions { get; set; }
    }

    public class Caption
    {
        public decimal Confidence { get; set; }

        public string Text { get; set; }
    }

    public class Color
    {
        public string DominantColorBackground { get; set; }
        public string DominantColorForeground { get; set; }

        public string[] DominantColors { get; set; }

        public string AccentColor { get; set; }

        public bool IsBWImg { get; set; }
    }

    public class Metadata
    {
        public string Format { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }
    }

    public class TagItem
    {
        public string Name { get; set; }

        public decimal Confidence { get; set; }
    }
}
