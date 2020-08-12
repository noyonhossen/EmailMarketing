//using Autofac.Extras.Moq;
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
