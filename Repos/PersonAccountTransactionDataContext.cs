using System;
using System.Collections.Generic;
using System.Xml;
using PersonManagement.Model;
using PersonManagement.Repos.Models;
using Microsoft.EntityFrameworkCore;
using PersonManagement.Repos.Models;

namespace PersonManagement.Repos
{
    public class PersonAccountTransactionDataContext : DbContext
    {
        public PersonAccountTransactionDataContext()
        {
        }

        public PersonAccountTransactionDataContext(DbContextOptions<PersonAccountTransactionDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Person> Persons { get; set; } 
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<AccountStatus> AccountStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<Transaction>().ToTable("Transactions");

            modelBuilder.Entity<Person>()
              .HasMany(a => a.Accounts)
              .WithOne(b => b.Person)
              .HasForeignKey(b => b.PersonCode);

            modelBuilder.Entity<Account>()
             .HasMany(a => a.Transactions)
             .WithOne(b => b.Account)
             .HasForeignKey(b => b.AccountCode);

            modelBuilder.Entity<Account>()
            .HasOne(a => a.Status);
            //.WithOne(b => b.Account)
            //.HasForeignKey<Account>(b => b.Status);

            modelBuilder.Entity<Transaction>(entity =>
            {
                // Configure primary key
                entity.HasKey(e => e.Code);
                entity.Property(e => e.Code).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Person>(entity =>
            {
                // Configure primary key
                entity.HasKey(e => e.Code);
                entity.Property(e => e.Code).ValueGeneratedOnAdd();
                // Map entity properties to database columns
                entity.Property(e => e.IdNumber).HasColumnName("id_number");
                // Customize other configurations here
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Code);
                entity.Property(e => e.Code).ValueGeneratedOnAdd();
                entity.Property(e => e.PersonCode).HasColumnName("person_code");
                entity.Property(e => e.AccountNumber).HasColumnName("account_number");
                entity.Property(e => e.OutstandingBalance).HasColumnName("outstanding_balance");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                // Configure primary key
                entity.HasKey(e => e.Code);
                entity.Property(e => e.Code).ValueGeneratedOnAdd();
                // Map entity properties to database columns
                entity.Property(e => e.Capturedate).HasColumnName("capture_date");
                entity.Property(e => e.AccountCode).HasColumnName("account_code");
                entity.Property(e => e.TransactionDate).HasColumnName("transaction_date");
                // Customize other configurations here
            });

            modelBuilder.Entity<AccountStatus>().HasData(
                new AccountStatus { Id=1, StatusType = "Open" },
                new AccountStatus { Id=2,StatusType = "Closed"}
                );
        }
    }
}
