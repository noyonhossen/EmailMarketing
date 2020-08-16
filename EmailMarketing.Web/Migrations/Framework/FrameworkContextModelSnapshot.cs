﻿// <auto-generated />
using System;
using EmailMarketing.Framework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmailMarketing.Web.Migrations.Framework
{
    [DbContext(typeof(FrameworkContext))]
    partial class FrameworkContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Campaigns.Campaign", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailSubject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmailTemplateId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDraft")
                        .HasColumnType("bit");

                    b.Property<bool>("IsProcessing")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSendEmailNotify")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSucceed")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SendDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SendEmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EmailTemplateId");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Campaigns.CampaignGroup", b =>
                {
                    b.Property<int>("CampaignId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("CampaignId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("CampaignGroups");
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Campaigns.CampaignReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CampaignId")
                        .HasColumnType("int");

                    b.Property<int>("ContactId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelivered")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPersonalized")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSeen")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SMTPConfigId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("SeenDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SendDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.HasIndex("ContactId");

                    b.HasIndex("SMTPConfigId");

                    b.ToTable("CampaignReports");
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Campaigns.EmailTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailTemplateBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailTemplateName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("EmailTemplates");
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Contacts.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContactUploadId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ContactUploadId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Contacts.ContactGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContactId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.HasIndex("GroupId");

                    b.ToTable("ContactGroups");
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Contacts.ContactUpload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasColumnHeader")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsProcessing")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSendEmailNotify")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSucceed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUpdateExisting")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SendEmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SucceedEntryCount")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("ContactUploads");
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Contacts.ContactUploadFieldMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContactUploadId")
                        .HasColumnType("int");

                    b.Property<int>("FieldMapId")
                        .HasColumnType("int");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ContactUploadId");

                    b.HasIndex("FieldMapId");

                    b.ToTable("ContactUploadFieldMaps");
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Contacts.ContactUploadGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContactUploadId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ContactUploadId");

                    b.HasIndex("GroupId");

                    b.ToTable("ContactUploadGroups");
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Contacts.ContactValueMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContactId")
                        .HasColumnType("int");

                    b.Property<int>("FieldMapId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.HasIndex("FieldMapId");

                    b.ToTable("ContactValueMaps");
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Contacts.FieldMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsStandard")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("FieldMaps");
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.DownloadQueue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DownloadQueueFor")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsProcessing")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSendEmailNotify")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSucceed")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SendEmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SucceedEntryCount")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("DownloadQueues");
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.DownloadQueueSubEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DownloadQueueId")
                        .HasColumnType("int");

                    b.Property<int>("DownloadQueueSubEntityId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DownloadQueueId");

                    b.ToTable("DownloadQueueSubEntities");
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Groups.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.SMTP.SMTPConfig", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("EnableSSL")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<string>("SenderEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SenderName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Server")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SMTPConfigs");
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Campaigns.Campaign", b =>
                {
                    b.HasOne("EmailMarketing.Framework.Entities.Campaigns.EmailTemplate", "EmailTemplate")
                        .WithMany()
                        .HasForeignKey("EmailTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Campaigns.CampaignGroup", b =>
                {
                    b.HasOne("EmailMarketing.Framework.Entities.Campaigns.Campaign", "Campaign")
                        .WithMany("CampaignGroups")
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmailMarketing.Framework.Entities.Groups.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Campaigns.CampaignReport", b =>
                {
                    b.HasOne("EmailMarketing.Framework.Entities.Campaigns.Campaign", "Campaign")
                        .WithMany("CampaignReports")
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmailMarketing.Framework.Entities.Contacts.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmailMarketing.Framework.Entities.SMTP.SMTPConfig", "SMTPConfig")
                        .WithMany()
                        .HasForeignKey("SMTPConfigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Contacts.Contact", b =>
                {
                    b.HasOne("EmailMarketing.Framework.Entities.Contacts.ContactUpload", "ContactUpload")
                        .WithMany()
                        .HasForeignKey("ContactUploadId");
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Contacts.ContactGroup", b =>
                {
                    b.HasOne("EmailMarketing.Framework.Entities.Contacts.Contact", "Contact")
                        .WithMany("ContactGroups")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmailMarketing.Framework.Entities.Groups.Group", "Group")
                        .WithMany("ContactGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Contacts.ContactUploadFieldMap", b =>
                {
                    b.HasOne("EmailMarketing.Framework.Entities.Contacts.ContactUpload", "ContactUpload")
                        .WithMany("ContactUploadFieldMaps")
                        .HasForeignKey("ContactUploadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmailMarketing.Framework.Entities.Contacts.FieldMap", "FieldMap")
                        .WithMany()
                        .HasForeignKey("FieldMapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Contacts.ContactUploadGroup", b =>
                {
                    b.HasOne("EmailMarketing.Framework.Entities.Contacts.ContactUpload", "ContactUpload")
                        .WithMany("ContactUploadGroups")
                        .HasForeignKey("ContactUploadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmailMarketing.Framework.Entities.Groups.Group", "Group")
                        .WithMany("ContactUploadGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.Contacts.ContactValueMap", b =>
                {
                    b.HasOne("EmailMarketing.Framework.Entities.Contacts.Contact", "Contact")
                        .WithMany("ContactValueMaps")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmailMarketing.Framework.Entities.Contacts.FieldMap", "FieldMap")
                        .WithMany()
                        .HasForeignKey("FieldMapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmailMarketing.Framework.Entities.DownloadQueueSubEntity", b =>
                {
                    b.HasOne("EmailMarketing.Framework.Entities.DownloadQueue", "DownloadQueue")
                        .WithMany("DownloadQueueSubEntities")
                        .HasForeignKey("DownloadQueueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
