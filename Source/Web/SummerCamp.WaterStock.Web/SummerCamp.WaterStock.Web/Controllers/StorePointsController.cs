using SummerCamp.WaterStock.Web.Libraries;
using SummerCamp.WaterStock.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SummerCamp.WaterStock.Web.Controllers
{
    public class StorePointsController : Controller
    {
        public ViewResult Home()
        {
            var model = new StorePointHomeModels();
            var rh = new RequestHelper<List<StorePointModels>>();

            var list = rh.GetRequest("StorePoints");

            model.StorePoints = list;

            var rh2 = new RequestHelper<List<ConsumptionsLite>>();
            var list2 = rh2.GetRequest("consumptions/bydays/31");
            model.History = list2;

            return View(model);
        }
    }
}
