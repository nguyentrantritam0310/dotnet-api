using System.Diagnostics;
using System;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace dotnet_api.Services
{
    public class WeatherPredictionService
    {
        private readonly string _pythonScriptPath;
        public WeatherPredictionService(IConfiguration configuration)
        {
            _pythonScriptPath = configuration["ML:PythonScriptPath"];
        }
        public string PredictWeather7Days(double lat, double lng)
        {
            try
            {
                var latStr = lat.ToString(CultureInfo.InvariantCulture);
                var lngStr = lng.ToString(CultureInfo.InvariantCulture);
                var psi = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = $"{_pythonScriptPath} {latStr} {lngStr}",
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