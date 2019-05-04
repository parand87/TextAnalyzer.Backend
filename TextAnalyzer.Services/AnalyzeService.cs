using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TextAnalyzer.Core.Dtos;
using TextAnalyzer.Core.Interfaces;

namespace TextAnalyzer.Services
{
    public class AnalyzeService : IAnalyzeService
    {
        private readonly IStopwordService _stopwordService;

        public AnalyzeService(IStopwordService stopwordService)
        {
            _stopwordService = stopwordService;
        }
        public IEnumerable<string> LongestWordsAnalyzer(string text, AnalyzeConfigDto config)
        {
            var words = PrepareTextForAnalyzing(text, config);
            if (words == null) return null;
            
            var maxLength = words.OrderByDescending(p => p.Length).First().Length;
            var maxlenwords = words.Where(p => p.Length == maxLength).Distinct();
            return maxlenwords;
        }

        public IEnumerable<string> MostRepetitiveWordsAnalyzer(string text, AnalyzeConfigDto config)
        {
            var words = PrepareTextForAnalyzing(text, config);
            if (words == null) return null;

            var occurences = words.GroupBy(p => p).OrderByDescending(p => p.Count()).ToList();
            var mostRepetitiveCount = occurences.First().Count();
            occurences = occurences.Where(p => p.Count() == mostRepetitiveCount).ToList();
           
            return occurences.Select(p => p.Key);
        }

        public int CountTextWordsAnalyzer(string text, AnalyzeConfigDto config)
        {
            var words = PrepareTextForAnalyzing(text, config);
            return words?.Count ?? 0;
        }

        public int CountTextDistinctWordsAnalyzer(string text, AnalyzeConfigDto config)
        {
            var words = PrepareTextForAnalyzing(text, config);
            return words?.Distinct().Count() ?? 0;
        }
        private List<string> PrepareTextForAnalyzing(string text, AnalyzeConfigDto config)
        {
            text = Regex.Replace(text, "[^a-zA-Z]", " ");
            if (string.IsNullOrEmpty(text))
                return null;
            
            //remove multiple spaces from text
            var regex = new Regex("[ ]{2,}", RegexOptions.None);
            text = regex.Replace(text, " ");
            text = text.Trim();
            if (config.ParseConnectedWords)
            {
                text = Regex.Replace(text, @"(\B[A-Z]+?(?=[A-Z][^A-Z])|\B[A-Z]+?(?=[^A-Z]))", " $1");
            }

            var words = text.Split(' ').ToList();

            if (config.UseMinWordLen)
                words = words.Where(p => p.Length > config.MinWordLen).ToList();

          
            if (config.UseStopwords)
            {
                var stopword = _stopwordService.GetStopword(config.StopwordId);
                if (stopword != null)
                {
                    var stopwords = stopword.Content.Split(",");
                    words = words.Where(w => stopwords.All(s => s != w)).ToList();
                }
            }

            return words;
        }
    }
}