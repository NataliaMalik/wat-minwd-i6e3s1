using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using OpenWeatherApp.DataAccessLayer.Class;
using OpenWeatherApp.DataAccessLayer.Models;

namespace OpenWeatherApp.DataAccessLayer.Controllers
{
    public class OpenWeatherMapMvcController : Controller
    {
        public ActionResult Index()
        {
            OpenWeatherMap openWeatherMap = FillCity();
            return View(openWeatherMap);
        }

        public OpenWeatherMap FillCity()
        {
            OpenWeatherMap openWeatherMap = new OpenWeatherMap();
            openWeatherMap.cities = new Dictionary<string, string>();
            openWeatherMap.cities.Add("Białystok", "776069");
            openWeatherMap.cities.Add("Bydgoszcz", "7530814");
            openWeatherMap.cities.Add("Gdańsk", "7531002");
            openWeatherMap.cities.Add("Katowice", "3096472");
            openWeatherMap.cities.Add("Kielce", "769250");
            openWeatherMap.cities.Add("Lublin", "765876");
            openWeatherMap.cities.Add("Olsztyn", "763166");
            openWeatherMap.cities.Add("Opole", "3090048");
            openWeatherMap.cities.Add("Poznan", "3088171");
            openWeatherMap.cities.Add("Szczecin", "3083829");
            openWeatherMap.cities.Add("Torun", "3083271");
            openWeatherMap.cities.Add("Warszawa", "756135");
            return openWeatherMap;
        }

        [HttpPost]
        public ActionResult Index(OpenWeatherMap openWeatherMap, string cities)
        {
            openWeatherMap = FillCity();

            if (cities != null)
            {
                /*Calling API http://openweathermap.org/api */
                string apiKey = "5f24f5d9cc870e960acaa5907fe88b93";
                HttpWebRequest apiRequest =
                WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?id=" +
                cities + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;

                string apiResponse = "";
                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponse = reader.ReadToEnd();
                }
                /*End*/

                /*http://json2csharp.com*/
                ResponseWeather rootObject = JsonConvert.DeserializeObject<ResponseWeather>(apiResponse);

                StringBuilder sb = new StringBuilder();
                sb.Append("<table><tr><th>Weather Description</th></tr>");
                sb.Append("<tr><td>City:</td><td>" +
                rootObject.name + "</td></tr>");
                sb.Append("<tr><td>Country:</td><td>" +
                rootObject.sys.country + "</td></tr>");
                sb.Append("<tr><td>Wind:</td><td>" +
                rootObject.wind.speed + " Km/h</td></tr>");
                sb.Append("<tr><td>Current Temperature:</td><td>" +
                rootObject.main.temp + " °C</td></tr>");
                sb.Append("<tr><td>Humidity:</td><td>" +
                rootObject.main.humidity + "</td></tr>");
                sb.Append("<tr><td>Weather:</td><td>" +
                rootObject.weather[0].description + "</td></tr>");
                sb.Append("</table>");
                openWeatherMap.apiResponse = sb.ToString();
            }
            else
            {
                if (!StringValues.IsNullOrEmpty(Request.Form["submit"]))

                {
                    openWeatherMap.apiResponse = "► Select City";
                }
            }
            return View(openWeatherMap);
        }
    }
}