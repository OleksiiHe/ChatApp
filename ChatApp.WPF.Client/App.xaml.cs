using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ChatApp.WPF.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindowViewModel mainWindowViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e); //Is needed?

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/chatapp") //can be changed on https from launchSettings.json
                .Build();

            mainWindowViewModel = MainWindowViewModel.CreatedConnectedViewModel(new SignalRChatService(connection)); //is that window?

            MainWindow window = new MainWindow
            {
                DataContext = mainWindowViewModel
            };

            window.Show();
        }

        //protected override void ShutDown(StartupEventArgs e)
        //{
        //    base.Shutdown(e);
        //    await mainWindowViewModel.LogoutCommand;
        //}

    }
}
