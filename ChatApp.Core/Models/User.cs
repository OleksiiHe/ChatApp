using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core
{
    public class User : IdentityUser
    {
        //public string Name { get; set; }
        //public string Password { get; set; }
        public string Test { get; set; } //TODO: Test Field


        //public int Id { get; set; }
        ////public string ID { get; set; }

        //public bool IsLoggedIn { get; set; }
        //public bool HasSentNewMessage { get; set; }
        //public bool IsTyping { get; set; }
    }
}
