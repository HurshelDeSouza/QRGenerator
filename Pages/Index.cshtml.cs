using Microsoft.AspNetCore.Mvc.RazorPages;
using QRCoder;

namespace QRGenerator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;        

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public string? QrCode { get; set; }

        public void OnPost()
        {
            string texto = Request.Form["texto"];

            if (!string.IsNullOrEmpty(texto))
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);
                PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
                byte[] qrCodeImage = qrCode.GetGraphic(20);
                QrCode = Convert.ToBase64String(qrCodeImage);
            }
        }
    }
}
