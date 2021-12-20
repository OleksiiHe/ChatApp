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
    public class ToSignUpCommand : ICommand
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly SignalRChatService _signalRChatService;

        public ToSignUpCommand(MainWindowViewModel mainWindowViewModel, SignalRChatService signalRChatService)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _signalRChatService = signalRChatService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            //if (!string.IsNullOrEmpty(_mainWindowViewModel.Login))
            //{
            return true;
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
                //string userName = (string)parameter;
                //List<User> users = new List<User>();

                //users = await _signalRChatService.Login(userName);

                //if (users != null)
                //{
                //    _mainWindowViewModel.Users = new ObservableCollection<UserViewModel>();

                //    users.ForEach(user => _mainWindowViewModel.Users.Add(new UserViewModel(user)
                //    {
                //        Name = user.Name,
                //        //IsLoggedIn = true,
                //        //HasSentNewMessage = user.HasSentNewMessage,
                //        //IsTyping = user.IsTyping
                //    }));
                //    _mainWindowViewModel.IsLoggedIn = true;
                //    //return true;
                //}
                //else
                //{
                //    MessageBox.Show("Username is already in use");
                //}

                //await _signalRChatService.SendChatMessage(new ChatMessage()
                //{
                //    TextMessage = _mainWindowViewModel.TextMessage
                //});

                //_mainWindowViewModel.ErrorMessage = string.Empty;

                //_mainWindowViewModel.TextMessage = "";

                _mainWindowViewModel.SignInScreenVisibility = Visibility.Hidden;
                _mainWindowViewModel.SignUpScreenVisibility = Visibility.Visible;
                _mainWindowViewModel.ChatScreenVisibility = Visibility.Hidden;
            }
            catch (Exception)
            {

                _mainWindowViewModel.ErrorMessage = "Unable to log in";
            }
        }
    }
}
