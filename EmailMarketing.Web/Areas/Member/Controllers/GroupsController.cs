using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using EmailMarketing.Common.Exceptions;
using EmailMarketing.Web.Areas.Member.Enums;
using EmailMarketing.Web.Areas.Member.Models;
using EmailMarketing.Web.Areas.Member.Models.Groups;
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
        public IActionResult Add()
        {
            var model = new CreateGroupModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(
            [Bind(nameof(CreateGroupModel.Name))] CreateGroupModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await model.AddAsync();
                    model.Response = new ResponseModel("Group creation successful.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Group creation failured.", ResponseType.Failure);
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = new EditGroupModel();
            await model.LoadByIdAsync(id);
            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            [Bind(nameof(EditGroupModel.Id),
            nameof(CreateGroupModel.Name))] EditGroupModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await model.UpdateAsync();
                    model.Response = new ResponseModel("Group edit successful.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Group edit failured.", ResponseType.Failure);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var model = new GroupModel();
                try
                {
                    var title = await model.DeleteAsync(id);
                    model.Response = new ResponseModel($"Group {title} successfully deleted.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Group delete failured.", ResponseType.Failure);
                }
            }
            return RedirectToAction("Index");
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
