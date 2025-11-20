using GameOn.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Macs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOn.Data.Context
{
    public class SudokuContext : DbContext
    {
        public DbSet<Game> Jeux {  get; set; }
        public virtual DbSet<Sudoku> Puzzles { get; set; }
        public DbSet<PartieSudoku> Parties {  get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string connectionString = "server=sql.decinfo-cchic.ca;port=33306;database=a25_equipe5_dev_app_expert;user=dev-2232508;password=Lucarinox123;";
            string connectionString = "server=sql.decinfo-cchic.ca;port=33306;database=a25_equipe5_dev_app_expert;user=dev-2534111;password=Password123;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().ToTable("jeux");
            modelBuilder.Entity<Sudoku>().ToTable("sudoku_puzzles");
            modelBuilder.Entity<PartieSudoku>().ToTable("parties_sudoku");
        }
    }
}
