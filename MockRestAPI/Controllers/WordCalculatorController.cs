using Microsoft.AspNetCore.Mvc;

namespace MockRestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordCalculatorController : ControllerBase
    {
        [HttpPost("CalculateWords")]
        public async Task<IActionResult> CalculateWords([FromBody] string text)
        {
            string[] source = text.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);

            int numberOfWords = source.Count();

            return Ok(numberOfWords);
        }
    }
}