using System;

namespace TextAnalyzer.WebApi.ViewModels
{
    public class AnalyzeViewModel
    {
        public string Text { get; set; }
        public bool UseStopwords { get; set; }
        public Guid StopwordId { get; set; }
        public bool UseMinWordLen { get; set; }
        public int MinWordLen { get; set; }
        public bool ParseConnectedWords { get; set; }
    }
}