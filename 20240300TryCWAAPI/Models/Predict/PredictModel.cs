using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;

namespace _20240300TryCWAAPI.Models.Predict
{
    public class PredictModel
    {
        public WeatherData WeatherDataList { get; set; }

        public List<PredictListData> PredictList { get; set; }

        // Wx天氣現象,ManT最高溫度,MinT最低溫度,CI舒適度,PoP降雨機率
        public class PredictListData
        { 
            public string cityname { get; set; }
            public List<string> newmaxt { get; set; }
            public List<string> newmint { get; set; }
            public List<string> newpop { get; set; }
            public List<string> newwx { get; set; }
            public List<string> newci { get; set; }
        }

        public class WeatherData
        {
            public string success { get; set; }
            public Result result { get; set; }
            public Records records { get; set; }
        }

        public class Result
        {
            public string ResourceId { get; set; }
            public List<Field> Fields { get; set; }
        }

        public class Field
        {
            public string Id { get; set; }
            public string Type { get; set; }
        }

        public class Records
        {
            public string DatasetDescription { get; set; }
            public List<Location> Location { get; set; }
        }

        public class Location
        {
            public string LocationName { get; set; }
            public List<WeatherElement> WeatherElement { get; set; }
        }

        public class WeatherElement
        {
            public string ElementName { get; set; }
            public List<Time> Time { get; set; }
        }

        public class Time
        {
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public Parameter Parameter { get; set; }
        }

        public class Parameter
        {
            public string ParameterName { get; set; }
            public string ParameterValue { get; set; }
            public string ParameterUnit { get; set; }
        }


        public async Task<bool> GetCWAApiListAsync()
        {
            try
            {
                string CWAAuthorization = "CWA-CD7DD4B6-4A6A-4A19-A2FF-AD801662DD42";
                string WeatherForecast36Hour = "https://opendata.cwa.gov.tw/api/v1/rest/datastore/F-C0032-001";
                string URL = WeatherForecast36Hour + "?Authorization=" + CWAAuthorization;

                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(URL);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(json);
                        WeatherDataList = weatherData;
                    }
                    else
                    {
                        Console.WriteLine($"API 請求失敗，狀態碼: {response.StatusCode}");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"發生錯誤: {ex.Message}");
            }

            return false;
        }

        public bool GetNewList() {

            PredictList = new List<PredictListData>();

            foreach (var WeatherDataList_temp in WeatherDataList.records.Location)
            {
                var predictData = new PredictListData
                {
                    cityname = WeatherDataList_temp.LocationName,
                    newmaxt = new List<string>(),
                    newmint = new List<string>(),
                    newpop = new List<string>(),
                    newwx = new List<string>(),
                    newci = new List<string>(),
                };

                foreach (var weatherElement in WeatherDataList_temp.WeatherElement)
                {
                    foreach (var time in weatherElement.Time)
                    {
                        if (weatherElement.ElementName == "MaxT") 
                        {
                            predictData.newmaxt.Add(time.Parameter.ParameterName ?? string.Empty);
                            break; 
                        }
                        if (weatherElement.ElementName == "MinT")
                        {
                            predictData.newmint.Add(time.Parameter.ParameterName ?? string.Empty);
                            break;
                        }
                        if (weatherElement.ElementName == "PoP")
                        {
                            predictData.newpop.Add(time.Parameter.ParameterName ?? string.Empty);
                            break;
                        }
                        if (weatherElement.ElementName == "CI")
                        {
                            predictData.newci.Add(time.Parameter.ParameterName ?? string.Empty);
                            break;
                        }
                        if (weatherElement.ElementName == "Wx")
                        {
                            predictData.newwx.Add(time.Parameter.ParameterName.ToString() ?? string.Empty);
                            break;
                        }
                    }
                }

                PredictList.Add(predictData);
            }

            return true;
        }
    }
}