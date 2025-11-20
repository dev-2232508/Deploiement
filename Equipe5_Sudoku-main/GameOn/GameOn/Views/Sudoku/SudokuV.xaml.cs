using GameOn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GameOn.Views.Sudoku
{
    /// <summary>
    /// Logique d'interaction pour SudokuV.xaml
    /// </summary>
    public partial class SudokuV : UserControl
    {
        private DispatcherTimer timer;
        private int seconds;
        public SudokuV()
        {
            InitializeComponent();

            seconds = 0;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }
        

        private void SudokuCell_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[1-9]$");
        }

        private void SudokuCell_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            seconds++;
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            TimerLabel.Text = time.ToString(@"mm\:ss");
        }

        private void JouerButton_Click(object sender, RoutedEventArgs e)
        {
            Grille.Visibility = Visibility.Visible;
            JouerButton.IsEnabled = false;
            timer.Start();
        }

        private void VerifierButton_Click(object sender, RoutedEventArgs e)
        {
            VerifierButton.IsEnabled = false;
            timer.Stop();
        }
    }
}
