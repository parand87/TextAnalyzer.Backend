using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TextAnalyzer.Core.Entities;
using TextAnalyzer.Core.Interfaces;
using TextAnalyzer.WebApi.ViewModels.Stopword;
namespace TextAnalyzer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StopwordsController : ControllerBase
    {
        private readonly IStopwordService _stopwordService;
        private readonly string _stopwordsFile = @"..\data\stopwords.json";

        public StopwordsController(IStopwordService stopwordService)
        {
            _stopwordService = stopwordService;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var stopwords = _stopwordService.GetStopwords();
            return Ok(stopwords);
        }

        [HttpPost]
        public IActionResult Post([FromBody] StopwordCreateViewModel model)
        {
            var stopword = new Stopword()
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Content = model.Content
            };
            var stopwords = _stopwordService.CreateStopword(stopword);
            return Ok(stopwords);
        }

        [HttpPut("{id}")]
        public IActionResult Post(Guid id, [FromBody] StopwordCreateViewModel model)
        {
            var stopword = new Stopword()
            {
                Id = id,
                Title = model.Title,
                Content = model.Content
            };
            var stopwords = _stopwordService.UpdateStopword(id, stopword);
            return Ok(stopwords);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var stopwords = _stopwordService.DeleteStopword(id);
            return Ok(stopwords);
        }
    }
}