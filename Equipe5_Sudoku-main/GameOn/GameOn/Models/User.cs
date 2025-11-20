using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace GameOn.Models
{
    [Table("employes")]
    public class User : ObservableObject
    {
        [Column("id_employe")]
        public int Id { get; set; }
        [Column("prenom")]
        public string FirstName { get; set; } = string.Empty;
        [Column("nom")]
        public string LastName { get; set; } = string.Empty;
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        [Column("mot_de_passe")]
        public string PwdHash { get; set; } = string.Empty;
        [Column("est_admin")]
        public bool IsAdmin { get; set; }
        [Column("num_telephone")]
        public string PhoneNumber { get; set; } = string.Empty;

        public User(int Id, string FirstName, string LastName, string Email, string PwdHash, bool IsAdmin, string PhoneNumber)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.PwdHash = PwdHash;
            this.IsAdmin = IsAdmin;
            this.PhoneNumber = PhoneNumber;
        }

        public User() { }
    }
}