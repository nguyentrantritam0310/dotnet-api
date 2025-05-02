using System.Diagnostics;
using System;
using Newtonsoft.Json.Linq;

namespace dotnet_api.Services
{
    public class WeatherPredictionService
    {
        public string PredictWeather7Days()
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = "../dotnet-api/MachineLearning/predict_rf_7days.py",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using (var process = Process.Start(psi))
                {
                    if (process == null)
                        throw new InvalidOperationException("Failed to start Python process.");

                    string result = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    if (!string.IsNullOrEmpty(error)) {
                        return $"Python error: {error}";
                    }
                    Console.WriteLine("Python result: " + result);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return $"Server error: {ex.Message}";
            }
        }
    }
}