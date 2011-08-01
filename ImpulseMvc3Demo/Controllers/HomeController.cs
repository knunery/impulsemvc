using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FizzWare.NBuilder;
using ImpulseMvc3Demo.Models;

namespace ImpulseMvc3Demo.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      var employees = Builder<Employee>.CreateListOfSize(30).Build();
      return View(employees);
    }
  }
}
