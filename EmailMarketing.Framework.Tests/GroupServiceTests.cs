
﻿using Autofac.Extras.Moq;
using EmailMarketing.Common.Exceptions;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Repositories.Group;
using EmailMarketing.Framework.Services.Groups;
using EmailMarketing.Framework.UnitOfWork.Group;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EmailMarketing.Common.Extensions;


namespace EmailMarketing.Framework.Tests
{
    [ExcludeFromCodeCoverage]
    public class GroupServiceTests
    {
        private AutoMock _mock;
        private Mock<IGroupRepository> _groupRepositoryMock;
        private Mock<IGroupUnitOfWork> _groupUnitOfWorkMock;
        private IGroupService _groupService;

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
            _groupRepositoryMock = _mock.Mock<IGroupRepository>();
            _groupUnitOfWorkMock = _mock.Mock<IGroupUnitOfWork>();
            _groupService = _mock.Create<GroupService>();
        }

        [TearDown]
        public void Clean()
        {
            _groupRepositoryMock.Reset();
            _groupUnitOfWorkMock.Reset();
        }

        [Test]
        public void GetByIdAsync_GroupId_ReturnsGroupObject()
        {
            //Arrange
            var group = new Group
            {
                Id = 1,
                Name = "Friends"
            };
            var id = 1;

            _groupUnitOfWorkMock.Setup(x => x.GroupRepository).Returns(_groupRepositoryMock.Object);
            _groupRepositoryMock.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(group)).Verifiable();
            //Act
            var result = _groupService.GetByIdAsync(id);
            var a = result.Result;
            //Assert
            a.ShouldBe(group);
            _groupRepositoryMock.Verify();
        }
        //[Test]
        //public void GetAllAsync_GroupLists_GetAllGroup()
        //{
        //    //Arrange
        //    var group = new List<Group>
        //    {
        //        new Group { Id = 1, Name = "Friends" },
        //        new Group {Id = 2, Name = "Colleague"},
        //        new Group {Id = 3, Name = "Employee"},
        //        new Group {Id = 4, Name = "Managars"},
        //    };

        //    int total = 4, totalFilter = 3;

        //    string searchText = "", orderBy = "Name";

        //    int pageIndex = 1, pageSize = 10;
        //    var columnsMap = new Dictionary<string, Expression<Func<Entities.Group, object>>>()
        //    {
        //        ["name"] = v => v.Name
        //    };


        //    var groupToMatch = new List<Group>
        //    {
        //        new Group { Id = 1, Name = "Friends" },
        //        new Group {Id = 2, Name = "Colleague"},
        //        new Group {Id = 3, Name = "Employee"},
        //        new Group {Id = 4, Name = "Managars"},
        //    };

        //    _groupUnitOfWorkMock.Setup(x => x.GroupRepository).Returns(_groupRepositoryMock.Object);
        //    _groupRepositoryMock.Setup(x => x.GetAsync<Group>(y => y.  )).Returns(Task.CompletedTask).Verifiable();
        //    //_groupRepositoryMock.Setup(x => x.GetAsync<Group>(
        //    //    x => x, x => x.Name.Contains(searchText),
        //    //    x => x.ApplyOrdering(columnsMap, orderBy), null,
        //    //    pageIndex, pageSize, true)).Returns(Task.(group)).Verifiable();



        //    //Act
        //    //_groupService.GetAllAsync(group);


        //    //Assert
        //    _groupRepositoryMock.VerifyAll();
        //    _groupUnitOfWorkMock.VerifyAll();
        //}

        [Test]
        public void AddAsync_GroupAlreadyExists_ThrowsException()
        {
            //Arrange
            var group = new Group
            {
                Id = 1,
                Name = "Friends"
            };

            var groupToMatch = new Group
            {
                Id = 2,
                Name = "Friends"
            };

            _groupUnitOfWorkMock.Setup(x => x.GroupRepository).Returns(_groupRepositoryMock.Object);

            _groupRepositoryMock.Setup(x => x.IsExistsAsync(
                It.Is<Expression<Func<Group, bool>>>(y => y.Compile()(groupToMatch)))).Returns(Task.FromResult(true)).Verifiable();


            //Act
            Should.Throw<DuplicationException>(() =>
                _groupService.AddAsync(group)
            );


            //Assert
            _groupRepositoryMock.Verify();
        }

        [Test]
        public void AddAsync_GroupDoesNotExists_SaveGroup()
        {
            //Arrange
            var group = new Group
            {
                Id = 1,
                Name = "Friends"
            };

            var groupToMatch = new Group
            {
                Id = 2,
                Name = "Friends"
            };

            _groupUnitOfWorkMock.Setup(x => x.GroupRepository).Returns(_groupRepositoryMock.Object);

            _groupRepositoryMock.Setup(x => x.IsExistsAsync(
                It.Is<Expression<Func<Group, bool>>>(y => y.Compile()(groupToMatch)))).Returns(Task.FromResult(false)).Verifiable();

            _groupRepositoryMock.Setup(x => x.AddAsync(It.Is<Group>(y => y.Id == group.Id))).Returns(Task.CompletedTask).Verifiable();
            _groupUnitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask).Verifiable();

            //Act
            _groupService.AddAsync(group);


            //Assert
            _groupRepositoryMock.VerifyAll();
            _groupUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public void UpdateAsync_GroupAlreadyExists_ThrowsException()
        {
            //Arrange
            var group = new Group
            {
                Id = 1,
                Name = "Friends"
            };

            var groupToMatch = new Group
            {
                Id = 2,
                Name = "Friends"
            };

            _groupUnitOfWorkMock.Setup(x => x.GroupRepository).Returns(_groupRepositoryMock.Object);

            _groupRepositoryMock.Setup(x => x.IsExistsAsync(
                It.Is<Expression<Func<Group, bool>>>(y => y.Compile()(groupToMatch)))).Returns(Task.FromResult(true)).Verifiable();


            //Act
            Should.Throw<DuplicationException>(() =>
                _groupService.UpdateAsync(group)
            );


            //Assert
            _groupRepositoryMock.Verify();
        }

        [Test]
        public void UpdateAsync_GroupDoesNotExists_UpdateGroup()
        {
            //Arrange
            var group = new Group
            {
                Id = 1,
                Name = "Friends"
            };

            var groupToMatch = new Group
            {
                Id = 2,
                Name = "Friends"
            };

            var id = 1;

            _groupUnitOfWorkMock.Setup(x => x.GroupRepository).Returns(_groupRepositoryMock.Object);

            _groupRepositoryMock.Setup(x => x.IsExistsAsync(
                It.Is<Expression<Func<Group, bool>>>(y => y.Compile()(groupToMatch)))).Returns(Task.FromResult(false)).Verifiable();

            _groupRepositoryMock.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(group)).Verifiable();

            _groupRepositoryMock.Setup(x => x.UpdateAsync(It.Is<Group>(y => y.Id == group.Id))).Returns(Task.CompletedTask).Verifiable();
            _groupUnitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask).Verifiable();

            //Act
            _groupService.UpdateAsync(group);


            //Assert
            _groupRepositoryMock.VerifyAll();
            _groupUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public void DeleteAsync_GroupIdExists_DeleteGroup()
        {
            //Arrange
            var group = new Group
            {
                Id = 1,
                Name = "Friends"
            };

            _groupUnitOfWorkMock.Setup(x => x.GroupRepository).Returns(_groupRepositoryMock.Object);

            _groupRepositoryMock.Setup(x => x.GetByIdAsync(group.Id)).Returns(Task.FromResult(group)).Verifiable();

            _groupRepositoryMock.Setup(x => x.DeleteAsync(group.Id)).Returns(Task.CompletedTask).Verifiable();
            _groupUnitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask).Verifiable();

            //Act
            var result = _groupService.DeleteAsync(group.Id);


            //Assert
            result.Result.ShouldBe(group);
            _groupRepositoryMock.VerifyAll();
            _groupUnitOfWorkMock.VerifyAll();
        }        
        
    }
}

﻿//using Autofac.Extras.Moq;
//using EmailMarketing.Common.Exceptions;
//using EmailMarketing.Framework.Entities;
//using EmailMarketing.Framework.Repositories.Group;
//using EmailMarketing.Framework.Services.Groups;
//using EmailMarketing.Framework.UnitOfWork;
//using Moq;
//using NUnit.Framework;
//using Shouldly;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics.CodeAnalysis;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace EmailMarketing.Framework.Tests
//{
//    [ExcludeFromCodeCoverage]
//    public class GroupServiceTests
//    {
//        private AutoMock _mock;
//        private Mock<IGroupRepository> _groupRepositoryMock;
//        private Mock<IGroupUnitOfWork> _groupUnitOfWorkMock;
//        private IGroupService _groupService;


//        [OneTimeSetUp]
//        public void ClassSetup()
//        {
//            _mock = AutoMock.GetLoose();
//        }

//        [OneTimeTearDown]
//        public void ClassCleanUp()
//        {
//            _mock?.Dispose();
//        }

//        [SetUp]
//        public void Setup()
//        {
//            _groupUnitOfWorkMock = _mock.Mock<IGroupUnitOfWork>();
//            _groupRepositoryMock = _mock.Mock<IGroupRepository>();
//            _groupService = _mock.Create<GroupService>();
//        }

//        [TearDown]
//        public void Clean()
//        {
//            _groupUnitOfWorkMock.Reset();
//            _groupRepositoryMock.Reset();
//        }

//        [Test]
//        public async Task AddAsync_GroupAlreadyExists_ThrowsException()
//        {
            
//            // Arrange
//            var group = new Group()
//            {
//                Id = 1,
//                Name = "TeamA"
//                //UserId = Guid.NewGuid(),
//                //Created = DateTime.Now,
//                //CreatedBy = null,
//                //IsActive = true,
//                //IsDeleted = false,
//                //LastModified = DateTime.Now,
//                //LastModifiedBy = null
//            };

//            var groupToMatch = new Group
//            {
//                Id = 2,
//                Name = "TeamA"
//            };

//            _groupUnitOfWorkMock.Setup(x => x.GroupRepository)
//                .Returns(_groupRepositoryMock.Object);

//            _groupRepositoryMock.Setup(x => x.IsExistsAsync(
//                It.Is<Expression<Func<Group, bool>>>(y => y.Compile()(groupToMatch))))
//                .Returns(new Task<bool>(() => true)).Verifiable();

//            // Act
            
//              Should.Throw<DuplicationException>(async() =>
//               await _groupService.AddAsync(group)
//           );

//            // Assert
//            _groupRepositoryMock.VerifyAll();
//        }

//        #region NotTestedYet
//        public void GetAllAsync_Test()
//        {
//            // Arrange
//            List<Group> group = new List<Group>()
//            {
//                new Group()
//                {
//                    Id = 1,
//                    UserId = Guid.NewGuid(),
//                    Name = "TeamA",
//                    Created = DateTime.Now,
//                    CreatedBy = null,
//                    IsActive = true,
//                    IsDeleted = false,
//                    LastModified = DateTime.Now,
//                    LastModifiedBy = null

//                },
//                new Group()
//                {
//                    Id = 1,
//                    UserId = Guid.NewGuid(),
//                    Name = "TeamB",
//                    Created = DateTime.Now,
//                    CreatedBy = null,
//                    IsActive = true,
//                    IsDeleted = false,
//                    LastModified = DateTime.Now,
//                    LastModifiedBy = null

//                },
//                new Group()
//                {
//                    Id = 1,
//                    UserId = Guid.NewGuid(),
//                    Name = "TeamB",
//                    Created = DateTime.Now,
//                    CreatedBy = null,
//                    IsActive = true,
//                    IsDeleted = false,
//                    LastModified = DateTime.Now,
//                    LastModifiedBy = null

//                }

//            };

//            _groupUnitOfWorkMock.Setup(x => x.GroupRepository)
//                .Returns(_groupRepositoryMock.Object);

//            //_groupRepositoryMock.Setup(x => x.GetAsync<Group>(
//            //    It.Is<Expression<Func<Group, bool>>>(y => y.Compile()(new List<Group>(() => group))))
//            //    .Returns((group,2,0)).Verifiable());

//            // Act


//        }
//        #endregion

//    }
//}

