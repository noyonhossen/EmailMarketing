using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Common.Services;
using EmailMarketing.Membership.Enums;
using EmailMarketing.Common.Extensions;
using EmailMarketing.Common.Exceptions;
using EmailMarketing.Membership.Exceptions;
using EmailMarketing.Membership.Constants;

namespace EmailMarketing.Membership.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public ApplicationUserService(
            ApplicationUserManager userManager,
            ApplicationRoleManager roleManager,
            ICurrentUserService currentUserService,
            IDateTime dateTime
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public async Task<(IList<ApplicationUser> Items, int Total, int TotalFilter)> GetAllAsync(
            string searchText,
            string orderBy,
            int pageIndex,
            int pageSize)
        {
            var resultItems = new List<ApplicationUser>();
            var resultTotal = 0;
            var resultTotalFilter = 0;

            var columnsMap = new Dictionary<string, Expression<Func<ApplicationUser, object>>>()
            {
                ["fullName"] = v => v.FullName,
                ["userName"] = v => v.UserName,
                ["email"] = v => v.Email
            };

            var query = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).AsQueryable();
            resultTotal = await query.CountAsync();

            query = query.Where(x => !x.IsDeleted &&
                x.Status != EnumApplicationUserStatus.SuperAdmin &&
                (string.IsNullOrWhiteSpace(searchText) || x.FullName.Contains(searchText) ||
                x.UserName.Contains(searchText) || x.Email.Contains(searchText)));

            resultTotalFilter = await query.CountAsync();
            query = query.ApplyOrdering(columnsMap, orderBy);
            query = query.ApplyPaging(pageIndex, pageSize);
            resultItems = (await query.AsNoTracking().ToListAsync());

            return (resultItems, resultTotal, resultTotalFilter);
        }

        public async Task<(IList<ApplicationUser> Items, int Total, int TotalFilter)> GetAllAdminAsync(
            string searchText,
            string orderBy,
            int pageIndex,
            int pageSize)
        {
            var resultItems = new List<ApplicationUser>();
            var resultTotal = 0;
            var resultTotalFilter = 0;

            var columnsMap = new Dictionary<string, Expression<Func<ApplicationUser, object>>>()
            {
                ["fullName"] = v => v.FullName,
                ["userName"] = v => v.UserName,
                ["email"] = v => v.Email
            };

            var query = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).AsQueryable();
            resultTotal = await query.CountAsync();

            query = query.Where(x => !x.IsDeleted && x.UserRoles.Any(ur => ur.Role.Name == ConstantsValue.UserRoleName.Admin) &&
                x.Status != EnumApplicationUserStatus.SuperAdmin &&
                (string.IsNullOrWhiteSpace(searchText) || x.FullName.Contains(searchText) ||
                x.UserName.Contains(searchText) || x.Email.Contains(searchText)));

            resultTotalFilter = await query.CountAsync();
            query = query.ApplyOrdering(columnsMap, orderBy);
            query = query.ApplyPaging(pageIndex, pageSize);
            resultItems = (await query.AsNoTracking().ToListAsync());

            return (resultItems, resultTotal, resultTotalFilter);
        }

        public async Task<ApplicationUser> GetByIdAsync(Guid id)
        {
            var query = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).AsQueryable();

            var user = await query.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), id);
            }

            return user;
        }

        public async Task<ApplicationUser> GetByUserNameAsync(string userName)
        {
            var query = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).AsQueryable();

            var user = await query.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), userName);
            }

            return user;
        }

        public async Task<Guid> AddAsync(ApplicationUser entity, Guid userRoleId, string newPassword)
        {
            var isExists = await this.IsExistsUserNameAsync(entity.UserName, entity.Id);
            if (isExists)
            {
                throw new DuplicationException(nameof(entity.Email));
            }

            entity.Status = EnumApplicationUserStatus.GeneralUser;
            entity.Created = _dateTime.Now;
            entity.CreatedBy = _currentUserService.UserId;

            var userSaveResult = await _userManager.CreateAsync(entity, newPassword);

            if (!userSaveResult.Succeeded)
            {
                throw new IdentityValidationException(userSaveResult.Errors);
            };

            // Add New User Role
            var user = await _userManager.FindByNameAsync(entity.UserName);
            var role = await _roleManager.FindByIdAsync(userRoleId.ToString());

            if (role == null)
            {
                throw new NotFoundException(nameof(ApplicationRole), userRoleId);
            }

            var roleSaveResult = await _userManager.AddToRoleAsync(user, role.Name);

            if (!roleSaveResult.Succeeded)
            {
                throw new IdentityValidationException(roleSaveResult.Errors);
            };

            return user.Id;
        }

        public async Task<Guid> AddAsync(ApplicationUser entity, string userRoleName, string newPassword)
        {
            var isExists = await this.IsExistsUserNameAsync(entity.UserName, entity.Id);
            if (isExists)
            {
                throw new DuplicationException(nameof(entity.Email));
            }

            entity.Status = EnumApplicationUserStatus.GeneralUser;
            entity.Created = _dateTime.Now;
            entity.CreatedBy = _currentUserService.UserId;

            var userSaveResult = await _userManager.CreateAsync(entity, newPassword);

            if (!userSaveResult.Succeeded)
            {
                throw new IdentityValidationException(userSaveResult.Errors);
            };

            // Add New User Role
            var user = await _userManager.FindByNameAsync(entity.UserName);
            var role = await _roleManager.FindByNameAsync(userRoleName);

            if (role == null)
            {
                throw new NotFoundException(nameof(ApplicationRole), userRoleName);
            }

            var roleSaveResult = await _userManager.AddToRoleAsync(user, role.Name);

            if (!roleSaveResult.Succeeded)
            {
                throw new IdentityValidationException(roleSaveResult.Errors);
            };

            return user.Id;
        }

        public async Task<Guid> UpdateAsync(ApplicationUser entity, Guid userRoleId)
        {
            var user = await this._userManager.FindByIdAsync(entity.Id.ToString());

            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), entity.Id);
            }

            var isExists = await this.IsExistsUserNameAsync(entity.UserName, entity.Id);
            if (isExists)
            {
                throw new DuplicationException(nameof(entity.Email));
            }

            user.FullName = entity.FullName;
            user.UserName = entity.UserName;
            user.Email = entity.Email;
            user.PhoneNumber = entity.PhoneNumber;
            user.Address = entity.Address;
            user.DateOfBirth = entity.DateOfBirth;
            user.Gender = entity.Gender;
            user.ImageUrl = entity.ImageUrl ?? user.ImageUrl;
            user.LastModified = _dateTime.Now;
            user.LastModifiedBy = _currentUserService.UserId;

            var userSaveResult = await _userManager.UpdateAsync(user);

            if (!userSaveResult.Succeeded)
            {
                throw new IdentityValidationException(userSaveResult.Errors);
            };

            // Remove Previous User Role
            var previousUserRoles = await _userManager.GetRolesAsync(user);
            if (previousUserRoles.Any())
            {
                var roleRemoveResult = await _userManager.RemoveFromRolesAsync(user, previousUserRoles);

                if (!roleRemoveResult.Succeeded)
                {
                    throw new IdentityValidationException(roleRemoveResult.Errors);
                };

            }

            // Add New User Role
            var role = await _roleManager.FindByIdAsync(userRoleId.ToString());

            if (role == null)
            {
                throw new NotFoundException(nameof(ApplicationRole), userRoleId);
            }

            var roleSaveResult = await _userManager.AddToRoleAsync(user, role.Name);

            if (!roleSaveResult.Succeeded)
            {
                throw new IdentityValidationException(roleSaveResult.Errors);
            };


            return user.Id;
        }

        public async Task<Guid> UpdateAsync(ApplicationUser entity)
        {
            var user = await this._userManager.FindByIdAsync(entity.Id.ToString());

            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), entity.Id);
            }

            var isExists = await this.IsExistsUserNameAsync(entity.UserName, entity.Id);
            if (isExists)
            {
                throw new DuplicationException(nameof(entity.Email));
            }

            user.FullName = entity.FullName;
            user.UserName = entity.UserName;
            user.Email = entity.Email;
            user.PhoneNumber = entity.PhoneNumber;
            user.Address = entity.Address;
            user.DateOfBirth = entity.DateOfBirth;
            user.Gender = entity.Gender;
            user.ImageUrl = entity.ImageUrl ?? user.ImageUrl;
            user.LastModified = _dateTime.Now;
            user.LastModifiedBy = _currentUserService.UserId;

            var userSaveResult = await _userManager.UpdateAsync(user);

            if (!userSaveResult.Succeeded)
            {
                throw new IdentityValidationException(userSaveResult.Errors);
            };

            return user.Id;
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), id);
            }

            user.IsDeleted = true;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new IdentityValidationException(result.Errors);
            };

            return user.FullName;
        }

        public async Task<string> ActiveInactiveAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), id);
            }

            user.IsActive = !user.IsActive;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new IdentityValidationException(result.Errors);
            };

            return user.FullName;
        }

        public async Task<string> BlockUnblockAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), id);
            }

            user.IsBlocked = !user.IsBlocked;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new IdentityValidationException(result.Errors);
            };

            return user.FullName;
        }

        public async Task<string> ResetPassword(ApplicationUser entity, string newPassword)
        {
            entity.PasswordHash = newPassword;
            entity.PasswordChangedCount = 0;
            await UpdateAsync(entity);
            return entity.FullName;
        }

        public async Task<IList<(Guid Value, string Text)>> GetAllForSelectAsync()
        {
            return _userManager.Users.Where(x => x.IsActive && !x.IsDeleted && !x.IsBlocked && x.Status != EnumApplicationUserStatus.SuperAdmin).OrderBy(x => x.FullName)
                                .AsEnumerable().Select(s => (Value: s.Id, Text: s.FullName)).ToList();
        }

        public async Task<bool> IsExistsUserNameAsync(string name, Guid id)
        {
            var result = await _userManager.Users.AnyAsync(x => x.UserName.ToLower() == name.ToLower() && x.Id != id && !x.IsDeleted);
            return result;
        }

        public async Task<bool> IsExistsEmailAsync(string email, Guid id)
        {
            var result = await _userManager.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower() && x.Id != id && !x.IsDeleted);
            return result;
        }
    }
}
