using System.Diagnostics;
using System;
using Newtonsoft.Json.Linq;

namespace dotnet_api.Services
{
    public class WeatherPredictionService
    {
        public string PredictTemperature()
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = "../MachineLearning/predict_lstm.py",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using (var process = Process.Start(psi))
                {
                    if (process == null)
                        throw new InvalidOperationException("Failed to start Python process.");

                    string result = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    Console.WriteLine("Python result: " + result);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                return $"Server error: {ex.Message}";
            }
        }
    }
}