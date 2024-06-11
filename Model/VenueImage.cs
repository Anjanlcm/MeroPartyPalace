using System.ComponentModel.DataAnnotations.Schema;

namespace meroPartyPalace.Models
{
    [Table("tbl_VenueImage")]
    public class VenueImage
    {
        public int ID { get; set; }
        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] Image3 { get; set; }
        public byte[] Image4 { get; set; }
        public byte[] Image5 { get; set; }
    }
}
