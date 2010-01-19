using System;
using System.Web.Mvc;
using MvcHelper.Models;
using System.Collections.Generic;

namespace AspNetMvcBarebones_Scriptaculous_Edition_1.Controllers
{
	
    
	[HandleError]
	public class HomeController : Controller
	{
		private List<State> GetStates()
		{
			List<State> result = new List<State>();
			result.Add(new State { Code = "01", Display = "Alabama" });
			result.Add(new State { Code = "08", Display = "Colorado" });
			result.Add(new State { Code = "48", Display = "Texas" });
			return result;
		}
		public ActionResult Index()
		{
			HomeViewModel model = new HomeViewModel
			{
				Customer = new Customer
				{
					FirstName = "Chris",
					LastName = "Carter",
					Street = "123 Main Street",
					City = "Fort Collins",
					StateCode = "08",
					Zip = "80525"
				},
				States = GetStates()
			};
			return View(model);
		}
	}
}
