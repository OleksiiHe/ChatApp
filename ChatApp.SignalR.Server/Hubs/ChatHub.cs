using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Core;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.SignalR.Server
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _db;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public ChatHub(AppDbContext db, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public static ChatHistory History = new ChatHistory() { AllMessages = new List<ChatMessage>() }; //TODO:

        private static ConcurrentDictionary<string, User> ChatUsers = new ConcurrentDictionary<string, User>();

        public async Task SendChatMessage(/*string text*/ ChatMessage chatMessage)
        {
            ChatMessage message = new ChatMessage
            {
                TextMessage = chatMessage.TextMessage,
                Sender = chatMessage.Sender,
                //Sender = Context.User.Identity.Name,
                MessageDate = DateTime.Now
            };

            //save the message in DB
            //_db.Messages.Add(message);
            //_db.SaveChanges();

            //send message to all
            await Clients.All.SendAsync("ReceiveChatMessage", message); //TODO: nameof?
        }

        public async Task<List<User>> Login(SignUpCredentials signUpCredentials)
        {

            if (signUpCredentials != null
                && !string.IsNullOrEmpty(signUpCredentials.Name)
                && !string.IsNullOrEmpty(signUpCredentials.Password))
            {

                //User newUser = new User
                //{
                //    //Id = Context.
                //    //ID = Context.ConnectionId,
                //    UserName = signUpCredentials.Name //TODO: Test Field
                //};

                User newUser = await _userManager.FindByNameAsync(signUpCredentials.Name);
                //User thirddUser = await _userManager.FindByEmailAsync(secondUser.Email);

                //await _db.Users.AddAsync(newUser);



                //var result = await _signInManager.PasswordSignInAsync(thirddUser.UserName, signUpCredentials.Password, true, false);

                var result = await _userManager.CheckPasswordAsync(newUser, signUpCredentials.Password);

                if (result && !ChatUsers.ContainsKey(signUpCredentials.Name))
                {
                    Console.WriteLine($"+++++ {signUpCredentials.Name} logged in +++++");
                    List<User> users = new List<User>(ChatUsers.Values);

                    //User newUser = new User
                    //{
                    //    //Id = Context.
                    //    //ID = Context.ConnectionId,
                    //    UserName = signUpCredentials.Name //TODO: Test Field
                    //};

                    if (!ChatUsers.TryAdd(signUpCredentials.Name, newUser))
                    {
                        return null;
                    }

                    //Clients.CallerState.UserName = userName;
                    await Clients.Others.SendAsync("Login", newUser);

                    return users;
                }

                return null;
            }

            return null;

        }

        public async Task Logout(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                User newUser = new User();
                ChatUsers.TryRemove(userName, out newUser);
                await Clients.Others.SendAsync("Logout", userName);
                Console.WriteLine($"----- {userName} logged out -----");
            }

        }

        public async Task<User> SignUp(SignUpCredentials signUpCredentials)
        {
            //var user = _userManager.FindByNameAsync(signUpCredentials.Name);
            if (signUpCredentials != null
                && _userManager.FindByNameAsync(signUpCredentials.Name).Result == null 
                && _userManager.FindByEmailAsync(signUpCredentials.Email).Result == null)
            {
                User newUser = new User
                {
                    UserName = signUpCredentials.Name,
                    Email = signUpCredentials.Email
                };


                var result = await _userManager.CreateAsync(newUser, signUpCredentials.Password);

                if (result.Succeeded)
                {
                    // установка куки
                    //await _signInManager.SignInAsync(newUser, false);
                    Console.WriteLine($"***** {newUser.UserName} Signed Up *****");
                    return newUser;
                }
                else
                {
                    Console.WriteLine($"!!!!! {newUser.UserName} Can`t be Signed Up !!!!!");
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        //public async Task<User> SignUp (string name, string email, Main)
        //{
        //    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email))
        //    {
        //        User user = new User { Name = name, Email = email };
        //        // добавляем пользователя
        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            // установка куки
        //            await _signInManager.SignInAsync(user, false);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description);
        //            }
        //        }
        //    }
        //    return View(model);
        //}

        //public void Logout() //TODO:
        //{

        //}
    }
}
