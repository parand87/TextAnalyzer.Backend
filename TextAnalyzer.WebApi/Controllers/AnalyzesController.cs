using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextAnalyzer.Core.Dtos;
using TextAnalyzer.Core.Interfaces;
using TextAnalyzer.WebApi.ViewModels;

namespace TextAnalyzer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyzesController : ControllerBase
    {
        private readonly IAnalyzeService _analyzeService;

        public AnalyzesController(IAnalyzeService analyzeService)
        {
            _analyzeService = analyzeService;
        }

        [HttpPost]
        public IActionResult Analyze(AnalyzeViewModel model)
        {
            if (model.UseMinWordLen && model.MinWordLen < 1)
                return BadRequest("مقدار حداقل تعداد حروف هر کلمه باید بیش از ۱ باشد.");

            var longestWords = _analyzeService.LongestWordsAnalyzer(model.Text, new AnalyzeConfigDto()
            {
                UseMinWordLen = model.UseMinWordLen,
                MinWordLen = model.MinWordLen,
                UseStopwords = model.UseStopwords,
                StopwordId = model.StopwordId,
                ParseConnectedWords = model.ParseConnectedWords
            });
            var mostRepetitiveWords = _analyzeService.MostRepetitiveWordsAnalyzer(model.Text, new AnalyzeConfigDto()
            {
                UseMinWordLen = model.UseMinWordLen,
                MinWordLen = model.MinWordLen,
                UseStopwords = model.UseStopwords,
                StopwordId = model.StopwordId,
                ParseConnectedWords = model.ParseConnectedWords
            });
            var textWordCount = _analyzeService.CountTextWordsAnalyzer(model.Text, new AnalyzeConfigDto()
            {
                UseMinWordLen = model.UseMinWordLen,
                MinWordLen = model.MinWordLen,
                UseStopwords = model.UseStopwords,
                StopwordId = model.StopwordId,
                ParseConnectedWords = model.ParseConnectedWords
            });
            var distinctTextWordCount = _analyzeService.CountTextDistinctWordsAnalyzer(model.Text, new AnalyzeConfigDto()
            {
                UseMinWordLen = model.UseMinWordLen,
                MinWordLen = model.MinWordLen,
                UseStopwords = model.UseStopwords,
                StopwordId = model.StopwordId,
                ParseConnectedWords = model.ParseConnectedWords
            });
            return Ok(new
            {
                longestWords,
                mostRepetitiveWords,
                textWordCount,
                distinctTextWordCount
            });
        }
    }
}