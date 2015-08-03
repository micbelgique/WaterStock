using SummerCamp.WaterStock.Web.Libraries;
using SummerCamp.WaterStock.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SummerCamp.WaterStock.Web.Controllers
{
    public class UsersController : Controller
    {
        public ViewResult Home()
        {
            //  users

            var rh = new RequestHelper<List<UserModels>>();

            var list = rh.GetRequest("Users");

            return View(list);
        }
    }
}
