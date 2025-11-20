using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOn.Models
{
    [Table("jeux")]
    public class Game
    {
        [Key]
        [Column("id_jeu")]
        public int IdJeu { get; set; }
        [Column("nom")]
        public string NomJeu { get; set; } = string.Empty;
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        public Game() { }
    }
}
