using System.ComponentModel.DataAnnotations;

namespace MeroPartyPalace.Model
{
    public class User
    {
        public int UserID { get; set; }
        [Required]
        public string FirstName{ get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]

        [RegularExpression(@"^[\w-_]+(\.[\w!#$%'*+\/=?\^`{|}]+)*@((([\-\w]+\.)+[a-zA-Z]{2,20})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$")]
        public string UserEmail { get; set; }
        [Required]
        public string UserPassword { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Address_Province { get; set; }
        [Required]
        public string Address_District { get; set; }
        [Required]
        public string Address_City { get; set; }
        [Required]
        public string MobileNo { get; set; }
        public int RoleID { get; set; }
    }
}


