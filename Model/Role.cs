using System.ComponentModel.DataAnnotations.Schema;

namespace meroPartyPalace.Models
{
    [Table("tbl_Role")]
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
