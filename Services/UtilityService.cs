using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats;

namespace MeroPartyPalace.Service
{
    public class UtilityService
    {
        public async Task<string> ConvertImageToBase64Async(List<IFormFile> imageFiles)
        {
            if (imageFiles == null || imageFiles.Count == 0)
            {
                throw new ArgumentException("No files provided.");
            }

            var imageFile = imageFiles[0];
            if (imageFile.Length == 0)
            {
                throw new ArgumentException("The file is empty.");
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(stream);
                    stream.Position = 0; // Ensure the stream is at the beginning

                    using (var image = Image.Load(stream)) // Use ImageSharp's Image.Load method
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            var encoder = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder(); // Choose appropriate encoder based on file type
                            image.Save(memoryStream, encoder); // Save image to memory stream with encoder
                            byte[] imageBytes = memoryStream.ToArray();
                            return Convert.ToBase64String(imageBytes);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new InvalidOperationException("Error processing the image file.", ex);
            }
        }
    }
}
