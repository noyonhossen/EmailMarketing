using Autofac;
using EmailMarketing.Framework.Extensions;
using EmailMarketing.Framework.Menus;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models
{
    public class AdminBaseModel
    {
        public MenuModel MenuModel { get; set; }
        public ResponseModel Response
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Session.IsAvailable
                    && _httpContextAccessor.HttpContext.Session.Keys.Contains(nameof(Response)))
                {
                    var response = _httpContextAccessor.HttpContext.Session.Get<ResponseModel>(nameof(Response));
                    _httpContextAccessor.HttpContext.Session.Remove(nameof(Response));

                    return response;
                }
                else
                    return null;
            }
            set
            {
                _httpContextAccessor.HttpContext.Session.Set(nameof(Response), value);
            }
        }

        protected IHttpContextAccessor _httpContextAccessor;
        public AdminBaseModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            SetupMenu();
        }

        public AdminBaseModel()
        {
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
            SetupMenu();
        }

        private void SetupMenu()
        {
            MenuModel = new MenuModel
            {
                MenuItems = new List<MenuItem>
                {
                    {
                        new MenuItem
                        {
                            Title = "Admins",
                            Icon = "icon-user-tie",
                            Children = new List<MenuChildItem>
                            {
                                new MenuChildItem () { Controller = "AdminUsers", Action = "Index", Area="Admin", Title = "View Admin List",
                                    Icon = "icon-user-tie", IsActive = false },
                                new MenuChildItem () { Controller = "AdminUsers", Action = "Add", Area="Admin", Title = "Add New Admin",
                                    Icon = "icon-user-tie", IsActive = false },
                             
                            }
                        }

                    },
                    {
                        new MenuItem
                        {
                            Title = "Members",
                            Icon = "icon-users4",
                            Children = new List<MenuChildItem>
                            {
                                new MenuChildItem () { Controller = "MemberUsers", Action = "Index", Area="Admin", Title = "View Members",
                                    Icon = "icon-users4", IsActive = false },
                                new MenuChildItem () { Controller = "MemberUsers", Action = "Add", Area="Admin", Title = "Add Member",
                                    Icon = "icon-user-plus", IsActive = false }
                            }
                        }

                    }
                }
            };
        }
    }
}
