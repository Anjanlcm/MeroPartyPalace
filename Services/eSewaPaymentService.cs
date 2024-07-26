using System;
using System.Security.Cryptography;
using System.Text;

public class eSewaPaymentService
{
    private readonly string _secretKey;

    public eSewaPaymentService()
    {
        _secretKey = "8gBm/:&EnhH.1/q("; // Secret key or any other required setup
    }

    public string GenerateSignature(string amount, string transactionUuid, string productCode)
    {
        // Parameters should be in the order of signed_field_names
        string dataToSign = $"amount={amount}&transaction_uuid={transactionUuid}&product_code={productCode}";

        using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey)))
        {
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataToSign));
            return Convert.ToBase64String(hash);
        }
    }

    public async Task<string> CreatePaymentAsync(decimal amount, string transactionUuid, string productCode, string successUrl, string failureUrl)
    {
        var signature = GenerateSignature(amount.ToString(), transactionUuid, productCode);

        // Construct the form data for eSewa
        var formData = new Dictionary<string, string>
        {
            { "amount", amount.ToString() },
            { "tax_amount", "0" },
            { "total_amount", amount.ToString() }, // Adjust if needed
            { "transaction_uuid", transactionUuid },
            { "product_code", productCode },
            { "product_service_charge", "0" },
            { "product_delivery_charge", "0" },
            { "success_url", successUrl },
            { "failure_url", failureUrl },
            { "signed_field_names", "total_amount,transaction_uuid,product_code" },
            { "signature", signature }
        };

        // Prepare the request content
        var content = new FormUrlEncodedContent(formData);

        // Send the request to eSewa
        var client = new HttpClient();
        var response = await client.PostAsync("https://epay.esewa.com.np/api/epay/main/v2/form", content);

        return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
    }

    // You might also need a method to handle the callback from eSewa
    public bool VerifyCallback(Dictionary<string, string> parameters, string receivedSignature)
    {
        var expectedSignature = GenerateSignature(parameters["total_amount"], parameters["transaction_uuid"], parameters["product_code"]);
        return expectedSignature == receivedSignature;
    }
}
