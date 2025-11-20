using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameOn.Data.Context;
using GameOn.Models;
using GameOn.Views;
using GameOn.Views.Sudoku;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOn.ViewModels
{
    public partial class LoginVM : ObservableObject
    {
        private Manager _manager;
        private UserContext _userContext;
        private MainWindow _main;

        [ObservableProperty]
        private string? _pwd, _email, _notification;

        public LoginVM(Manager manager, MainWindow main)
        {
            _main = main;
            _manager = manager;
            _userContext = new();
            _pwd = string.Empty;
            _email = string.Empty;
            _notification = string.Empty;

            foreach(User u in _userContext.Users.ToArray())
            {
                _manager.AddUser(u);
            }
        }

        [RelayCommand]
        public void Login()
        {
            if (Pwd is not null && Email is not null)
            {
                if(_manager.Login(Pwd, Email))
                {
                    Notification = "Connexion réussi.";
                    if (_main is null)
                    {
                        return;
                    }
                    else
                    {
                        _main.SudokuV.Visibility = System.Windows.Visibility.Visible;
                        _main.LoginV.Visibility = System.Windows.Visibility.Hidden;
                        return;
                    }
                }
                else Notification = "Connexion échouée.";
            }
        }
    }
}
