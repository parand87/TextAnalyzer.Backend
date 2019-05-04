using System;

namespace TextAnalyzer.Core.Dtos
{
    public class AnalyzeConfigDto
    {
        public bool UseStopwords { get; set; }
        public Guid StopwordId { get; set; }
        public bool UseMinWordLen { get; set; }
        public int MinWordLen { get; set; }
        public bool ParseConnectedWords { get; set; }
    }
}