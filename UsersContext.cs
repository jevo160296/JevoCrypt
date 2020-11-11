using JevoCrypt.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JevoCrypt
{
    public class UsersContext:DbContext
    {
        #region DataBaseTables
        public DbSet<User> Users { get; set; }
        #endregion
        #region Configuration
        private static string dbname = "users.db";
        public string FolderPath { get; set; } = null;
        public string DbPath { get => Path.Combine(FolderPath, dbname); }
        #endregion
        #region Constructores
        public UsersContext() : base()
        {
            FolderPath = "C:/";
        }
        public UsersContext(string folderpath) : base()
        {
            FolderPath = folderpath;
            Database.Migrate();
        }
        #endregion
        #region Override
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(p => p.UserName)
                .IsRequired();
            modelBuilder
                .HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangedNotifications);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={DbPath}");
        }
        #endregion
    }
}
