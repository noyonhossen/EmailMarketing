using EmailMarketing.Common.Exceptions;
using Autofac.Extras.Moq;
using EmailMarketing.Framework.Entities.Groups;
using EmailMarketing.Framework.Entities.Contacts;
using EmailMarketing.Framework.Enums;
using EmailMarketing.Framework.Repositories.Contacts;
using EmailMarketing.Framework.Repositories.Groups;
using EmailMarketing.Framework.Services.Contacts;
using EmailMarketing.Framework.UnitOfWorks.Contacts;
using EmailMarketing.Framework.UnitOfWorks.Groups;
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
using EmailMarketing.Framework.Entities;


namespace EmailMarketing.Framework.Tests.Services.Contacts
{
    [ExcludeFromCodeCoverage]
    public class ContactServiceTests
    {
        private AutoMock _mock;
        private Mock<IGroupRepository> _groupRepositoryMock;
        private Mock<IGroupContactRepository> _groupContactRepositoryMock;
        private Mock<IContactRepository> _contactRepositoryMock;
        private Mock<IContactValueMapRepository> _contactValueMapRepositoryMock;
        private Mock<IFieldMapRepository> _fieldMapRepositoryMock;

        private Mock<IContactUnitOfWork> _contactUnitOfWorkMock;
        private Mock<IGroupUnitOfWork> _groupUnitOfWorkMock;

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
            _groupRepositoryMock = _mock.Mock<IGroupRepository>();
            _groupContactRepositoryMock = _mock.Mock<IGroupContactRepository>();
            _contactRepositoryMock = _mock.Mock<IContactRepository>();
            _contactValueMapRepositoryMock = _mock.Mock<IContactValueMapRepository>();
            _fieldMapRepositoryMock = _mock.Mock<IFieldMapRepository>();

            _groupUnitOfWorkMock = _mock.Mock<IGroupUnitOfWork>();
            _contactUnitOfWorkMock = _mock.Mock<IContactUnitOfWork>();
          
            _contactService = _mock.Create<ContactService>();
        }

        [TearDown]
        public void Clean()
        {
            _groupRepositoryMock.Reset();
            _groupContactRepositoryMock.Reset();
            _contactRepositoryMock.Reset();
            _contactValueMapRepositoryMock.Reset();
            _fieldMapRepositoryMock.Reset();

            _groupUnitOfWorkMock.Reset();
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
        public void GroupContactCountAsync_ContactNotNull_CountContact()
        {
            //Arrange
            int id = 1;
          
            var contact = new Contact
            {
                Id = 1,
                Email = "teama@gmail.com"
            };


            _contactUnitOfWorkMock.Setup(x => x.ContactRepository).Returns(_contactRepositoryMock.Object);

           // _contactRepositoryMock.Setup(x => x.GetCountAsync()).ReturnAsync(null).Verifiable();
            _contactUnitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask).Verifiable();

            //Act
            _contactService.GroupContactCountAsync(contact.Id);

            //Assert
            _contactRepositoryMock.VerifyAll();
            _contactUnitOfWorkMock.VerifyAll();
        }
        [Test]
        public void AddContact_ContactNotNull_AddContact()
        {
            //Arrange
            int id = 1;

            var contact = new Contact
            {
                Id = 1,
                Email = "teama@gmail.com"
            };

            _contactUnitOfWorkMock.Setup(x => x.ContactRepository)
                .Returns(_contactRepositoryMock.Object);

            _contactRepositoryMock.Setup(x => x.AddAsync(contact)).Returns(Task.CompletedTask).Verifiable();
            _contactUnitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask).Verifiable();

            //Act
            _contactService.AddContact(contact);

            //Assert
            _contactRepositoryMock.VerifyAll();
            _contactUnitOfWorkMock.VerifyAll();
        }
        [Test]
        public void AddContactValueMapsList_ContactValueMapsListNotNull_AddContactValueMapsList()
        {
            //Arrange
            var contactValueMapslist = new List<ContactValueMap>
            {
                new ContactValueMap { Value = "ABCDEF", ContactId = 1,FieldMapId = 1 },
                new ContactValueMap { Value = "ALFOAO", ContactId = 2,FieldMapId = 2 },
                new ContactValueMap { Value = "ELAGLA", ContactId = 3,FieldMapId = 3 },
                new ContactValueMap { Value = "ALJOAJ", ContactId = 4,FieldMapId = 4 },
            };

            _contactUnitOfWorkMock.Setup(x => x.ContactValueMapRepository).Returns(_contactValueMapRepositoryMock.Object);

            _contactValueMapRepositoryMock.Setup(x => x.AddRangeAsync(contactValueMapslist)).Returns(Task.CompletedTask).Verifiable();
            _contactUnitOfWorkMock .Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask).Verifiable();

            //Act
            _contactService.AddContacValueMaps(contactValueMapslist);

            //Assert
            _contactValueMapRepositoryMock.VerifyAll();
            _contactUnitOfWorkMock.VerifyAll();
        }
        
        [Test]
        public void AddContactGroupList_AddContactGroupListNotNull_AddContactGroupList()
        {
            //Arrange
            var contactGrouplist = new List<ContactGroup>
            {
                new ContactGroup { ContactId = 1,GroupId = 1 },
                new ContactGroup { ContactId = 2,GroupId = 2 },
                new ContactGroup { ContactId = 3,GroupId = 3 },
                new ContactGroup { ContactId = 4,GroupId = 4 },
            };

            _contactUnitOfWorkMock.Setup(x => x.GroupContactRepository).Returns(_groupContactRepositoryMock.Object);

            _groupContactRepositoryMock.Setup(x => x.AddRangeAsync(contactGrouplist)).Returns(Task.CompletedTask).Verifiable();
            _contactUnitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask).Verifiable();

            //Act
            _contactService.AddContactGroups(contactGrouplist);

            //Assert
            _contactValueMapRepositoryMock.VerifyAll();
            _contactUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public void GetGroupByIdAsync_ValidGroupId_GetContactObject()
        {
            //Arrange
            int id = 1;

        var contact = new Contact
        {
            Id = 1,
            Email = "teama@gmail.com"
        };

        _contactUnitOfWorkMock.Setup(x => x.ContactRepository)
                .Returns(_contactRepositoryMock.Object);

        _contactRepositoryMock.Setup(x => x.AddAsync(contact)).Returns(Task.CompletedTask).Verifiable();
        _contactUnitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask).Verifiable();

        //Act
        _contactService.AddContact(contact);

            //Assert
            _contactRepositoryMock.VerifyAll();
            _contactUnitOfWorkMock.VerifyAll();
        }
        [Test]
        public void GetContactValueMapByIdAsync_ForContactValueMapId_ReturnsContactValueMapObject()
        {
            //Arrange
            var contactValueMap = new ContactValueMap()
            {
                Id = 1,
                ContactId = 1,
                FieldMapId = 2,
                Value = "Team A"
            };
            int id = 1; 
            
            _contactUnitOfWorkMock.Setup(x => x.ContactValueMapRepository).Returns(_contactValueMapRepositoryMock.Object);
            _contactValueMapRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(contactValueMap).Verifiable();

            //Act
            var returnedDownloadQueue = _contactService.GetContactValueMapByIdAsync(id);

            //Assert
            _contactValueMapRepositoryMock.Verify();
        }

        [Test]
        public void DeleteContactGroupAsync_ForInvalidId_ThrowsException()
        {
            //Arrange
            var contactGroup = new ContactGroup()
            {
                Id = 1,
                GroupId = 2,
                ContactId = 1
            };
            int groupId = 2, contactId = 1;
            ContactGroup? nullContactGroup = null;

            _contactUnitOfWorkMock.Setup(x => x.GroupContactRepository).Returns(_groupContactRepositoryMock.Object);
            _groupContactRepositoryMock.Setup(x => x.GetFirstOrDefaultAsync(
                It.Is<Expression<Func<ContactGroup,ContactGroup>>>(y => y.Compile()(new ContactGroup()) is ContactGroup),
                It.Is<Expression<Func<ContactGroup,bool>>>(y => y.Compile()(contactGroup)),
                null,
                true)).ReturnsAsync(nullContactGroup).Verifiable();

            //Act
           // Should.Throw<NotFoundException>(
           // () => _contactService.DeleteContactGroupAsync(groupId,contactId)
            //);

            //Assert
            _groupContactRepositoryMock.VerifyAll();
        }

        [Test]
        public void DeleteContactGroupAsync_ForValidId_DeleteContactGroup()
        {
            //Arrange
            var contactGroup = new ContactGroup()
            {
                Id = 1,
                GroupId = 2,
                ContactId = 1
            };

            var contactGroupToMatch = new ContactGroup()
            {
                Id = 1,
                GroupId = 2,
                ContactId = 1
            };

            int groupId = 2, contactId = 1;

            _contactUnitOfWorkMock.Setup(x => x.GroupContactRepository).Returns(_groupContactRepositoryMock.Object);
            _groupContactRepositoryMock.Setup(x => x.GetFirstOrDefaultAsync(
                It.Is<Expression<Func<ContactGroup, ContactGroup>>>(y => y.Compile()(new ContactGroup()) is ContactGroup),
                It.Is<Expression<Func<ContactGroup, bool>>>(y => y.Compile()(contactGroupToMatch)),
                null,
                true)).ReturnsAsync(contactGroup).Verifiable();

            _groupContactRepositoryMock.Setup(x => x.DeleteAsync(contactGroup.Id)).Returns(Task.CompletedTask).Verifiable();
            _groupUnitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask).Verifiable();

            //Act
            //_contactService.DeleteContactGroupAsync(groupId, contactId);

            //Assert
            _groupContactRepositoryMock.VerifyAll();
            _contactUnitOfWorkMock.VerifyAll();

        }

        [Test]
        public void UpdateAsync_ForInvalidContact_ThrowsException()
        {
            //Assign
            var contact = new Contact
            {
                Id = 1,
                Email = "teamA@gmail.com"
            };
            var contactToMatch = new Contact
            {
                Id = 2,
                Email = "teamA@gmail.com"
            };
            _contactUnitOfWorkMock.Setup(x => x.ContactRepository).Returns(_contactRepositoryMock.Object);
            _contactRepositoryMock.Setup(x => x.IsExistsAsync(
                It.Is<Expression<Func<Contact,bool>>>(y => y.Compile()(contactToMatch))
                )).ReturnsAsync(true).Verifiable();

            //Act
            Should.Throw<DuplicationException>(
                () => _contactService.UpdateAsync(contact));

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
        public void UpdateAsync_ForValidContactId_UpdateContact()
        {
            //Assign
            var existingContact = new Contact
            {
                Id = 1,
                Email = "teama@gmail.com"
            };
            var contactToUpdate = new Contact
            {
                Id = 1,
                Email = "teamA@gmail.com"
            };
            var contactToMatch = new Contact
            {
                Id = 2,
                Email = "teamA@gmail.com"
            };
            _contactUnitOfWorkMock.Setup(x => x.ContactRepository).Returns(_contactRepositoryMock.Object);

            _contactRepositoryMock.Setup(x => x.IsExistsAsync(
                It.Is<Expression<Func<Contact, bool>>>(y => y.Compile()(contactToMatch))
                )).ReturnsAsync(false).Verifiable();

            _contactRepositoryMock.Setup(x => x.GetFirstOrDefaultAsync(
                It.Is<Expression<Func<Contact, Contact>>>(y => y.Compile()(new Contact()) is Contact),
                It.Is<Expression<Func<Contact, bool>>>(y => y.Compile()(existingContact)),
                It.IsAny<Func<IQueryable<Contact>,IIncludableQueryable<Contact,object>>>(),
                true
                )).ReturnsAsync(existingContact).Verifiable();

            _contactRepositoryMock.Setup(x => x.UpdateAsync(existingContact)).Returns(Task.CompletedTask).Verifiable();
            _contactUnitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask).Verifiable();

            //Act
            _contactService.UpdateAsync(contactToUpdate);

            //Assert
            _contactRepositoryMock.VerifyAll();
            _contactUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public void GetAllContactValueMapsCustom_ForUserId_ReturnsFieldMapList()
        {
            var userId = Guid.NewGuid();
            var list = new List<ValueTuple<int, string>>
            {
                (1, "Email"),
                (2, "Address"),
                (3, "Date of Birth")
            };

            var fieldMapTemp = new FieldMap
            {
                IsActive = true,
                IsDeleted = false,
                IsStandard = false,
                UserId = userId
            };

            _contactUnitOfWorkMock.Setup(x => x.FieldMapRepository).Returns(_fieldMapRepositoryMock.Object);
            _fieldMapRepositoryMock.Setup(x => x.GetAsync(
                It.IsAny<Expression<Func<FieldMap, ValueTuple<int, string>>>>(),
                It.Is<Expression<Func<FieldMap, bool>>>(y => y.Compile()(fieldMapTemp)),
                null,
                null,
                true
               )).ReturnsAsync(list).Verifiable();

            //Act
            var result = _contactService.GetAllContactValueMapsCustom(userId);

            ////Assert
            _fieldMapRepositoryMock.VerifyAll();

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
           // var result = _contactService.GetIdByEmail(email);
            //result.Result.ShouldBe(contact);
  
            //Assert
            _contactRepositoryMock.VerifyAll();
        }

        [Test]
        public void GetAllContactAsync_ForUserId_ShowContactList()
        {
            //Assign
            int pageIndex = 1;
            int pageSize = 10;
            var searchText = "team";
            var userId = Guid.NewGuid();
            var orderBy = "asc";
            
            var contactListToReturn = new List<Contact>
            {
                new Contact
                {
                    Id = 1,
                    Email = "teamA@gmail.com"
                },
                new Contact
                {
                    Id = 2,
                    Email= "teamB@gmail.com"
                },
                new Contact
                {
                    Id = 3,
                    Email = "teamC@gmail.com"
                }
            };
            var contactToMatch = new Contact
            {
                UserId = userId,
                Email = "team"
            };

            var columnsMap = new Dictionary<string, Expression<Func<Entities.Contacts.Contact, object>>>()
            {
                ["Email"] = v => v.Email
            };

            _contactUnitOfWorkMock.Setup(x => x.ContactRepository).Returns(_contactRepositoryMock.Object);
            _contactRepositoryMock.Setup(x => x.GetAsync<Contact>(
                It.Is<Expression<Func<Contact, Contact>>>(y => y.Compile()(new Contact()) is Contact),
                It.Is<Expression<Func<Contact, bool>>>(y => y.Compile()(contactToMatch)),
                It.IsAny<Func<IQueryable<Contact>, IOrderedQueryable<Contact>>>(),
                It.IsAny<Func<IQueryable<Contact>, IIncludableQueryable<Contact, object>>>(),
                pageIndex,
                pageSize,
                true
            )).ReturnsAsync((contactListToReturn,4,3)).Verifiable();

            //Act
            var result = _contactService.GetAllContactAsync(userId, searchText, orderBy, pageIndex, pageSize);
            result.Result.ShouldBe((contactListToReturn, 4,3));
  
            //Assert
            _contactRepositoryMock.VerifyAll();
        }


    }
}

