using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace _20240300TryCWAAPI.Models.EarthquakeReport
{
    public class EarthquakeReportListModel
    {
        /// <summary>
        /// API地震顯著報告列表
        /// </summary>
        public List<Earthquakes> EarthquakeSignificantDataAPIData { get; set; }

        /// <summary>
        /// 地震顯著報告
        /// </summary>
        public class EarthquakeSignificantData
        {
            public string success { get; set; }
            public Records records { get; set; }
        }
        public class Records
        {
            public string DatasetDescription { get; set; }
            public List<Earthquakes> Earthquake { get; set; }
        }

        public class Earthquakes
        {
            public int EarthquakeNo { get; set; }
            public string ReportType { get; set; }
            public string ReportColor { get; set; }
            public string ReportContent { get; set; }
            public string ReportImageURI { get; set; }
            public string ReportRemark { get; set; }
            public string Web { get; set; }
            public string ShakemapImageURI { get; set; }
            public EarthquakeInfo EarthquakeInfo { get; set; }
        }
        public class EarthquakeInfo
        {
            public string OriginTime {  get; set; }
            public string FocalDepth {  get; set; }
            public string Source { get; set; }
            public Epicenter Epicenter {  get; set; }
            public EarthquakeMagnitude EarthquakeMagnitude { get; set; }
        }
        public class Epicenter
        {
            public string Location { get; set; }
            public float EpicenterLatitude { get; set; }
            public float EpicenterLongitude { get; set; }

        }

        public class EarthquakeMagnitude
        {
            public string MagnitudeType { get; set; }
            public float MagnitudeValue { get; set; }
        }

        public async Task<bool> GetCWAEarthquakeSignificantlyApiListAsync()
        {
            EarthquakeSignificantDataAPIData = new List<Earthquakes>();
            try
            {
                string CWAAuthorization = "CWA-CD7DD4B6-4A6A-4A19-A2FF-AD801662DD42";
                string WeatherForecast36Hour = "https://opendata.cwa.gov.tw/api/v1/rest/datastore/E-A0015-001";
                string URL = WeatherForecast36Hour + "?Authorization=" + CWAAuthorization;

                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(URL);

                    if (response.IsSuccessStatusCode)
                    {
      
                        string json = await response.Content.ReadAsStringAsync();
                        
                        EarthquakeSignificantData earthquakesignificantData = JsonConvert.DeserializeObject<EarthquakeSignificantData>(json);
                        EarthquakeSignificantDataAPIData = earthquakesignificantData.records.Earthquake;
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
    }
}