using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailMarketing.Web.Areas.Member.Models.Memberships;
using Microsoft.AspNetCore.Mvc;

namespace EmailMarketing.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class UpgrateMembershipController : Controller
    {
        public IActionResult Index()
        {
            var model = new UpgrateMembershipModel();
            return View(model);
        }
    }
}