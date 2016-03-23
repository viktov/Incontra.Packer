using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Incontra.Packer.Web.Controllers
{
    public class ViewerController : Controller
    {
        //
        // GET: /Viewer/
        public ActionResult View3D(int id)
        {
            ViewBag.ContainerID = id;
            return View();
        }
	}
}