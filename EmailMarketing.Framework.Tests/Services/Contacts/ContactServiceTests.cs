using Autofac.Extras.Moq;
using EmailMarketing.Common.Exceptions;
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
    public class ContactServiceTests
    {

        private AutoMock _mock;
        private Mock<IContactRepository> _contactRepositoryMock;
        private Mock<IFieldMapRepository> _fieldMapRepositoryMock;
        private Mock<IContactUnitOfWork> _contactUnitOfWorkMock;
        private IContactService _contactService;
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
            _contactRepositoryMock = _mock.Mock<IContactRepository>();
            _fieldMapRepositoryMock = _mock.Mock<IFieldMapRepository>();
            _contactUnitOfWorkMock = _mock.Mock<IContactUnitOfWork>();
            _contactService = _mock.Create<ContactService>();
        }

        [TearDown]
        public void Clean()
        {
            _contactRepositoryMock.Reset();
            _fieldMapRepositoryMock.Reset();
            _contactUnitOfWorkMock.Reset();
        }
        [Test]
        public void GetByIdAsync_InValidId_ThrowException()
        {
            //Arrange
            int id = 1;

            Contact? contact = null;
            var contactsToMatch = new Contact
            {
                Id = 1
            };

            _contactUnitOfWorkMock.Setup(x => x.ContactRepository)
                .Returns(_contactRepositoryMock.Object);

            _contactRepositoryMock.Setup(x => x.GetFirstOrDefaultAsync(
                It.Is<Expression<Func<Contact, Contact>>>(y => y.Compile()(new Contact()) is Contact),
                It.Is<Expression<Func<Contact, bool>>>(y => y.Compile()(contactsToMatch)),
                It.IsAny<Func<IQueryable<Contact>, IIncludableQueryable<Contact, object>>>(),
                true
                )).ReturnsAsync(contact).Verifiable();

            //Act
            Should.Throw<NotFoundException>(() =>
                _contactService.GetByIdAsync(id)
            );

            //Assert
            _contactRepositoryMock.VerifyAll();
        }
        [Test]
        public void GetByIdAsync_ValidId_ReturnContact()
        {
            //Arrange
            int id = 1;

            var contact = new Contact
            {
                Id = 1,
                Email = "teama@gmail.com"
            };

            var contactsToMatch = new Contact
            {
                Id = 1
            };

            _contactUnitOfWorkMock.Setup(x => x.ContactRepository)
                .Returns(_contactRepositoryMock.Object);

            _contactRepositoryMock.Setup(x => x.GetFirstOrDefaultAsync(
                It.Is<Expression<Func<Contact, Contact>>>(y => y.Compile()(new Contact()) is Contact),
                It.Is<Expression<Func<Contact, bool>>>(y => y.Compile()(contactsToMatch)),
                It.IsAny<Func<IQueryable<Contact>, IIncludableQueryable<Contact, object>>>(),
                true
                )).ReturnsAsync(contact).Verifiable();

            //Act

            var result = _contactService.GetByIdAsync(id);
            result.Result.ShouldBe(contact);

            //Assert
            _contactRepositoryMock.VerifyAll();
        }
        [Test]
        public void DeleteAsync_InValidId_ThrowException()
        {
            //Arrange
            int id = 1;

            Contact? contact = null;
            var contactsToMatch = new Contact
            {
                Id = 1
            };

            _contactUnitOfWorkMock.Setup(x => x.ContactRepository)
                .Returns(_contactRepositoryMock.Object);

            _contactRepositoryMock.Setup(x => x.GetFirstOrDefaultAsync(
                It.Is<Expression<Func<Contact, Contact>>>(y => y.Compile()(new Contact()) is Contact),
                It.Is<Expression<Func<Contact, bool>>>(y => y.Compile()(contactsToMatch)),
                It.IsAny<Func<IQueryable<Contact>, IIncludableQueryable<Contact, object>>>(),
                true
                )).ReturnsAsync(contact).Verifiable();

            //Act
            Should.Throw<NotFoundException>(() =>
                _contactService.DeleteAsync(id)
            );

            //Assert
            _contactRepositoryMock.VerifyAll();
        }
        [Test]
        public void DeleteAsync_ValidId_ReturnContact()
        {
            //Arrange
            int id = 1;

            var contact = new Contact
            {
                Id = 1,
                Email = "teama@gmail.com"
            };
            var contactsToMatch = new Contact
            {
                Id = 1
            };

            _contactUnitOfWorkMock.Setup(x => x.ContactRepository)
                .Returns(_contactRepositoryMock.Object);

            _contactRepositoryMock.Setup(x => x.GetFirstOrDefaultAsync(
                It.Is<Expression<Func<Contact, Contact>>>(y => y.Compile()(new Contact()) is Contact),
                It.Is<Expression<Func<Contact, bool>>>(y => y.Compile()(contactsToMatch)),
                It.IsAny<Func<IQueryable<Contact>, IIncludableQueryable<Contact, object>>>(),
                true
                )).ReturnsAsync(contact).Verifiable();

            _contactRepositoryMock.Setup(x => x.DeleteAsync(id)).Returns(Task.CompletedTask).Verifiable();
            _contactUnitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask).Verifiable();

            //Act
            var result = _contactService.DeleteAsync(id);
            result.Result.ShouldBe(contact);

            //Assert
            _contactUnitOfWorkMock.VerifyAll();
            _contactRepositoryMock.VerifyAll();
        }
        [Test]
        public void GetIdByEmail_ValidEmail_ReturnContact()
        {
            //Arrange
            string email = "n@gmail.com";
            var contact = new Contact
            {
                Id = 1,
                Email = "n@gmail.com",
            };

            var contactToMatch = new Contact
            {
                Id = 1,
                Email = "n@gmail.com",
            };

            _contactUnitOfWorkMock.Setup(x => x.ContactRepository).Returns(_contactRepositoryMock.Object);
            _contactRepositoryMock.Setup(x => x.GetFirstOrDefaultAsync(
                It.Is<Expression<Func<Contact, Contact>>>(y => y.Compile()(new Contact()) is Contact),
                It.Is<Expression<Func<Contact, bool>>>(y => y.Compile()(contactToMatch)),
                null, true
                )).ReturnsAsync(contact).Verifiable();

            //Act
            var result = _contactService.GetIdByEmail(email);
            result.Result.ShouldBe(contact);

            //Assert
            _contactRepositoryMock.VerifyAll();
        }
    }
}
