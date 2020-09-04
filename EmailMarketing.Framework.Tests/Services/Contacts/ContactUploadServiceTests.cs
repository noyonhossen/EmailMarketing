using Autofac.Extras.Moq;
using EmailMarketing.Common.Exceptions;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.Contacts;
using EmailMarketing.Framework.Repositories.Contacts;
using EmailMarketing.Framework.Services.Contacts;
using EmailMarketing.Framework.UnitOfWorks.Contacts;
using Microsoft.EntityFrameworkCore.Query;
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

namespace EmailMarketing.Framework.Tests.Services.Contacts
{
    [ExcludeFromCodeCoverage]
    public class ContactUploadServiceTests
    {
        private AutoMock _mock;
        private Mock<IContactUploadRepository> _contactUploadRepositoryMock;
        private Mock<IContactRepository> _contactRepositoryMock;
        private Mock<IContactUploadUnitOfWork> _contactUploadUnitOfWorkMock;
        private IContactUploadService _contactUploadService;


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
            _contactUploadUnitOfWorkMock = _mock.Mock<IContactUploadUnitOfWork>();
            _contactUploadRepositoryMock = _mock.Mock<IContactUploadRepository>();
            _contactRepositoryMock = _mock.Mock<IContactRepository>();
            _contactUploadService = _mock.Create<ContactUploadService>();
        }

        [TearDown]
        public void Clean()
        {
            _contactUploadUnitOfWorkMock.Reset();
            _contactRepositoryMock.Reset();
            _contactUploadRepositoryMock.Reset();
        }

        [Test]
        public async Task ContactExcelImportAsync_SearchByContactUploadId_ReturnContactUploadObject()
        {
            
            // Arrange
            var newContacts = new List<ContactUpload>()
            {
                new ContactUpload()
                {
                    Id = 1,
                    FileUrl = "c/newfolder/example.xlsx"
                }
                ,
                new ContactUpload()
                {

                    Id = 2,
                    FileUrl = "c/newfolder/example1.xlsx"
                }
                    
                //UserId = Guid.NewGuid(),
                //Created = DateTime.Now,
                //CreatedBy = null,
                //IsActive = true,
                //IsDeleted = false,
                //LastModified = DateTime.Now,
                //LastModifiedBy = null
            };

            var newContactToMatch = new ContactUpload
            {
                Id = 2
            };

            _contactUploadUnitOfWorkMock.Setup(x => x.ContactRepository)
                .Returns(_contactRepositoryMock.Object);

            _contactUploadUnitOfWorkMock.Setup(x => x.ContactUploadRepository)
                .Returns(_contactUploadRepositoryMock.Object);

            //_contactUploadRepositoryMock.Setup(x => x.GetFirstOrDefaultAsync(
            //    It.Is<Expression<Func<ContactUpload, bool>>>(y => y.Compile()(newContactToMatch))))
            //    .Returns(new Task<bool>(() => true)).Verifiable();

            // Act

            //Should.Throw<DuplicationException>(async () =>);
             //await _groupService.AddAsync(group)
         //);

            // Assert
            //_groupRepositoryMock.VerifyAll();
        }

        [Test]
        public void GetAllAsync_Test()
        {
            // Arrange

            int total = 4, totalFilter = 3;
            string searchText = "", orderBy = "Name";
            int pageIndex = 1, pageSize = 10;
            Guid userId = Guid.NewGuid();

            var contactUpload = new List<ContactUpload>
            {
                new ContactUpload { Id = 1, FileName = "Friends" },
                new ContactUpload {Id = 2, FileName = "Colleague"},
                new ContactUpload {Id = 3, FileName = "Employee"},
                new ContactUpload {Id = 4, FileName = "Managars"},
            };

            var contactUploadToMatch = new ContactUpload
            {
                Id = 1, FileName = "Friends" ,
                UserId = userId
            };
            var columnsMap = new Dictionary<string, Expression<Func<Entities.Contacts.ContactUpload, object>>>()
            {
                ["created"] = v => v.Created,
                ["fileName"] = v => v.FileName
            };

            _contactUploadUnitOfWorkMock.Setup(x => x.ContactUploadRepository).Returns(_contactUploadRepositoryMock.Object);

            _contactUploadRepositoryMock.Setup(x => x.GetAsync(
                It.Is<Expression<Func<ContactUpload, ContactUpload>>>(y => y.Compile()(new ContactUpload()) is ContactUpload),
                It.Is<Expression<Func<ContactUpload, bool>>>(y => y.Compile()(contactUploadToMatch)),
                It.IsAny<Func<IQueryable<ContactUpload>, IOrderedQueryable<ContactUpload>>>(),
                It.IsAny<Func<IQueryable<ContactUpload>, IIncludableQueryable<ContactUpload, object>>>(),
                pageIndex, pageSize, true)).ReturnsAsync((contactUpload, total, totalFilter)).Verifiable();



            //Act
            _contactUploadService.GetAllAsync(userId, searchText, orderBy, pageIndex, pageSize);


            //Assert
            _contactUploadRepositoryMock.VerifyAll();
            _contactUploadUnitOfWorkMock.VerifyAll();

        }


    }
}
