using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static _20240300TryCWAAPI.Models.Predict.PredictDetailModel;
using static _20240300TryCWAAPI.Models.Predict.PredictListModel;

namespace _20240300TryCWAAPI.Models.Predict
{
    public class PredictListModel
    {
        /// <summary>
        /// API天氣預報列表
        /// </summary>
        public WeatherData WeatherDataAPIData { get; set; }

        /// <summary>
        /// 天氣預報列表
        /// </summary>
        public List<PredictListData> PredictList { get; set; }

        /// <summary>
        /// API天氣警告特報
        /// </summary>
        public SpecialWeatherWarningData SpecialWeatherWarningAPIData { get; set; }
        /// <summary>
        /// 天氣預報列表
        /// </summary>
        public List<SpecialWeatherWarningListData> SpecialWeatherWarningList { get; set; }

        /// <summary>
        /// Wx天氣現象
        /// ManT最高溫度
        /// MinT最低溫度
        /// CI舒適度
        /// PoP降雨機率
        /// </summary>
        public class PredictListData
        {
            public string cityname { get; set; }
            public List<string> newmaxt { get; set; }
            public List<string> newmint { get; set; }
            public List<string> newpop { get; set; }
            public List<string> newwx { get; set; }
            public List<string> newci { get; set; }
        }
        /// <summary>
        /// datasetDescription濃霧特報
        /// contentText描述內容
        /// </summary>
        public class SpecialWeatherWarningListData
        {
            public string DatasetDescription { get; set; }
            public string ContentText { get; set; }
        }

        /// <summary>
        /// 天氣資訊
        /// </summary>
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

        /// <summary>
        /// 天氣特警報
        /// </summary>
        public class SpecialWeatherWarningData
        {
            public string success { get; set; }
            public Result result { get; set; }
            public SpecialWeatherWarningRecords Records { get; set; }
        }

        public class SpecialWeatherWarningRecords
        {
            public List<SpecialWeatherWarningRecord> Record { get; set; }
        }

        public class SpecialWeatherWarningRecord
        {
            public SpecialWeatherWarningDatasetInfo DatasetInfo { get; set; }
            public SpecialWeatherWarningContents Contents { get; set; }
            public SpecialWeatherWarningHazardConditions HazardConditions { get; set; }
        }
        public class SpecialWeatherWarningDatasetInfo
        {
            public string datasetDescription { get; set; }
            public string datasetLanguage { get; set; }
            public SpecialWeatherWarningvalidTime validTime { get; set; }
            public string issueTime { get; set; }
            public string update { get; set; }
        }
        public class SpecialWeatherWarningvalidTime
        {
            public string startTime { get; set; }
            public string endTime { get; set; }
        }
        public class SpecialWeatherWarningContents
        {
            public SpecialWeatherWarningContent content { get; set; }
        }
        public class SpecialWeatherWarningContent
        {
            public string contentLanguage { get; set; }
            public string contentText { get; set; }
        }
        public class SpecialWeatherWarningHazardConditions
        {
            public SpecialWeatherWarningHazards Hazards { get; set; }
        }

        public class SpecialWeatherWarningHazards
        {
            public List<SpecialWeatherWarningHazard> Hazard { get; set; }
        }

        public class SpecialWeatherWarningHazard
        {
            public SpecialWeatherWarningInfo Info { get; set; }
        }

        public class SpecialWeatherWarningInfo
        {
            public string Language { get; set; }
            public string Phenomena { get; set; }
            public string Significance { get; set; }
            public SpecialWeatherWarningAffectedAreas AffectedAreas { get; set; }
        }

        public class SpecialWeatherWarningAffectedAreas
        {
            public List<SpecialWeatherWarningLocation> Location { get; set; }
        }

        public class SpecialWeatherWarningLocation
        {
            public string LocationName { get; set; }
        }

        /// <summary>
        /// 取得中央氣象局API內容列表
        /// </summary>
        /// <returns></returns>
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
                        WeatherDataAPIData = weatherData;
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

        /// <summary>
        /// 取得中央氣象局API特殊天氣警告
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetCWAApiSpecialWeatherWarningAsync()
        {
            string CWAAuthorization = "CWA-CD7DD4B6-4A6A-4A19-A2FF-AD801662DD42";
            string SpecialWeatherWarning = "https://opendata.cwa.gov.tw/api/v1/rest/datastore/W-C0033-002";
            string URL = SpecialWeatherWarning + "?Authorization=" + CWAAuthorization;

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(URL);
                
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    SpecialWeatherWarningData specialweatherwarningdata = JsonConvert.DeserializeObject<SpecialWeatherWarningData>(json);
                    SpecialWeatherWarningAPIData = specialweatherwarningdata;
                }
                else
                {
                    Console.WriteLine($"API 請求失敗，狀態碼: {response.StatusCode}");
                }
            }

            return true;
        }

        /// <summary>
        /// 取得新天氣列表
        /// </summary>
        /// <returns></returns>
        public bool GetNewWeatherList()
        {

            PredictList = new List<PredictListData>();

            foreach (var WeatherDataList_temp in WeatherDataAPIData.records.Location)
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
        /// <summary>
        /// 取得新特警報列表
        /// </summary>
        /// <returns></returns>
        public bool GetNewWeatherWaringList()
        {
            SpecialWeatherWarningList = new List<SpecialWeatherWarningListData>();

            foreach (var SpecialWeatherWarningAPIData_temp in SpecialWeatherWarningAPIData.Records.Record)
            {
                var specialweatherwarninglistData = new SpecialWeatherWarningListData
                {
                    DatasetDescription = SpecialWeatherWarningAPIData_temp.DatasetInfo.datasetDescription ?? string.Empty,
                    ContentText = SpecialWeatherWarningAPIData_temp.Contents.content.contentText ?? string.Empty
                };

                SpecialWeatherWarningList.Add(specialweatherwarninglistData);
            }

            return true;
        }
    }
}