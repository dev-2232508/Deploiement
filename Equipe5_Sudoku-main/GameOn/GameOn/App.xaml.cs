using System.Configuration;
using System.Data;
using System.Windows;
using AutoUpdaterDotNET;

namespace GameOn
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AutoUpdater.Start("https://raw.githubusercontent.com/TON_USER/TON_REPO/main/update.xml");
        }
    }

}
