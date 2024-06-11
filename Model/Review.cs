using System.ComponentModel.DataAnnotations.Schema;

namespace meroPartyPalace.Models
{
    [Table("tbl_Review")]
    public class Review
    {
        public int ReviewID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public Venue Venue { get; set; }
        public User User { get; set; }
    }
}
