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
        public ActionResult<JObject> Predict()
        {
            try
            {
                // Call the weather prediction service
                var result = _predictionService.PredictWeather7Days();

                // Kiểm tra nếu kết quả trả về là rỗng hoặc không hợp lệ
                if (string.IsNullOrEmpty(result))
                {
                    return BadRequest("No data returned from the prediction service.");
                }

                // In ra kết quả để kiểm tra
                Console.WriteLine("Raw Python result: " + result);

                // Cố gắng phân tích kết quả là JSON
                try
                {
                    // var jsonObject = JObject.Parse(result);
                    // System.Console.WriteLine("Accuracy: " + jsonObject["accuracy"]);
                    // return Ok(jsonObject);

                    // Parse JSON thành dynamic
                    // dynamic jsonObject = JsonSerializer.Deserialize<dynamic>(result);

                    // // Truy cập giá trị
                    // Console.WriteLine("Accuracy: " + jsonObject["accuracy"]);

                    // return Ok(jsonObject);

                    // Parse JSON thành Dictionary
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(result, options);


            return Ok(dict);

                }
                catch (Exception parseEx)
                {
                    return StatusCode(500, $"Error parsing Python result: {parseEx.Message}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }

        }
    }

}
