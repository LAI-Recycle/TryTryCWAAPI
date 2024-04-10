using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace _20240300TryCWAAPI.Models.Predict
{
    public class PredictDetailModel
    {
        /// <summary>
        /// 選擇的城市名稱
        /// </summary>
        public string choose_cityname { get; set; }

        /// <summary>
        /// 今天的日期
        /// </summary>
        public string TodayDate { get; set; }

        /// <summary>
        /// 天氣預報列表
        /// </summary>
        public List<PredictListData> PredictList { get; set; }

        /// <summary>
        /// API天氣預報列表
        /// </summary>
        public WeatherData WeatherDataAPIData { get; set; }

        public class PredictListData
        {
            public string cityname { get; set; }
            public List<string> newmaxt { get; set; }
            public List<string> newmint { get; set; }
            public List<string> newpop { get; set; }
            public List<string> newwx { get; set; }
            public List<string> newci { get; set; }
            public List<int> startTime { get; set; }
            public List<int> endTime { get; set; }
        }

        /// <summary>
        /// 天氣數據
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
        /// 選擇城市名稱字典
        /// </summary>
        public Dictionary<string, string> ChooseCitynameDict = new Dictionary<string, string>();

        /// <summary>
        /// 初始化設定
        /// </summary>
        public void InitDict()
        {
            DateTime today = DateTime.Now;
            string month = today.Month.ToString();
            string day = today.Day.ToString();
            TodayDate = month + "/" + day;

            ChooseCitynameDict = new Dictionary<string, string>();
            ChooseCitynameDict.Add( "宜蘭縣", Resources.PredictDetail.宜蘭縣);
            ChooseCitynameDict.Add( "花蓮縣", Resources.PredictDetail.花蓮縣);
            ChooseCitynameDict.Add( "臺東縣", Resources.PredictDetail.臺東縣);
            ChooseCitynameDict.Add( "澎湖縣", Resources.PredictDetail.澎湖縣);
            ChooseCitynameDict.Add( "金門縣", Resources.PredictDetail.金門縣);
            ChooseCitynameDict.Add( "連江縣", Resources.PredictDetail.連江縣);
            ChooseCitynameDict.Add( "臺北市", Resources.PredictDetail.臺北市);
            ChooseCitynameDict.Add( "新北市", Resources.PredictDetail.新北市);
            ChooseCitynameDict.Add( "桃園市", Resources.PredictDetail.桃園市);
            ChooseCitynameDict.Add( "臺中市", Resources.PredictDetail.臺中市);
            ChooseCitynameDict.Add( "臺南市", Resources.PredictDetail.臺南市);
            ChooseCitynameDict.Add( "高雄市", Resources.PredictDetail.高雄市);
            ChooseCitynameDict.Add( "基隆市", Resources.PredictDetail.基隆市);
            ChooseCitynameDict.Add( "新竹縣", Resources.PredictDetail.新竹縣);
            ChooseCitynameDict.Add( "新竹市", Resources.PredictDetail.新竹市);
            ChooseCitynameDict.Add( "苗栗縣", Resources.PredictDetail.苗栗縣);
            ChooseCitynameDict.Add( "彰化縣", Resources.PredictDetail.彰化縣);
            ChooseCitynameDict.Add( "南投縣", Resources.PredictDetail.南投縣);
            ChooseCitynameDict.Add( "雲林縣", Resources.PredictDetail.雲林縣);
            ChooseCitynameDict.Add( "嘉義縣", Resources.PredictDetail.嘉義縣);
            ChooseCitynameDict.Add( "嘉義市", Resources.PredictDetail.嘉義市);
            ChooseCitynameDict.Add( "屏東縣", Resources.PredictDetail.屏東縣);
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

        public bool GetCityDetail() 
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
                    startTime = new List<int>(),
                    endTime = new List<int>(),
                };

                if (WeatherDataList_temp.LocationName == choose_cityname) {
                    foreach (var weatherElement in WeatherDataList_temp.WeatherElement)
                    {
                        foreach (var time in weatherElement.Time)
                        {
                            if (weatherElement.ElementName == "MaxT")
                            {
                                predictData.startTime.Add(DateTime.Parse(time.StartTime).Hour);
                                predictData.endTime.Add(DateTime.Parse(time.EndTime).Hour);
                                predictData.newmaxt.Add(time.Parameter.ParameterName ?? string.Empty);
                            }
                            if (weatherElement.ElementName == "MinT")
                            {
                                predictData.newmint.Add(time.Parameter.ParameterName ?? string.Empty);
                            }
                            if (weatherElement.ElementName == "PoP")
                            {
                                predictData.newpop.Add(time.Parameter.ParameterName ?? string.Empty);
                            }
                            if (weatherElement.ElementName == "CI")
                            {
                                predictData.newci.Add(time.Parameter.ParameterName ?? string.Empty);
                            }
                            if (weatherElement.ElementName == "Wx")
                            {
                                predictData.newwx.Add(time.Parameter.ParameterName.ToString() ?? string.Empty);  
                            }
                        }
                    }
                    PredictList.Add(predictData);
                    break;
                }
            }
            return true;
        }
    }
}