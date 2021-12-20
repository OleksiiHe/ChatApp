using ChatApp.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChatApp.WPF.Client
{
    public class SignUpCommand : ICommand
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly SignalRChatService _signalRChatService;

        //private readonly Validator _validator;

        //private bool isValid = _mainWindowViewModel.Validator();

        public SignUpCommand(MainWindowViewModel mainWindowViewModel, SignalRChatService signalRChatService)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _signalRChatService = signalRChatService;
            //_validator = new Validator(_mainWindowViewModel);
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
                //if(_validator.Validate())
                //{

                    SignUpCredentials signUpCredentials = new SignUpCredentials()
                    {
                        Name = _mainWindowViewModel.Login,
                        Email = _mainWindowViewModel.Email,
                        Password = (parameter as PasswordBox).Password.ToString()
                        //Password = (parameter as IHavePassword).SecureString.Unsecure

                    };

                    User user = await _signalRChatService.SignUp(signUpCredentials);

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

                    if (user != null)
                    {
                        _mainWindowViewModel.SignInScreenVisibility = Visibility.Visible;
                        _mainWindowViewModel.SignUpScreenVisibility = Visibility.Hidden;
                        _mainWindowViewModel.ChatScreenVisibility = Visibility.Hidden;

                        //_mainWindowViewModel.Login = "";
                        _mainWindowViewModel.Email = "";
                        (parameter as PasswordBox).Password = "";
                    }
                    else
                    {
                        _mainWindowViewModel.ErrorMessage = "Check all fields";
                    }
                //}
            }
            catch (Exception)
            {

                _mainWindowViewModel.ErrorMessage = "Check all fields";
            }
        }
    }
}
