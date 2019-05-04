using System;
using System.Collections.Generic;
using TextAnalyzer.Core.Entities;

namespace TextAnalyzer.Core.Interfaces
{
    public interface IStopwordService
    {
        List<Stopword> GetStopwords();
        List<Stopword> CreateStopword(Stopword stopword);
        List<Stopword> UpdateStopword(Guid id, Stopword stopword);
        List<Stopword> DeleteStopword(Guid id);
        Stopword GetStopword(Guid id);
    }
}