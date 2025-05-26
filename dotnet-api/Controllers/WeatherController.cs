using System.Text.Json;
using dotnet_api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace dotnet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherPredictionService _predictionService;

        public WeatherController(WeatherPredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        [HttpGet("predict")]
        public ActionResult<JObject> Predict([FromQuery] double lat, [FromQuery] double lng)
        {
            Console.WriteLine(lat);
            Console.WriteLine(lng);
            try
            {
                // Call the weather prediction service with coordinates
                var result = _predictionService.PredictWeather7Days(lat, lng);

                // Kiểm tra nếu kết quả trả về là rỗng hoặc không hợp lệ
                if (string.IsNullOrEmpty(result))
                {
                    return BadRequest("No data returned from the prediction service.");
                }

                // Parse JSON thành Dictionary
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(result, options);

                return Ok(dict);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }
    }
}
