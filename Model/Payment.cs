using System.ComponentModel.DataAnnotations.Schema;

namespace meroPartyPalace.Models
{
    [Table("tbl_Payment")]
    public class Payment
    {
        public int PaymentID { get; set; }
        public int PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentTime { get; set; }
        public Booking Booking { get; set; }
    }
}
