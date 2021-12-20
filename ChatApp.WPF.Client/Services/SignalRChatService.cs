using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Core;
using System.Security;

namespace ChatApp.WPF.Client
{
    public class SignalRChatService
    {
        private readonly HubConnection _connection;

        public event Action<User> UserLoggedIn;
        public event Action<ChatMessage> ChatMessageReceived;
        public event Action<string> UserLoggedOut;

        public SignalRChatService(HubConnection connection)
        {
            _connection = connection;

            _connection.On<User>("Login", (user) => UserLoggedIn?.Invoke(user)); // Is it right method name?
            _connection.On<ChatMessage>("ReceiveChatMessage", (chatMessage) => ChatMessageReceived?.Invoke(chatMessage));
            _connection.On<string>("Logout", (name) => UserLoggedOut?.Invoke(name)); // Is it right method name?
        }

        public async Task Connect()
        {
            await _connection.StartAsync();
        }

        public async Task SendChatMessage(ChatMessage chatMessage)
        {
            await _connection.SendAsync("SendChatMessage", chatMessage);
        }

        public async Task <List<User>> Login(SignUpCredentials signUpCredentials)
        {
            return await _connection.InvokeAsync<List<User>>("Login", signUpCredentials);
        }

        public async Task Logout(string userName)
        {
            await _connection.InvokeAsync("Logout", userName);
        }

        public async Task<User> SignUp(SignUpCredentials signUpCredentials)
        {
            //SignUpCredentials signUpCredentials = new SignUpCredentials()
            //{
            //};
            return await _connection.InvokeAsync<User>("SignUp", signUpCredentials);
        }
    }
}
