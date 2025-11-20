using GameOn.Models;
using GameOn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace GameOn.Views
{
    /// <summary>
    /// Logique d'interaction pour LoginV.xaml
    /// </summary>
    public partial class LoginV : UserControl
    {
        public LoginV()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pb = sender as PasswordBox;
            if (DataContext is LoginVM vm)
                vm.Pwd = pb.Password;
        }
    }
}
