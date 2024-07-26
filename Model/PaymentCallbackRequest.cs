using System.ComponentModel.DataAnnotations;

namespace MeroPartyPalace.Model
{
    public class PaymentCallbackRequest
    {
        [Required]
        public string TotalAmount { get; set; }

        [Required]
        public string TransactionUuid { get; set; }

        [Required]
        public string ProductCode { get; set; }

        [Required]
        public string Signature { get; set; }
    }
}
