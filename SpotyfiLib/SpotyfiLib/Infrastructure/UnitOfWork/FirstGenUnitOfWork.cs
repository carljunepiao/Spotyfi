using System;
using SpotyfiLib.Domain.RecordAgg;
using SpotyfiLib.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace SpotyfiLib.Infrastructure.UnitOfWork
{
    public class FirstGenUnitOfWork : DbContext, IUnitOfWork
    {
        public DbSet<Record> Records {get;set;}

        public FirstGenUnitOfWork(DbContextOptions<FirstGenUnitOfWork> options):base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Record>(record =>
            {
                record.HasKey("Id");
                record.Property("Id").HasColumnName("id");
                record.Property(p => p.ConcurrencyStamp).ForNpgsqlHasColumnName("xmin").ForNpgsqlHasColumnType("xid").ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
                record.ToTable("record");
            });

            base.OnModelCreating(builder);
        }

        public void Commit(){ SaveChanges(); }
        public void Rollback(){ Dispose(); }
    }
}