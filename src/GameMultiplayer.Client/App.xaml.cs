using GameMultiplayer.Client.Authentication;
using GameMultiplayer.Client.Connections;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GameMultiplayer.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow window;

        public App()
        {
            window = new MainWindow();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Connection connection = AuthenticateConnection.Authenticate();
            if (connection is null)
            {
                this.Shutdown();
                return;
            }

            window.Connection = connection;
            window.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.Shutdown();
            base.OnExit(e);
        }

    }
}
