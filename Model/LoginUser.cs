using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MeroPartyPalace.Model
{
    public class LoginUser
    {
        public string UserEmail {  get; set; }

        public string UserPassword { get; set; }

    }
}
