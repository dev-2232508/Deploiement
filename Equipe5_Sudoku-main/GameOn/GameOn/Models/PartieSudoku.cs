using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOn.Models
{
    [Table("parties_sudoku")]
    public class PartieSudoku
    {
        // comment est-ce qu'on utilise cette classe ? 
        [Key]
        [Column("id_partie")]
        public int IdPartie { get; set; }
        [Column("id_sudoku")]
        public int SudokuId { get; set; }
        [Column("date_partie")]
        public DateTime DatePartie { get; set; }

        [Column("jeu")]
        public int JeuId { get; set; }

        [Column("info_competition")]
        public int InfoCompetitionId { get; set; }
        public Sudoku Sudoku { get; set; }
        public Game Jeu { get; set; }

        public PartieSudoku() { }
    }
}
