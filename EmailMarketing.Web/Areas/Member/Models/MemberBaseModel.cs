using Autofac;
using EmailMarketing.Framework.Extensions;
using EmailMarketing.Framework.Menus;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Extensions;
using EmailMarketing.Framework.Menus;
using EmailMarketing.Membership.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models
{
    public class MemberBaseModel
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
        protected ICurrentUserService _currentUserService;
        protected IApplicationUserService _applicationuserService;
        public MemberBaseModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            SetupMenu();
        }
      
        public MemberBaseModel(ICurrentUserService currentUserService, IApplicationUserService applicationuserService)
        {
            _applicationuserService = applicationuserService;
            _currentUserService = currentUserService;
            SetupMenu();
        }

        public MemberBaseModel()
        {
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
            _applicationuserService = Startup.AutofacContainer.Resolve<IApplicationUserService>();
            _currentUserService = Startup.AutofacContainer.Resolve<ICurrentUserService>();
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
                            Title = "Groups",
                            Icon = "icon-collaboration",
                            Children = new List<MenuChildItem>
                            {
                                new MenuChildItem () { Controller = "Groups", Action = "Index", Area="Member", Title = "View Groups",
                                    Icon = "icon-collaboration", IsActive = false },
                                new MenuChildItem () { Controller = "Groups", Action = "Add", Area="Member", Title = "Add Group",
                                    Icon = "icon-plus-circle2", IsActive = false },
                             
                            }
                        }

                    },
                    {
                        new MenuItem
                        {
                            Title = "Contacts",
                            Icon = "icon-users4",
                            Children = new List<MenuChildItem>
                            {
                                new MenuChildItem () { Controller = "Contacts", Action = "Index", Area="Member", Title = "Contacts",
                                    Icon = "icon-users4", IsActive = false },
                                new MenuChildItem () { Controller = "ContactUpload", Action = "Index", Area="Member", Title = "Manage Uploads",
                                    Icon = "icon-file-text3", IsActive = false },
                                new MenuChildItem () { Controller = "ContactUpload", Action = "UploadContact", Area="Member", Title = "Upload/Add Contacts",
                                    Icon = "icon-file-upload2", IsActive = false },
                                new MenuChildItem () { Controller = "Contacts", Action = "AddSingleContact", Area="Member", Title = "Add Single Contact",
                                    Icon = "icon-plus-circle2", IsActive = false },
                                new MenuChildItem () { Controller = "Contacts", Action = "CustomFields", Area="Member", Title = "Custom Fields",
                                    Icon = "icon-list3", IsActive = false },

                            }
                        }

                    },
                    {
                        new MenuItem
                        {
                            Title = "Campaigns",
                            Icon = "icon-paperplane",
                            Children = new List<MenuChildItem>
                            {
                                new MenuChildItem () { Controller = "Campaigns", Action = "Index", Area="Member", Title = "View Campaigns",
                                    Icon = "icon-paperplane", IsActive = false },
                                new MenuChildItem () { Controller = "Campaigns", Action = "Add", Area="Member", Title = "Add Campaign",
                                    Icon = "icon-plus-circle2", IsActive = false },
                                new MenuChildItem () { Controller = "Campaigns", Action = "ViewReport", Area="Member", Title = "View Report",
                                    Icon = "icon-eye", IsActive = false },

                            }
                        }

                    }
                }
            };
        }
    }
}
