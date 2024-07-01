using System;
using System.Collections.Generic;
using PersonManagement.Model;
using PersonManagement.Repos.Models;
using Microsoft.EntityFrameworkCore;

namespace PersonManagement.Repos;

public partial class SecurityContext : DbContext
{
    public SecurityContext()
    {
    }

    public SecurityContext(DbContextOptions<SecurityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<OtpManager> OtpManagers { get; set; }

    public virtual DbSet<PwdManger> PwdMangers { get; set; }

    public virtual DbSet<Refreshtoken> Refreshtokens { get; set; }

    public virtual DbSet<Tempuser> Tempusers { get; set; }

    public virtual DbSet<User> Users { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tempuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("_tempuser1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
