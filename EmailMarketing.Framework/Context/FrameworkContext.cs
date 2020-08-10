﻿using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.Contacts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Context
{
    public class FrameworkContext : DbContext
    {

        private string _connectionString;
        private string _migrationAssemblyName;

        public FrameworkContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CampaignGroup>(userRole =>
            {
                userRole.HasKey(ur => new { ur.CampaignId, ur.GroupId });
            });
        }

        //public DbSet<Entities.Expense> Expenses { get; set; }
        public DbSet<Entities.SMTPConfig> SMTPConfigs { get; set; }
        public DbSet<Entities.Campaign> Campaigns { get; set; }
        public DbSet<Entities.CampaignGroup> CampaignGroups { get; set; }
        public DbSet<Entities.CampaignReport> CampaignReports { get; set; }
        public DbSet<Entities.Group> Groups { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactGroup> ContactGroups { get; set; }
        public DbSet<ContactUploadGroup> ContactUploadGroups { get; set; }
        public DbSet<ContactValueMap> ContactValueMaps { get; set; }
        public DbSet<ContactUpload> ContactUploads { get; set; }
        public DbSet<FieldMap> FieldMaps { get; set; }
        public DbSet<ContactUploadFieldMap> ContactUploadFieldMaps { get; set; }
    }
}
