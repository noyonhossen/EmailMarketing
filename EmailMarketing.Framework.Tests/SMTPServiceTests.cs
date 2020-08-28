using Autofac.Extras.Moq;
using EmailMarketing.Framework.Entities.SMTP;
using EmailMarketing.Framework.Repositories.SMTP;
using EmailMarketing.Framework.Services.SMTP;
using EmailMarketing.Framework.UnitOfWorks.SMTP;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace EmailMarketing.Framework.Tests
{
    [ExcludeFromCodeCoverage]
    public class SMTPServiceTests
    {
        private AutoMock _mock;
        private Mock<ISMTPRepository> _smtpRepositoryMock;
        private Mock<ISMTPUnitOfWork> _smtpUnitOfWorkMock;
        private ISMTPService _smtpService;

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
            _smtpRepositoryMock = _mock.Mock<ISMTPRepository>();
            _smtpUnitOfWorkMock = _mock.Mock<ISMTPUnitOfWork>();
            _smtpService = _mock.Create<SMTPService>();
        }

        [TearDown]
        public void Clean()
        {
            _smtpRepositoryMock.Reset();
            _smtpUnitOfWorkMock.Reset();
        }

        [Test]
        public void GetByIdAsync_GroupId_ReturnsGroupObject()
        {
            //Arrange
            var group = new SMTPConfig
            {
                 //UserId = 1,
                 Server = "Example",
                 Port = 8080,
                 SenderName = "ABC",
                 SenderEmail ="ABC@gmail.com",
                 UserName = "Abc",
                 Password = "12345",
                 EnableSSL = true
            };
        
        }

    }
}
