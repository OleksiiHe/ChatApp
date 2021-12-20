using ChatApp.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.WPF.Client
{
    public class UserViewModel : ViewModelBase
    {
        //public ObservableCollection<ChatMessageViewModel> RoomMessages { get; set; }

        //private int _id;
        //public int Id
        //{
        //    get
        //    {
        //        return _id;
        //    }
        //    set
        //    {
        //        _id = value;
        //        OnPropertyChanged(nameof(Id));
        //    }
        //}

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _test; //TODO: Test Field
        public string Test //TODO: Test Field
        {
            get
            {
                return _test;
            }
            set
            {
                _test = value;
                OnPropertyChanged(nameof(Test));
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

        private bool _hasSentNewMessage;
        public bool HasSentNewMessage
        {
            get
            {
                return _hasSentNewMessage;
            }
            set
            {
                _hasSentNewMessage = value;
                OnPropertyChanged(nameof(HasSentNewMessage));
            }
        }

        private bool _isTyping;
        public bool IsTyping
        {
            get
            {
                return _isTyping;
            }
            set
            {
                _isTyping = value;
                OnPropertyChanged(nameof(IsTyping));
            }
        }

        public User User { get; set; }
        public UserViewModel(User user)
        {
            User = user;
            //RoomMessages = new ObservableCollection<ChatMessageViewModel>();
        }
    }
}
