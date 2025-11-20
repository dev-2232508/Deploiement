using GameOn.Data.Context;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOn.Models
{
    [Table("sudoku_puzzles")]
    public class Sudoku
    {
        [NotMapped]
        private SudokuContext _context;
        [Key]
        [Column("id_sudoku")]
        public int Id { get; set; }
        [Column("puzzle_date")]
        public DateTime? _date { get; set; }
        [NotMapped]
        public DateTime? Date
        {
            get 
            {
                if (_date is null) return new DateTime();
                else return _date;
            }
            set
            {
                _date = value;
            }
        }

        [Column("initial_grid")]
        public string GrilleInitiale { get; set; }

        [Column("solution_grid")]
        public string GrilleSolution { get; set; }

        public Sudoku() { }
        public bool GameValidation(string partie)
        {
            if (partie.IsNullOrEmpty()) return false;

            if(partie == GrilleSolution)
            {
                return true;
            }

            return false;
        }
    }

}
