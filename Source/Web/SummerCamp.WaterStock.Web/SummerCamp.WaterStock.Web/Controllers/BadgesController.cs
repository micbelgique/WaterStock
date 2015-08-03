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
    public class BadgesController : Controller
    {
        public ActionResult Home()
        {
            var rh = new RequestHelper<List<BadgeModels>>();

            var list = rh.GetRequest("Badges");

            return View(list);
        }
    }
}
