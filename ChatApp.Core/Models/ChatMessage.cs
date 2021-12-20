using System;

namespace ChatApp.Core
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string TextMessage { get; set; }
        public DateTime MessageDate { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string FromTo { get; set; }

        //TODO: With trigger
        //public bool IsOriginNative { get; set; } 
    }
}
