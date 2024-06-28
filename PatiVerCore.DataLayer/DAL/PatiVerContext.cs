using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using PatiVerCore.DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatiVerCore.DataLayer.DAL
{
    public class PatiVerContext : DbContext
    {
        public DbSet<PersonResponseModel> PersonsResponces { get; set; }

        public DbSet<LocalData> FomsLocalData { get; set; }

        public PatiVerContext(DbContextOptions<PatiVerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Сопоставление классов с таблицами
            modelBuilder.Entity<PersonResponseModel>().ToTable("PersonResponseModels");
            modelBuilder.Entity<LocalData>().ToTable("FomsLocalData");
        }
    }
}
