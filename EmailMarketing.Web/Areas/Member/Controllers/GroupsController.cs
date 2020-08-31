using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using EmailMarketing.Common.Exceptions;
using EmailMarketing.Membership.Constants;
using EmailMarketing.Web.Areas.Member.Enums;
using EmailMarketing.Web.Areas.Member.Models;
using EmailMarketing.Web.Areas.Member.Models.Groups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailMarketing.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class GroupsController : Controller
    {
        private readonly ILogger<GroupsController> _logger;

        public GroupsController(ILogger<GroupsController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var model = Startup.AutofacContainer.Resolve<GroupModel>();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int? id)
        {
            var model = new GroupModel();

            #region for edit
            if (id.HasValue && id != 0)
            {
                await model.LoadByIdAsync(id.Value);
            }
            #endregion

            return PartialView("_AddOrEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(GroupModel model)
        {
            ModelState.Remove("Id");

            if (ModelState.IsValid)
            {
                try
                {

                    if (model.Id.HasValue && model.Id != 0)
                    {
                        await model.UpdateAsync();
                        model.Response = new ResponseModel("Group Update successful.", ResponseType.Success);
                        return RedirectToAction("Index");
                    }
                        
                    else
                    {
                        await model.AddAsync();
                        model.Response = new ResponseModel("Group creation successful.", ResponseType.Success);
                        return RedirectToAction("Index");
                    }
                        
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Group creation failured.", ResponseType.Failure);
                }

                return RedirectToAction("Index");
            }


            model.Response = new ResponseModel("Group edit failured.", ResponseType.Failure);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var model = new GroupModel();
            await model.DeleteAsync(id);
            return Json(true);
        }
        public async Task<IActionResult> GetGroups()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<GroupModel>();
            var data = await model.GetAllAsync(tableModel);
            return Json(data);
        }

    }
}
