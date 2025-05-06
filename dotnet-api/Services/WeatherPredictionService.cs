using System.Diagnostics;
using System;
using Newtonsoft.Json.Linq;

namespace dotnet_api.Services
{
    public class WeatherPredictionService
    {
        public string PredictWeather7Days(double lat, double lng)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = $"../dotnet-api/MachineLearning/predict_rf_7days.py {lat} {lng}",
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

                    if (process.ExitCode != 0)
                    {
                        return $"Python error (Exit code: {process.ExitCode}): {error}";
                    }

                    if (!string.IsNullOrEmpty(error))
                    {
                        return $"Python error: {error}";
                    }

                    if (string.IsNullOrEmpty(result))
                    {
                        return "No data returned from Python script";
                    }

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