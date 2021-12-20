using ChatApp.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChatApp.WPF.Client
{
    public class SignInCommand : ICommand
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly SignalRChatService _signalRChatService;

        public SignInCommand(MainWindowViewModel mainWindowViewModel, SignalRChatService signalRChatService)
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
                SignUpCredentials signUpCredentials = new SignUpCredentials
                {
                    Name = _mainWindowViewModel.Login,
                    Password = (parameter as PasswordBox).Password.ToString()
                };

                List<User> users = new List<User>();

                users = await _signalRChatService.Login(signUpCredentials);

                if (users != null)
                {
                    _mainWindowViewModel.Users = new ObservableCollection<UserViewModel>();

                    users.ForEach(user => _mainWindowViewModel.Users.Add(new UserViewModel(user)
                    {
                        Name = user.UserName, //TODO: Test Field
                        //IsLoggedIn = true,
                        //HasSentNewMessage = user.HasSentNewMessage,
                        //IsTyping = user.IsTyping
                    }));
                    _mainWindowViewModel.IsLoggedIn = true;
                    //return true;

                    _mainWindowViewModel.SignInScreenVisibility = Visibility.Hidden;
                    _mainWindowViewModel.SignUpScreenVisibility = Visibility.Hidden;
                    _mainWindowViewModel.ChatScreenVisibility = Visibility.Visible;
                }
                else
                {
                    _mainWindowViewModel.ErrorMessage = "Check all fields";
                }

                //await _signalRChatService.SendChatMessage(new ChatMessage()
                //{
                //    TextMessage = _mainWindowViewModel.TextMessage
                //});

                //_mainWindowViewModel.ErrorMessage = string.Empty;

                //_mainWindowViewModel.TextMessage = "";


            }
            catch (Exception)
            {

                _mainWindowViewModel.ErrorMessage = "Check all fields";
            }
        }
    }
}
