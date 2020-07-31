﻿using Autofac;
using EmailMarketing.Framework.Extensions;
using EmailMarketing.Framework.Menus;
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
        public MemberBaseModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            SetupMenu();
        }

        public MemberBaseModel()
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
                            Title = "Groups",
                            Icon = "icon-user-tie",
                            Children = new List<MenuChildItem>
                            {
                                new MenuChildItem () { Controller = "Groups", Action = "Index", Area="Member", Title = "View Groups",
                                    Icon = "icon-user-tie", IsActive = false },
                                new MenuChildItem () { Controller = "Groups", Action = "Add", Area="Member", Title = "Add Group",
                                    Icon = "icon-user-tie", IsActive = false },
                             
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
                                    Icon = "icon-user-tie", IsActive = false },
                                new MenuChildItem () { Controller = "Contacts", Action = "ManageUploads", Area="Member", Title = "Manage Uploads",
                                    Icon = "icon-user-tie", IsActive = false },
                                new MenuChildItem () { Controller = "Contacts", Action = "UploadContacts", Area="Member", Title = "Upload Contact",
                                    Icon = "icon-user-tie", IsActive = false },
                                new MenuChildItem () { Controller = "Contacts", Action = "CustomFields", Area="Member", Title = "Custom Fields",
                                    Icon = "icon-user-tie", IsActive = false },

                            }
                        }

                    }
                }
            };
        }
    }
}