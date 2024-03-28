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
                await model.GetCWAApiListAsync();

                model.GetNewList();

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View();
            }
        }

        public async Task<ActionResult> PredictDetail (PredictDetailModel model)
        {
            await model.GetCWAApiListAsync();
            model.GetCityDetail();

            return View(model);
        }
    }
}