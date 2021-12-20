using ChatApp.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.WPF.Client
{
    public class ChatHistoryViewModel : ViewModelBase //TODO
    {
        public ChatHistory History { get; set; }

        private ObservableCollection<ChatMessageViewModel> _allMessagesList;
        public ObservableCollection<ChatMessageViewModel> AllMessagesList
        {
            get
            {
                return _allMessagesList;
            }
            set
            {
                _allMessagesList = value;
                OnPropertyChanged(nameof(AllMessagesList));
            }
        }



        public ChatHistoryViewModel(ChatHistory history)
        {
            AllMessagesList =  new ObservableCollection<ChatMessageViewModel>();
            History = history;

            if (History.AllMessages != null)
            {
                foreach (ChatMessage chatMessage in History.AllMessages)
                {
                    ChatMessageViewModel chatMessageViewModel = new ChatMessageViewModel(chatMessage);
                    AllMessagesList.Add(chatMessageViewModel);
                }
            }
        }
    }
}
