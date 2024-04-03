using _20240300TryCWAAPI.Models.Predict;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace _20240300TryCWAAPI.Controllers
{
    public class PredictController : Controller
    {

        public async Task<ActionResult> PredictList(PredictListModel model)
        {
            try
            {
                if (await model.GetCWAApiListAsync() == false)
                {
                    throw new Exception("failed.");
                }

                if (await model.GetCWAApiSpecialWeatherWarningAsync() == false) 
                {
                    throw new Exception("failed.");
                }

                if (model.GetNewWeatherList() == false) 
                {
                    throw new Exception("failed.");
                }

                if (model.GetNewWeatherWaringList() == false)
                {
                    throw new Exception("failed.");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View("Error");
            }
        }

        public async Task<ActionResult> PredictDetail (PredictDetailModel model)
        {
            try
            {
                model.InitDict();

                if (await model.GetCWAApiListAsync() == false)
                { 
                }
                if (model.GetCityDetail() == false) 
                { 
                }
            
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View("Error");
            }
        }

        /// <summary>
        /// 測試用
        /// </summary>
        /// <returns></returns>
        public ActionResult map()
        {
            return View();
        }
    }
}