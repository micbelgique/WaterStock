using SummerCamp.WaterStock.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SummerCamp.WaterStock.Services.Controllers
{
    /// <summary>
    /// Base class to define default properties and methods
    /// </summary>
    public class BaseController : ApiController, IDisposable
    {
        private DataService _service = null;

        /// <summary>
        /// Gets a reference to the DataService
        /// </summary>
        protected virtual DataService DataService
        {
            get
            {
                return _service ?? (_service = new DataService());
            }
        }

        ~BaseController()
        {
            this.Dispose(false);
        }
       protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_service != null)
            {
                _service.Dispose();
                _service = null;
            }

        }

    }
}