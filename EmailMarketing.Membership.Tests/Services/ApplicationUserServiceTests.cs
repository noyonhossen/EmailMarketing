using Autofac.Extras.Moq;
using EmailMarketing.Membership.Services;
using EmailMarketing.Membership.Entities;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using EmailMarketing.Common.Exceptions;

namespace EmailMarketing.Framework.Tests
{
    [ExcludeFromCodeCoverage]
    public class ApplicationUserServiceTests
    {
        private AutoMock _mock;
        private Mock<ApplicationUserManager> _userManagerMock;
        private IApplicationUserService _applicationUserServiceMock;


        [OneTimeSetUp]
        public void ClassSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void ClassCleanUp()
        {
            _mock?.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            _applicationUserServiceMock = _mock.Create<ApplicationUserService>();
            //var mockStore = new Mock<Microsoft.AspNetCore.Identity.IUserStore<ApplicationUser>>();

            //var userStoreMock = new Mock<Microsoft.AspNetCore.Identity.IUserStore<ApplicationUser>>();
            //_userManagerMock = new Mock<Microsoft.AspNetCore.Identity.UserManager<ApplicationUser>>(
            //userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

        [TearDown]
        public void Clean()
        {
            //_groupUnitOfWorkMock.Reset();
            //_userManagerMock.Reset();
        }

        //[Test]
        //public async Task AddAsync_GroupAlreadyExists_ThrowsException()
        //{

        //    // Arrange

        //    var ApplicationUser = new ApplicationUser()
        //    {
        //        Id = Guid.Empty,
        //        Email = "abc@gmail.com",
        //        Address = "Bangladesh"

        //        //UserId = Guid.NewGuid(),
        //        //Created = DateTime.Now,
        //        //CreatedBy = null,
        //        //IsActive = true,
        //        //IsDeleted = false,
        //        //LastModified = DateTime.Now,
        //        //LastModifiedBy = null
        //    };

        //    var groupToMatch = new ApplicationUser
        //    {
        //        Email = "abc@gmail.com",
        //    };


        //    var userStore = _mock.Mock<Microsoft.AspNetCore.Identity.IUserStore<ApplicationUser>>();
        //    var _userManagerMock = new Mock<Microsoft.AspNetCore.Identity.UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);



        //    // Act

        //    _userManagerMock.Setup(x => x.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).AsQueryable());
        //    //_applicationUserServiceMock.GetByIdAsync(Guit

        //    //_userManagerMock.Setup(x => x.Users).Returns(applicationUsers);

        //    var result = await _applicationUserServiceMock.GetByIdAsync(Guid.Empty);
        //    result.ShouldBe<ApplicationUser>(ApplicationUser);


        //    // Assert


        //    //_groupRepositoryMock.VerifyAll();
        //}


        [Test]
        public async Task UpdateAsync_GroupAlreadyExists_ThrowsException()
        {

            // Arrange

            var ApplicationUser = new List<ApplicationUser>()
            {
                new  ApplicationUser{
                    Id = Guid.Empty,
                    Email = "abc@gmail.com",
                    Address = "Bangladesh"
                }
                //UserId = Guid.NewGuid(),
                //Created = DateTime.Now,
                //CreatedBy = null,
                //IsActive = true,
                //IsDeleted = false,
                //LastModified = DateTime.Now,
                //LastModifiedBy = null
            }.AsQueryable();

            var ApplicationUser1 = new ApplicationUser
            {
                Id = Guid.Empty,
                Email = "abc@gmail.com",
                Address = "Bangladesh"
            };

            var groupToMatch = new ApplicationUser
            {
                Id = Guid.Empty,
                Email = "abc1@gmail.com",
            };
            var userStore = _mock.Mock<Microsoft.AspNetCore.Identity.IUserStore<ApplicationUser>>();
            var _userManagerMock = new Mock<Microsoft.AspNetCore.Identity.UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);

            // var result = _userManagerMock.Setup(x => x.FindByIdAsync(Guid.Empty.ToString()));
            
            _userManagerMock.Setup(x => x.FindByIdAsync(Guid.Empty.ToString())).Returns(Task.FromResult(new ApplicationUser
            {
                Id = Guid.Empty,
                Email = "abc@gmail.com",
                Address = "Bangladesh"
            })).Verifiable();
            //var result2 = await _applicationUserServiceMock.IsExistsUserNameAsync("yeasinarafat10@gmail.com",Guid.Empty);
            var result2 = await _applicationUserServiceMock.UpdateAsync(groupToMatch);

            // Act

            //var result3 = await _applicationUserServiceMock.IsExistsEmailAsync("yeasinarafat10@gmail.com", Guid.Empty);
            //_applicationUserServiceMock.GetByIdAsync(Guit

            //_userManagerMock.Setup(x => x.Users).Returns(applicationUsers);

            //var result = await _applicationUserServiceMock.GetByIdAsync(Guid.Empty);
            result2.ShouldBe<Guid>(Guid.Empty);
            //result3.ShouldNotBe<DuplicationException>();


            // Assert

        }
        //#region NotTestedYet
        //public void GetAllAsync_Test()
        //{
        //    // Arrange
        //    List<Group> group = new List<Group>()
        //    {
        //        new Group()
        //        {
        //            Id = 1,
        //            UserId = Guid.NewGuid(),
        //            Name = "TeamA",
        //            Created = DateTime.Now,
        //            CreatedBy = null,
        //            IsActive = true,
        //            IsDeleted = false,
        //            LastModified = DateTime.Now,
        //            LastModifiedBy = null

        //        },
        //        new Group()
        //        {
        //            Id = 1,
        //            UserId = Guid.NewGuid(),
        //            Name = "TeamB",
        //            Created = DateTime.Now,
        //            CreatedBy = null,
        //            IsActive = true,
        //            IsDeleted = false,
        //            LastModified = DateTime.Now,
        //            LastModifiedBy = null

        //        },
        //        new Group()
        //        {
        //            Id = 1,
        //            UserId = Guid.NewGuid(),
        //            Name = "TeamB",
        //            Created = DateTime.Now,
        //            CreatedBy = null,
        //            IsActive = true,
        //            IsDeleted = false,
        //            LastModified = DateTime.Now,
        //            LastModifiedBy = null

        //        }

        //    };

        //    _groupUnitOfWorkMock.Setup(x => x.GroupRepository)
        //        .Returns(_groupRepositoryMock.Object);

        //    //_groupRepositoryMock.Setup(x => x.GetAsync<Group>(
        //    //    It.Is<Expression<Func<Group, bool>>>(y => y.Compile()(new List<Group>(() => group))))
        //    //    .Returns((group,2,0)).Verifiable());

        //    // Act


        //}
        //#endregion

    }
}
