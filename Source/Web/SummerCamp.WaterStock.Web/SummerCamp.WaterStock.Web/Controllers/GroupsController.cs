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
    public class GroupsController : Controller
    {
        public ViewResult Home()
        {
            var rh = new RequestHelper<List<GroupModels>>();

            var list = rh.GetRequest("Groups");

            return View(list);
        }
    }
}
