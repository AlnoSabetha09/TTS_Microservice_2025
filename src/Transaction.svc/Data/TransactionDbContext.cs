using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Transaction.svc.Entities;

namespace Transaction.svc.Data
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {
        }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(s => s.SaleId);
            });

            modelBuilder.Entity<SaleItem>(entity =>
            {
                entity.HasKey(si => si.SaleItemId);

                entity.HasOne(si => si.Sale)
                    .WithMany(s => s.SaleItems)
                    .HasForeignKey(si => si.SaleId);
            });
        }
    }
}