using ChatApp.Core;
using ChatApp.SignalR.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChatApp.WPF.Client
{
    public class MainWindowViewModel : ViewModelBase //TODO:
    {
        private string _textMessage;
        public string TextMessage 
        {
            get
            {
                return _textMessage;
            } 
            set
            {
                _textMessage = value;
                OnPropertyChanged(nameof(TextMessage));
            }               
        }

        private DateTime _messageDate;
        public DateTime MessageDate
        {
            get
            {
                return _messageDate;
            }
            set
            {
                _messageDate = value;
                OnPropertyChanged(nameof(MessageDate));
            }
        }

        private string _login;
        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get
            {
                return _isLoggedIn;
            }
            set
            {
                _isLoggedIn = value;
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        private ObservableCollection<UserViewModel> _users;
        public ObservableCollection<UserViewModel> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));//TODO: Refac
            }
        }

        private ObservableCollection<ChatMessageViewModel> _messages;
        public ObservableCollection<ChatMessageViewModel> Messages
        {
            get
            {
                return _messages;
            }
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        private Visibility _signInScreenVisibility;
        public Visibility SignInScreenVisibility
        {
            get
            {
                return _signInScreenVisibility;
            }
            set
            {
                _signInScreenVisibility = value;
                OnPropertyChanged(nameof(SignInScreenVisibility));
            }
        }

        private Visibility _signUpScreenVisibility;
        public Visibility SignUpScreenVisibility
        {
            get
            {
                return _signUpScreenVisibility;
            }
            set
            {
                _signUpScreenVisibility = value;
                OnPropertyChanged(nameof(SignUpScreenVisibility));
            }
        }

        private Visibility _chatScreenVisibility;
        public Visibility ChatScreenVisibility
        {
            get
            {
                return _chatScreenVisibility;
            }
            set
            {
                _chatScreenVisibility = value;
                OnPropertyChanged(nameof(ChatScreenVisibility));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);


        //public ObservableCollection<ChatMessageViewModel> Messages { get; }

        public ICommand SignInCommand { get; }
        public ICommand ToSignUpCommand { get; }
        public ICommand SignUpCommand { get; }
        public ICommand SendChatMessageCommand { get; }
        public ICommand LogoutCommand { get; }


        public MainWindowViewModel(SignalRChatService signalRChatService)
        {
            SignInCommand = new SignInCommand(this, signalRChatService);
            ToSignUpCommand = new ToSignUpCommand(this, signalRChatService);
            SignUpCommand = new SignUpCommand(this, signalRChatService);
            SendChatMessageCommand = new SendChatMessageCommand(this, signalRChatService);
            LogoutCommand = new LogoutCommand(this, signalRChatService);


            //Messages = new ObservableCollection<ChatMessageViewModel>();

            Messages = new ChatHistoryViewModel(ChatHub.History).AllMessagesList;

            if(Messages != null)
            {
                foreach (ChatMessageViewModel message in Messages)
                {
                    TextMessage = message.ChatMessage.TextMessage;
                    MessageDate = message.ChatMessage.MessageDate;
                }
            }

            signalRChatService.ChatMessageReceived += SignalRChatService_ChatMessageReceived;
            signalRChatService.UserLoggedIn += SignalRChatService_UserLoggedIn;
            signalRChatService.UserLoggedOut += SignalRChatService_UserLoggedOut;

            SignInScreenVisibility = Visibility.Visible;
            SignUpScreenVisibility = Visibility.Hidden;
            ChatScreenVisibility = Visibility.Hidden;
        }

        private void SignalRChatService_UserLoggedOut(string name)
        {
            var foundedUser = Users.Where((user) => string.Equals(user.Name, name)).FirstOrDefault(); //TODO: Test Field
            if (foundedUser != null)
            {
                foundedUser.IsLoggedIn = false;

                Users.Remove(foundedUser);
            }
        }

        private void SignalRChatService_UserLoggedIn(User user)
        {
            var foundUser = Users.FirstOrDefault((findingUser) => string.Equals(findingUser.Test, user.Test)); //TODO: Test Field
            if (IsLoggedIn && foundUser == null)
            {
                Users.Add(new UserViewModel(user)
                {
                    Name = user.UserName //TODO: Test Field
                });
            }
            Users.Add(new UserViewModel(user));
            //ChatHub.History.AllMessages.Add(chatMessage);
        }

        public static MainWindowViewModel CreatedConnectedViewModel(SignalRChatService signalRChatService)
        {
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(signalRChatService);

            signalRChatService.Connect().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    mainWindowViewModel.ErrorMessage = "Unable to connect to chat hub";
                }

            });

            return mainWindowViewModel;
        }

        private void SignalRChatService_ChatMessageReceived(ChatMessage chatMessage)
        {
            ChatMessageViewModel chatMessageViewModel = new ChatMessageViewModel(chatMessage)
            {
                TextMessage = chatMessage.TextMessage,
                MessageDate = chatMessage.MessageDate,
                Sender = chatMessage.Sender,
                Receiver = chatMessage.Receiver         
            };

            if(chatMessage.Sender != Login)
            {
                chatMessageViewModel.Color = "RoyalBlue";
                chatMessageViewModel.Alignment = "Left";
            }
            else if(chatMessage.Sender == Login)
            {
                chatMessageViewModel.Color = "DeepSkyBlue";
                chatMessageViewModel.Alignment = "Right";
            }

            Messages.Add(chatMessageViewModel);

            ChatHub.History.AllMessages.Add(chatMessage);
        }

        //public bool Validator()
        //{
        //    List<string> passwords = new List<string>();
        //    int count = VisualTreeHelper.GetChildrenCount(App.Current.MainWindow);
        //    for (int i = 0; i < count; i++)
        //    {
        //        DependencyObject current = VisualTreeHelper.GetChild(App.Current.MainWindow, i);

        //        if (current.GetType().Equals(typeof(PasswordBox)))
        //        {
        //            PasswordBox passwordBox = current as PasswordBox;
        //            passwords.Add(passwordBox.Password);
        //        }
        //    }

        //    if (passwords[0] == passwords[2])
        //    {
        //        return true;
        //    }

        //    else
        //    {
        //        return false;
        //    }
        //}

        //private void GetBorder()
        //{
        //    int count = VisualTreeHelper.GetChildrenCount(App.Current.MainWindow);
        //    for (int i = 0; i < count; i++)
        //    {
        //        DependencyObject current = VisualTreeHelper.GetChild(App.Current.MainWindow, i);

        //        if (current.GetType().Equals(typeof(Grid)))
        //        {
        //            int count2 = VisualTreeHelper.GetChildrenCount(current);

        //            for (int k = 0; k < count2; k++)
        //            {
        //                DependencyObject currentX = VisualTreeHelper.GetChild(current, k);

        //                if (currentX.GetType().Equals(typeof(Border)))
        //                {
        //                    Border border = (Border)currentX;

        //                    switch (border.Name)
        //                    {
        //                        case: 
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //private string _author;
        //public string Author
        //{
        //    get
        //    {
        //        return _author;
        //    }
        //    set
        //    {
        //        _author = value;
        //        OnPropertyChanged(nameof(Author));
        //    }
        //}

        //private string _time;
        //public string Time
        //{
        //    get
        //    {
        //        return _time;
        //    }
        //    set
        //    {
        //        _time = value;
        //        OnPropertyChanged(nameof(Time));
        //    }
        //}
    }
}
