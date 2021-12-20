using ChatApp.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChatApp.WPF.Client
{
    public class LogoutCommand : ICommand
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly SignalRChatService _signalRChatService;

        public LogoutCommand(MainWindowViewModel mainWindowViewModel, SignalRChatService signalRChatService)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _signalRChatService = signalRChatService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            //if (!string.IsNullOrEmpty(_mainWindowViewModel.Login))
            //{
            return _mainWindowViewModel.IsLoggedIn;
            //}
            //else
            //{
            //    return false;
            //}
        }

        public async void Execute(object parameter) //TODO: Rename to ExecuteAsync
        {
            try
            {
                await _signalRChatService.Logout(_mainWindowViewModel.Login);
            }
            catch (Exception)
            {

                _mainWindowViewModel.ErrorMessage = "Unable to log out";
            }
        }
    }
}
