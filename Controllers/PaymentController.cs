using MeroPartyPalace.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly eSewaPaymentService _eSewaPaymentService;

    public PaymentController(eSewaPaymentService eSewaPaymentService)
    {
        _eSewaPaymentService = eSewaPaymentService;
    }


    [HttpPost("create")]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _eSewaPaymentService.CreatePaymentAsync(request.Amount, request.TransactionUuid, request.ProductCode, request.SuccessUrl, request.FailureUrl);
            return Ok(result); // Return the redirect URL or form response
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("callback")]
    public IActionResult PaymentCallback([FromForm] PaymentCallbackRequest callbackRequest)
    {
        if (callbackRequest == null || !ModelState.IsValid)
        {
            return BadRequest("Invalid callback request");
        }

        var parameters = new Dictionary<string, string>
        {
            { "total_amount", callbackRequest.TotalAmount },
            { "transaction_uuid", callbackRequest.TransactionUuid },
            { "product_code", callbackRequest.ProductCode }
        };

        var isValid = _eSewaPaymentService.VerifyCallback(parameters, callbackRequest.Signature);
        if (isValid)
        {
            // Handle successful verification
            return Ok();
        }
        else
        {
            // Handle verification failure
            return BadRequest("Invalid signature");
        }
    }
}
