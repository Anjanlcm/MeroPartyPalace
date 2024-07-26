using System.Drawing;

namespace MeroPartyPalace.Service
{
    public class UtilityService
    {
        public string ConvertImageToBase64(List<IFormFile> imageFiles)
        {
            if (imageFiles == null || imageFiles.Count == 0)
            {
                throw new ArgumentException("The provided file is invalid.");
            }
            var imageFile = imageFiles[0];
            using (var stream = new MemoryStream())
            {
                imageFile.CopyTo(stream);
                //stream.Seek(0, SeekOrigin.Begin);
                using (Image image = Image.FromStream(stream))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        image.Save(memoryStream, image.RawFormat);
                        byte[] imageBytes = memoryStream.ToArray();

                        // Convert byte[] to Base64 string
                        string imageString = Convert.ToBase64String(imageBytes);
                        return imageString;
                    }
                }
            }
        }
    }
}