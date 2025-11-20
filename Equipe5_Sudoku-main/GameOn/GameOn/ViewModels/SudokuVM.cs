using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameOn.Data.Context;
using GameOn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using System.Windows.Threading;

namespace GameOn.ViewModels
{
    public partial class SudokuVM : ObservableObject
    {
        private Manager _manager;
        private SudokuContext _sudokuContext;

        private const int TAILLE = 9;
        private const int MAX_CHAINE_LONGUEUR = 81;
        private int[,]? _grilleInitiale;

        [ObservableProperty]
        private ObservableCollection<SudokuCell>? _cellules;
        [ObservableProperty]
        private string _messageFinDePartie;

        [ObservableProperty]
        private Sudoku? _puzzleActuel;

        public SudokuVM(Manager manager)
        {
            _manager = manager;
            _sudokuContext = new SudokuContext();
            Cellules = new();

            CommencerPartie();
        }

        private void ChargerSudokuParId(int idSudoku)
        {
            PuzzleActuel = _sudokuContext.Puzzles.FirstOrDefault(p => p.Id == idSudoku);
            _grilleInitiale = ConvertirEnGrille(PuzzleActuel.GrilleInitiale);
        }

        private int[,] ConvertirEnGrille(string chaine)
        {
            int[,] resultat = new int[TAILLE, TAILLE];

            if (string.IsNullOrWhiteSpace(chaine) || chaine.Length < MAX_CHAINE_LONGUEUR)
                throw new ArgumentOutOfRangeException("Format de grille invalide (81 chiffres requis).");

            int index = 0;
            for (int ligne = 0; ligne < TAILLE; ligne++)
            {
                for (int colonne = 0; colonne < TAILLE; colonne++)
                {
                    resultat[ligne, colonne] = int.Parse(chaine[index].ToString());
                    index++;
                }
            }
            return resultat;
        }

        private void CommencerPartie()
        {
            ChargerSudokuParId(_manager.ChargerDernierSudoku());

            for (int ligne = 0; ligne < TAILLE; ligne++)
            {
                for (int colonne = 0; colonne < TAILLE; colonne++)
                {
                    if (_grilleInitiale[ligne, colonne] != 0)
                    {
                        Cellules.Add(new SudokuCell(_grilleInitiale[ligne, colonne], true, $"Cell_{ligne}_{colonne}"));
                    }
                    else
                    {
                        Cellules.Add(new SudokuCell($"Cell_{ligne}_{colonne}"));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        [RelayCommand]
        public void CheckGrid()
        {
            string cellsString = "";
            foreach (SudokuCell c in Cellules)
            {
                if (c.Value is null) cellsString += "0";
                cellsString += c.Value.ToString();
            }
            Console.WriteLine(cellsString);
            //string solution = "429368715176425938538719264792143586351682497684597123817234659243956871965871342";

            if (PuzzleActuel.GameValidation(cellsString))
            {
                MessageFinDePartie = "Vous avez gagné";
            }
            else
            {
                MessageFinDePartie = "Vous avez perdu";
            }
        }
    }
}
