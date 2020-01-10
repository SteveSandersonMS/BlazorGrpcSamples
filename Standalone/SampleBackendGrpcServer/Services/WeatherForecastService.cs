using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SampleBackendGrpc;

namespace SampleBackendGrpcServer.Services
{
    public class WeatherForecastsService : WeatherForecasts.WeatherForecastsBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public override Task<WeatherReply> GetWeather(Empty request, ServerCallContext context)
        {
            var reply = new WeatherReply();

            var rng = new Random();
            reply.Forecasts.Add(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateTimeStamp = Timestamp.FromDateTime(DateTime.UtcNow.AddDays(index)),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }));

            return Task.FromResult(reply);
        }
    }
}
