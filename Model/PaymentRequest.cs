using System.ComponentModel.DataAnnotations;

namespace MeroPartyPalace.Model
{
    public class PaymentRequest
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string TransactionUuid { get; set; }

        [Required]
        public string ProductCode { get; set; }

        [Required]
        public string SuccessUrl { get; set; }

        [Required]
        public string FailureUrl { get; set; }
    }
}
