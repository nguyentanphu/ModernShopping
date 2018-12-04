using System.Collections.Generic;
using ModernShopping.Presentation.Models;

namespace ModernShopping.Presentation.Providers
{
    public interface IWeatherProvider
    {
        List<WeatherForecast> GetForecasts();
    }
}
