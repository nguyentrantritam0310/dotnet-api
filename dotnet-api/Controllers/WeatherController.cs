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
        public ActionResult<JArray> Predict()
        {
            var result = _predictionService.PredictTemperature();
            var jsonArray = JArray.Parse(result); // ép string JSON thành JArray
            return Ok(jsonArray);
        }
    }

}
