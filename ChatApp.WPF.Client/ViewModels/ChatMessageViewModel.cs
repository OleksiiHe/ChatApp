using ChatApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.WPF.Client
{
    public class ChatMessageViewModel : ViewModelBase
    {
        private string _textMessage;
        public string TextMessage
        {
            get
            {
                return ChatMessage.TextMessage;
            }
            set
            {
                _textMessage = value;
                OnPropertyChanged(nameof(TextMessage));
            }
        }

        private string _sender;
        public string Sender
        {
            get
            {
                return ChatMessage.Sender;
            }
            set
            {
                _sender = value;
                OnPropertyChanged(nameof(Sender));
            }
        }

        private string _receiver;
        public string Receiver
        {
            get
            {
                return ChatMessage.Receiver;
            }
            set
            {
                _receiver = value;
                OnPropertyChanged(nameof(Receiver));
            }
        }

        private string _color;
        public string Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        private string _alignment;
        public string Alignment
        {
            get
            {
                return _alignment;
            }
            set
            {
                _alignment = value;
                OnPropertyChanged(nameof(Alignment));
            }
        }

        private DateTime _messageDate;
        public DateTime MessageDate
        {
            get
            {
                return ChatMessage.MessageDate;
            }
            set
            {
                _messageDate = value;
                OnPropertyChanged(nameof(MessageDate));
            }
        }

        public ChatMessage ChatMessage { get; set; }

        public ChatMessageViewModel(ChatMessage chatMessage)
        {
            ChatMessage = chatMessage;
        }
    }
}
