namespace InRiver.ImageAnalyzer.Models
{
    public class Color
    {
        public string DominantColorBackground { get; set; }
        public string DominantColorForeground { get; set; }

        public string[] DominantColors { get; set; }

        public string AccentColor { get; set; }

        public bool IsBWImg { get; set; }
    }
}