using System.Collections.Generic;
using TextAnalyzer.Core.Dtos;

namespace TextAnalyzer.Core.Interfaces
{
    public interface IAnalyzeService
    {
        IEnumerable<string> LongestWordsAnalyzer(string text, AnalyzeConfigDto config);
        IEnumerable<string> MostRepetitiveWordsAnalyzer(string text, AnalyzeConfigDto config);
        int CountTextWordsAnalyzer(string text, AnalyzeConfigDto config);
        int CountTextDistinctWordsAnalyzer(string text, AnalyzeConfigDto config);
    }
}