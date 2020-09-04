
﻿using Autofac.Extras.Moq;
using EmailMarketing.Common.Exceptions;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Repositories.Groups;
using EmailMarketing.Framework.Services.Groups;
using EmailMarketing.Framework.UnitOfWorks.Groups;
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
using EmailMarketing.Framework.Entities.Groups;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query;

namespace EmailMarketing.Framework.Tests.Services.Groups
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
        [Test]
        public void GetAllAsync_GroupLists_GetAllGroup()
        {
            //Arrange
            int total = 4, totalFilter = 3;
            string searchText = "", orderBy = "Name";
            int pageIndex = 1, pageSize = 10;

            var group = new List<Group>
            {
                new Group { Id = 1, Name = "Friends" },
                new Group {Id = 2, Name = "Colleague"},
                new Group {Id = 3, Name = "Employee"},
                new Group {Id = 4, Name = "Managars"},
            };

            var groupToMatch = new List<Group>
            {
                new Group { Id = 1, Name = "Friends" },
                new Group {Id = 2, Name = "Colleague"},
                new Group {Id = 3, Name = "Employee"},
                new Group {Id = 4, Name = "Managars"},
            };

            _groupUnitOfWorkMock.Setup(x => x.GroupRepository).Returns(_groupRepositoryMock.Object);

            _groupRepositoryMock.Setup(x => x.GetAsync(
                It.Is<Expression<Func<Group, Group>>>(y => y.Compile()(new Group()) is Group),
                It.Is<Expression<Func<Group, bool>>>(y => y.Compile()(new Group() { Name = "Employee" })),
                It.IsAny<Func<IQueryable<Group>, IOrderedQueryable<Group>>>(),
                It.IsAny<Func<IQueryable<Group>, IIncludableQueryable<Group, object>>>(),
                pageIndex, pageSize, true)).ReturnsAsync((group, total, totalFilter)).Verifiable();



            //Act
            _groupService.GetAllAsync(searchText, orderBy, pageIndex, pageSize);


            //Assert
            _groupRepositoryMock.VerifyAll();
            _groupUnitOfWorkMock.VerifyAll();
        }
        [Test]
        public void GetAllGroupForSelectAsync_GroupList()
        {
            //Arrange
            int Value = 1, ContactCount = 3;
            //Guid userId = 1;
            string Text = "", orderBy = "Name";
            int pageIndex = 1, pageSize = 10;

            var group = new List<Group>
            {
                new Group { Id = 1, Name = "Friends" },
                new Group {Id = 2, Name = "Colleague"},
                new Group {Id = 3, Name = "Employee"},
                new Group {Id = 4, Name = "Managars"},
            };

            var groupToMatch = new List<Group>
            {
                new Group { Id = 1, Name = "Friends" },
                new Group {Id = 2, Name = "Colleague"},
                new Group {Id = 3, Name = "Employee"},
                new Group {Id = 4, Name = "Managars"},
            };

            //_groupUnitOfWorkMock.Setup(x => x.GroupRepository).Returns(_groupRepositoryMock.Object);

            //_groupRepositoryMock.Setup(x => x.GetAsync(
            //    It.Is<Expression<Func<Group, Group>>>(y => y.Compile()(new Group()) is Group),
            //    It.Is<Expression<Func<Group, bool>>>(y => y.Compile()(new Group() { Name = "Employee" })),
            //    It.IsAny<Func<IQueryable<Group>, IOrderedQueryable<Group>>>(),
            //    It.IsAny<Func<IQueryable<Group>, IIncludableQueryable<Group, object>>>();
            //    //pageIndex, pageSize, true)).ReturnsAsync(group).Verifiable();



            //Act
            //_groupService.GetAllAsync(searchText, orderBy, pageIndex, pageSize);


            //Assert
           // _groupRepositoryMock.VerifyAll();
           // _groupUnitOfWorkMock.VerifyAll();
        }
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
