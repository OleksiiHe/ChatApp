using ChatApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatApp.WPF.Client
{
    public class SendChatMessageCommand : ICommand
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly SignalRChatService _signalRChatService;

        public SendChatMessageCommand(MainWindowViewModel mainWindowViewModel, SignalRChatService signalRChatService)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _signalRChatService = signalRChatService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                await _signalRChatService.SendChatMessage(new ChatMessage()
                {
                    TextMessage = _mainWindowViewModel.TextMessage,
                    //MessageDate = DateTime.Now,
                    Sender = _mainWindowViewModel.Login,
                    //Receiver = "All"
                });

                _mainWindowViewModel.ErrorMessage = string.Empty;

                _mainWindowViewModel.TextMessage = "";
            }
            catch (Exception)
            {

                _mainWindowViewModel.ErrorMessage = "Unable to send message";
            }
        }
    }
}
