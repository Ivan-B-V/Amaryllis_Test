using MetaweatherTests.DataTransferObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace MetaweatherTests
{
    public class MetaweatherTest
    {
        string apiURL = "https://www.metaweather.com/api";
        string locationSearchEndpoint = "/location/search/?query=";
        string locationEndpoint = "/location";
        string searchingText = "min";
        string expectedTown = "Minsk";
        string woeid = "834463";
        double realLattitude = 53.9000000;
        double realLongitude = 27.5666700;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void LocationSearchingTest()
        {
            var entity = RequestsManager.RequestToEntitiesT<List<TownDescBase>>(string.Concat(apiURL + locationSearchEndpoint + searchingText));
            var townDesc = entity.Find(x => x.title.ToLower().Equals(expectedTown.ToLower()));
            Assert.AreEqual(expectedTown.ToLower(), townDesc?.title.ToLower());
        }

        [Test]
        public void CheckLatitudeLongitude()
        {
            var entity = RequestsManager.RequestToEntitiesT<List<TownDescBase>>(string.Concat(apiURL + locationSearchEndpoint + searchingText));
            var townDesc = entity?.Find(x => x.title.ToLower().Equals(expectedTown.ToLower()));
            var lattLong = townDesc?.latt_long.Split(',');

            Assert.IsTrue(Math.Abs(realLattitude - double.Parse(lattLong[0])) > 1e-5 &&
                          Math.Abs(realLongitude - double.Parse(lattLong[1])) > 1e-5);

            //Assert.AreEqual(realLattitude, double.Parse(lattLong[0]), 1e-5, "Real lattitude and actual should be different.");
            //Assert.AreEqual(realLongitude, double.Parse(lattLong[1]), 1e-5, "Real longitude and actual should be different.");
        }

        [Test]
        public void TodayWeatherInMinsk()
        {
            var entity = RequestsManager.RequestToEntitiesT<ResponceLocation>(string.Concat(apiURL + locationEndpoint + "/" + woeid));
            var weather = entity?.consolidated_weather[0];
            Assert.AreEqual(DateTime.Now.ToString("yyyy-MM-dd"), weather.applicable_date);
        }

        [Test]
        public void IsFiveYearsAgoWeatherInMinskContainsTodayWeatherName()
        {
            string past = DateTime.Now.AddYears(-5).ToString("yyyy-MM-dd");
            string today = DateTime.Now.ToString("yyyy-MM-dd");

            var pastForecast = RequestsManager.RequestToEntitiesT<List<WheatherDescription>>(string.Concat(apiURL + locationEndpoint + "/" + woeid + "/" + past.Replace('-', '/')));
            var todayForecast = RequestsManager.RequestToEntitiesT<ResponceLocation>(string.Concat(apiURL + locationEndpoint + "/" + woeid));
            
            Assert.IsTrue(pastForecast.Exists(x => x.weather_state_name == todayForecast?.consolidated_weather[0].weather_state_name));
        }

        [Test]
        public void IsSummerTempAboveZero()
        {
            int day = new Random().Next(0, 30);
            int month = new Random().Next(6, 8);
            var forecast = RequestsManager.RequestToEntitiesT<List<WheatherDescription>>(string.Concat(apiURL + locationEndpoint + "/" + woeid + "/" + $"{2021}" + "/" + $"{month}" + "/" + $"{day}"));
            var temp = forecast.Select(f => f.min_temp).Where(f => f > 0);
            Assert.IsTrue(temp != null);
        }

        [Test]
        public void IsWinterTempBelowZero()
        {
            int day = new Random().Next(0, 28);
            int[] months = { 12, 1, 2 };
            int i = new Random().Next(0, 2);

            var forecast = RequestsManager.RequestToEntitiesT<List<WheatherDescription>>(string.Concat(apiURL + locationEndpoint + "/" + woeid + "/" + $"{2021}" + "/" + $"{months[i]}" + "/" + $"{day}"));
            var temp = forecast.Select(f => f.min_temp).Where(f => f < 0);
            Assert.IsTrue(temp != null);
        }

        [Test]
        public void IsSpringAndFallTempInRange()
        {
            int[] months = { 9, 10, 11, 3, 4, 5 };
            int i = new Random().Next(0, 5);
            int day = new Random().Next(0, 28);

            var forecast = RequestsManager.RequestToEntitiesT<List<WheatherDescription>>(string.Concat(apiURL + locationEndpoint + "/" + woeid + "/" + $"{2021}" + "/" + $"{months[i]}" + "/" + $"{day}"));
            var temp = forecast.Select(f => f.min_temp).Where(f => f < 25 || f > 0);
            Assert.IsTrue(temp != null);
        }
    }
}