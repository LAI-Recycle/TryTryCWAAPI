using _20240300TryCWAAPI.Models.EarthquakeReport;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace _20240300TryCWAAPI.Controllers
{
    public class EarthquakeReportController : Controller
    {
        public async Task<ActionResult> EarthquakeReportList(EarthquakeReportListModel model)
        {
            try 
            {
                if (await model.GetCWAEarthquakeSignificantlyApiListAsync() == false)
                {
                    throw new Exception("GetCWAEarthquakeSignificantlyApiListAsync failed.");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View("Error");
            }
            return View(model);
        }
    }
}