using BCrypt.Net;
using CommunityToolkit.Mvvm.ComponentModel;
using GameOn.Data.Context;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameOn.Models
{
    public class Manager : ObservableObject
    {
        public ObservableCollection<User> Users { get; private set; }
        public SudokuContext _sudokuContext {  get; set; }

        public Manager()
        {
            Users = new ObservableCollection<User>();
            _sudokuContext = new SudokuContext();
        }

        public virtual bool Login(string pwd, string email)
        {
            using var sha = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(pwd);
            string hash = Convert.ToHexString(sha.ComputeHash(bytes)).ToLower();

            return Users.Any(u => u.PwdHash == hash && u.Email == email);
        }

        public bool AddUser(User user)
        {
            if (Users.Contains(user)) return false;
            else
            {
                Users.Add(user);
                return true;
            }
        }

        public virtual int ChargerDernierSudoku()
        {
            Sudoku[] puzzles = _sudokuContext.Puzzles.Where(p => p._date != null).ToArray();
            return puzzles[^1].Id;
        }
    }
}
