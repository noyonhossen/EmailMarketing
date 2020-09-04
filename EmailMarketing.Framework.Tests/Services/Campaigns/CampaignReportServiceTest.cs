using Autofac.Extras.Moq;
using EmailMarketing.Framework.Entities.Campaigns;
using EmailMarketing.Framework.Repositories.Campaigns;
using EmailMarketing.Framework.Services.Campaigns;
using EmailMarketing.Framework.UnitOfWorks.Campaigns;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Tests.Services.Campaigns
{
    [ExcludeFromCodeCoverage]
    public class CampaignReportServiceTest
    {
        private AutoMock _mock;
        private Mock<ICampaignReportRepository> _campaignReportRepositoryMock;
        private Mock<ICampaignReportUnitOfWork> _campaignReportUnitOfWorkMock;
        private ICampaignReportService _campaignReportService;

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
            _campaignReportRepositoryMock = _mock.Mock<ICampaignReportRepository>();
            _campaignReportUnitOfWorkMock = _mock.Mock<ICampaignReportUnitOfWork>();
            _campaignReportService = _mock.Create<CampaignReportService>();
        }

        [TearDown]
        public void Cleanup()
        {
            _campaignReportRepositoryMock?.Reset();
            _campaignReportUnitOfWorkMock?.Reset();
        }

        [Test]
        
        public void AddCampaignReportAsync_Save()
        {
            //Arrange
            var campaignReports = new List<CampaignReport>
            {
                new CampaignReport
                {
                    Id =2,
                    CampaignId = 2,
                    ContactId =2,

                },
                new CampaignReport
                {
                    Id = 1,
                    CampaignId = 1,
                    ContactId = 1,

                },
                new CampaignReport
                {
                    Id =3,
                    CampaignId = 3,
                    ContactId =3,

                }


            };            

            _campaignReportUnitOfWorkMock.Setup(x => x.CampaingReportRepository).Returns(_campaignReportRepositoryMock.Object);
            _campaignReportRepositoryMock.Setup(x => x.AddRangeAsync(campaignReports)).Returns(Task.CompletedTask).Verifiable();

              _campaignReportUnitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask).Verifiable();
            //Act

            _campaignReportService.AddCampaingReportAsync(campaignReports);


            //Assert
            _campaignReportRepositoryMock.VerifyAll();
            _campaignReportUnitOfWorkMock.VerifyAll();
        }
    }
}
