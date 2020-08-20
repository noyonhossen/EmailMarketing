
using Autofac.Extras.Moq;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.Contacts;
using EmailMarketing.Framework.Enums;
using EmailMarketing.Framework.Repositories.Contacts;
using EmailMarketing.Framework.Repositories.Groups;
using EmailMarketing.Framework.Services.Contacts;
using EmailMarketing.Framework.UnitOfWorks.Contacts;
using EmailMarketing.Framework.UnitOfWorks.Groups;
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

namespace EmailMarketing.Framework.Tests.Services
{
    [ExcludeFromCodeCoverage]
    public class ContactExportServiceTests
    {
        private AutoMock _mock;
        private Mock<IGroupRepository> _groupRepositoryMock;
        private Mock<IGroupContactRepository> _groupContactRepositoryMock;
        private Mock<IContactRepository> _contactRepositoryMock;
        private Mock<IDownloadQueueRepository> _downloadQueueRepositoryMock;

        private Mock<IContactExportUnitOfWork> _contactExportUnitOfWorkMock;
        private Mock<IContactUnitOfWork> _contactUnitOfWorkMock;
        private Mock<IGroupUnitOfWork> _groupUnitOfWorkMock;

        private IContactExportService _contactExportService;

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
            _groupContactRepositoryMock = _mock.Mock<IGroupContactRepository>();
            _contactRepositoryMock = _mock.Mock<IContactRepository>();
            _downloadQueueRepositoryMock = _mock.Mock<IDownloadQueueRepository>();

            _groupUnitOfWorkMock = _mock.Mock<IGroupUnitOfWork>();
            _contactExportUnitOfWorkMock = _mock.Mock<IContactExportUnitOfWork>();
            _contactUnitOfWorkMock = _mock.Mock<IContactUnitOfWork>();
            
            _contactExportService = _mock.Create<ContactExportService>();
        }

        [TearDown]
        public void Clean()
        {
            _groupRepositoryMock.Reset();
            _groupContactRepositoryMock.Reset();
            _contactRepositoryMock.Reset();
            _downloadQueueRepositoryMock.Reset();

            _groupUnitOfWorkMock.Reset();
            _contactExportUnitOfWorkMock.Reset();
            _contactUnitOfWorkMock.Reset();
        }

        [Test]
        public void GetContactById_ForContactId_ReturnsContactObject()
        {
            //Arrange
            var contact = new Contact
            {
                Id = 1,
                Email = "devskillTeamA@gmail.com",
                UserId = Guid.Empty,
                ContactUploadId = 2
            };
            var id = 1;
            _contactUnitOfWorkMock.Setup(x => x.ContactRepository).Returns(_contactRepositoryMock.Object);
            _contactRepositoryMock.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(contact)).Verifiable();
            
            //Act
            var returnedContact = _contactExportService.GetContactByIdAsync(id);
            var result = returnedContact.Result;

            //Assert
            result.ShouldBe(contact);
            _groupRepositoryMock.Verify();
        }

        [Test]
        public void UpdateDownloadQueueAync_DownloadQueueItemExist_UpdateItem()
        {
            //Arrange
            var item = new DownloadQueue
            {
                Id = 1,
                FileUrl = "C:\\EmailMarketingTeamA",
                DownloadQueueFor = DownloadQueueFor.ContactAllExport,
                IsProcessing = true
            };
            var itemToUpdate = new DownloadQueue
            {
                Id = 1,
                FileUrl = "C:\\EmailMarketingTeamA",
                FileName = "ContactsExportReport.xlsx",
                DownloadQueueFor = DownloadQueueFor.ContactAllExport,
                IsProcessing = false
            };

            _contactExportUnitOfWorkMock.Setup(x => x.DownloadQueueRepository).Returns(_downloadQueueRepositoryMock.Object);
            _downloadQueueRepositoryMock.Setup(x => x.UpdateAsync(itemToUpdate))
                .Returns(Task.CompletedTask).Verifiable();

            //Act
            var result = _contactExportService.UpdateDownloadQueueAync(item);

            //Assert
            
        }




        //public void GetDownloadQueue_ForAllProcessingDownlaodQueue_ReturnsDownloadQueueList()
        //{
        //    var list = new List<DownloadQueue>()
        //    {
        //        new DownloadQueue()
        //        {
        //            Id = 1,
        //            FileUrl = "C:\\EmailMarketingTeamA",
        //            DownloadQueueFor = DownloadQueueFor.ContactAllExport,
        //            IsProcessing = true
        //        },
        //        new DownloadQueue()
        //        {
        //            Id = 2,
        //            FileUrl = "C:\\EmailMarketingTeamA",
        //            DownloadQueueFor = DownloadQueueFor.ContactAllExport,
        //            IsProcessing = true
        //        },
        //        new DownloadQueue()
        //        {
        //            Id = 3,
        //            FileUrl = "C:\\EmailMarketingTeamA",
        //            DownloadQueueFor = DownloadQueueFor.ContactAllExport,
        //            IsProcessing = false
        //        }
        //    };

        //    var groupToMatch = new List<DownloadQueue>
        //    {
        //        new DownloadQueue { Id = 1, FileUrl = "C:\\EmailMarketingTeamA",DownloadQueueFor = DownloadQueueFor.ContactAllExport },
        //        new DownloadQueue { Id = 2, FileUrl = "C:\\EmailMarketingTeamA",DownloadQueueFor = DownloadQueueFor.ContactAllExport },
        //        //new DownloadQueue { Id = 3, FileUrl = "C:\\EmailMarketingTeamA",DownloadQueueFor = DownloadQueueFor.ContactAllExport }
        //    };

        //    _contactExportUnitOfWorkMock.Setup(x => x.DownloadQueueRepository).Returns(_downloadQueueRepositoryMock.Object);
        //    _downloadQueueRepositoryMock.Setup(x => x.GetAsync<IList<DownloadQueue>>(It.Is<Expression<Func<DownloadQueue, DownloadQueue>>>(y => y.Compile()(new DownloadQueue())), It.Is<Expression<Func<DownloadQueue,DownloadQueue>>>(y => y.Compile()(groupToMatch)))
        //        .Returns(Task.CompletedTask).Verifiable();

        //}
        //[Test]
        //public void AddAsync_GroupAlreadyExists_ThrowsException()
        //{
        //    //Arrange
        //    var group = new Group
        //    {
        //        Id = 1,
        //        Name = "Friends"
        //    };

        //    var groupToMatch = new Group
        //    {
        //        Id = 2,
        //        Name = "Friends"
        //    };

        //    _groupUnitOfWorkMock.Setup(x => x.GroupRepository).Returns(_groupRepositoryMock.Object);

        //    _groupRepositoryMock.Setup(x => x.IsExistsAsync(
        //        It.Is<Expression<Func<Group, bool>>>(y => y.Compile()(groupToMatch)))).Returns(Task.FromResult(true)).Verifiable();


        //    //Act
        //    Should.Throw<DuplicationException>(() =>
        //        _groupService.AddAsync(group)
        //    );


        //    //Assert
        //    _groupRepositoryMock.Verify();
        //}

        //[Test]
        //public void AddAsync_GroupDoesNotExists_SaveGroup()
        //{
        //    //Arrange
        //    var group = new Group
        //    {
        //        Id = 1,
        //        Name = "Friends"
        //    };

        //    var groupToMatch = new Group
        //    {
        //        Id = 2,
        //        Name = "Friends"
        //    };

        //    _groupUnitOfWorkMock.Setup(x => x.GroupRepository).Returns(_groupRepositoryMock.Object);

        //    _groupRepositoryMock.Setup(x => x.IsExistsAsync(
        //        It.Is<Expression<Func<Group, bool>>>(y => y.Compile()(groupToMatch)))).Returns(Task.FromResult(false)).Verifiable();

        //    _groupRepositoryMock.Setup(x => x.AddAsync(It.Is<Group>(y => y.Id == group.Id))).Returns(Task.CompletedTask).Verifiable();
        //    _groupUnitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask).Verifiable();

        //    //Act
        //    _groupService.AddAsync(group);


        //    //Assert
        //    _groupRepositoryMock.VerifyAll();
        //    _groupUnitOfWorkMock.VerifyAll();
        //}

        //[Test]
        //public void UpdateAsync_GroupAlreadyExists_ThrowsException()
        //{
        //    //Arrange
        //    var group = new Group
        //    {
        //        Id = 1,
        //        Name = "Friends"
        //    };

        //    var groupToMatch = new Group
        //    {
        //        Id = 2,
        //        Name = "Friends"
        //    };

        //    _groupUnitOfWorkMock.Setup(x => x.GroupRepository).Returns(_groupRepositoryMock.Object);

        //    _groupRepositoryMock.Setup(x => x.IsExistsAsync(
        //        It.Is<Expression<Func<Group, bool>>>(y => y.Compile()(groupToMatch)))).Returns(Task.FromResult(true)).Verifiable();


        //    //Act
        //    Should.Throw<DuplicationException>(() =>
        //        _groupService.UpdateAsync(group)
        //    );


        //    //Assert
        //    _groupRepositoryMock.Verify();
        //}

        //[Test]
        //public void UpdateAsync_GroupDoesNotExists_UpdateGroup()
        //{
        //    //Arrange
        //    var group = new Group
        //    {
        //        Id = 1,
        //        Name = "Friends"
        //    };

        //    var groupToMatch = new Group
        //    {
        //        Id = 2,
        //        Name = "Friends"
        //    };

        //    var id = 1;

        //    _groupUnitOfWorkMock.Setup(x => x.GroupRepository).Returns(_groupRepositoryMock.Object);

        //    _groupRepositoryMock.Setup(x => x.IsExistsAsync(
        //        It.Is<Expression<Func<Group, bool>>>(y => y.Compile()(groupToMatch)))).Returns(Task.FromResult(false)).Verifiable();

        //    _groupRepositoryMock.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(group)).Verifiable();

        //    _groupRepositoryMock.Setup(x => x.UpdateAsync(It.Is<Group>(y => y.Id == group.Id))).Returns(Task.CompletedTask).Verifiable();
        //    _groupUnitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask).Verifiable();

        //    //Act
        //    _groupService.UpdateAsync(group);


        //    //Assert
        //    _groupRepositoryMock.VerifyAll();
        //    _groupUnitOfWorkMock.VerifyAll();
        //}

        //[Test]
        //public void DeleteAsync_GroupIdExists_DeleteGroup()
        //{
        //    //Arrange
        //    var group = new Group
        //    {
        //        Id = 1,
        //        Name = "Friends"
        //    };

        //    _groupUnitOfWorkMock.Setup(x => x.GroupRepository).Returns(_groupRepositoryMock.Object);

        //    _groupRepositoryMock.Setup(x => x.GetByIdAsync(group.Id)).Returns(Task.FromResult(group)).Verifiable();

        //    _groupRepositoryMock.Setup(x => x.DeleteAsync(group.Id)).Returns(Task.CompletedTask).Verifiable();
        //    _groupUnitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask).Verifiable();

        //    //Act
        //    var result = _groupService.DeleteAsync(group.Id);


        //    //Assert
        //    result.Result.ShouldBe(group);
        //    _groupRepositoryMock.VerifyAll();
        //    _groupUnitOfWorkMock.VerifyAll();
        //}

    }
}

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

