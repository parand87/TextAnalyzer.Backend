using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Newtonsoft.Json;
using TextAnalyzer.Core.Entities;
using TextAnalyzer.Core.Interfaces;

namespace TextAnalyzer.Services
{
    public class StopwordService : IStopwordService
    {
        private readonly string _stopwordsFile = @"..\data\stopwords.json";
        public List<Stopword> GetStopwords()
        {
            var stopwords = new List<Stopword>();
            var owners = System.IO.File.ReadAllLines(_stopwordsFile);
            if (owners.Any())
                stopwords = JsonConvert.DeserializeObject<List<Stopword>>(owners.First());
            return stopwords;
        }

        public Stopword GetStopword(Guid id)
        {
            var owners = System.IO.File.ReadAllLines(_stopwordsFile);
            if (!owners.Any())
                return null;

            var stopwords = JsonConvert.DeserializeObject<List<Stopword>>(owners.First());
            var stopword = stopwords.FirstOrDefault(p => p.Id == id);
            return stopword;
        }
        public List<Stopword> CreateStopword(Stopword stopword)
        {
            var stopwords = new List<Stopword>();
            var owners = System.IO.File.ReadAllLines(_stopwordsFile);
            if (owners.Any())
                stopwords = JsonConvert.DeserializeObject<List<Stopword>>(owners.First());

            stopwords.Add(stopword);
            var fileWriter = new System.IO.StreamWriter(_stopwordsFile);
            fileWriter.WriteLine(JsonConvert.SerializeObject(stopwords));
            fileWriter.Dispose();
            return stopwords;
        }

        public List<Stopword> UpdateStopword(Guid id, Stopword stopword)
        {
            var stopwords = new List<Stopword>();
            var owners = System.IO.File.ReadAllLines(_stopwordsFile);
            if (!owners.Any())
                return stopwords;

            stopwords = JsonConvert.DeserializeObject<List<Stopword>>(owners.First());
            var existedStopword = stopwords.FirstOrDefault(p => p.Id == id);
            if(existedStopword == null)
                throw new EntryPointNotFoundException("لیست توقف یافت نشد.");

            existedStopword.Content = stopword.Content;
            existedStopword.Title = stopword.Title;

            var fileWriter = new System.IO.StreamWriter(_stopwordsFile);
            fileWriter.WriteLine(JsonConvert.SerializeObject(stopwords));
            fileWriter.Dispose();
            return stopwords;
        }

        public List<Stopword> DeleteStopword(Guid id)
        {
            var stopwords = new List<Stopword>();
            var owners = System.IO.File.ReadAllLines(_stopwordsFile);
            if (!owners.Any())
                return stopwords;

            stopwords = JsonConvert.DeserializeObject<List<Stopword>>(owners.First());
            var existedStopword = stopwords.FirstOrDefault(p => p.Id == id);
            if (existedStopword == null)
                throw new EntryPointNotFoundException("لیست توقف یافت نشد.");

            stopwords.Remove(existedStopword);
            var fileWriter = new System.IO.StreamWriter(_stopwordsFile);
            fileWriter.WriteLine(JsonConvert.SerializeObject(stopwords));
            fileWriter.Dispose();
            return stopwords;
        }
    }
}