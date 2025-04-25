using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL
{
    public class PUKolContext : DbContext
    {
        public DbSet<Student> Studenci { get; set; }
        public DbSet<Grupa> Grupy { get; set; }
        public DbSet<Historia> Historie { get; set; }

        public PUKolContext(DbContextOptions<PUKolContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>()
                .HasKey(s => s.ID);

            builder.Entity<Student>()
                .Property(s => s.Imie)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<Student>()
                .Property(s => s.Nazwisko)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<Student>()
                .HasOne(s => s.Grupa)
                .WithMany(g => g.Studenci)
                .HasForeignKey(s => s.GrupaID)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Grupa>()
                .HasKey(g => g.ID);

            builder.Entity<Grupa>()
                .Property(g => g.Nazwa)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<Historia>()
                .HasKey(h => h.ID);

            builder.Entity<Historia>()
                .Property(h => h.Imie)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<Historia>()
                .Property(h => h.Nazwisko)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<Historia>()
                .Property(h => h.TypAkcji)
                .IsRequired()
                .HasMaxLength(50);

            builder.Entity<Historia>()
                .Property(h => h.Data)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PBKOL;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
