using GameOn.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOn.Data.Context
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string connectionString = "server=sql.decinfo-cchic.ca;port=33306;database=a25_dev_app_expert_grp2_2232508;user=dev-2232508;password=Lucarinox123;";
            string connectionString = "server=sql.decinfo-cchic.ca;port=33306;database=a25_equipe5_dev_app_expert;user=dev-2534111;password=Password123;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("employes");
        }
    }
}
