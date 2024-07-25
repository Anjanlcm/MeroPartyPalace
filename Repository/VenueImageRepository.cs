//using System.Drawing.Imaging.ImageFormat;

//namespace MeroPartyPalace.Repository
//{
//    public class VenueImageRepository
//    {
//        public byte[] ConvertImageToBinary(Image image)
//        {
//            using (MemoryStream ms = new MemoryStream())
//            {
//                if (image.RawFormat.Equals(Jpeg))
//                {
//                    image.Save(ms, Jpeg);
//                }
//                else if (image.Png))
//                {
//                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
//                }
//                else
//                {
//                    // Handle other image formats if needed
//                    throw new NotSupportedException("Image format not supported.");
//                }

//                return ms.ToArray();
//            }
//        }

//        public Image ConvertBinaryToImage(byte[] data)
//        {
//            if (data == null || data.Length == 0)
//            {
//                return null; // or return a default image placeholder if desired
//            }

//            try
//            {
//                using (MemoryStream ms = new MemoryStream(data))
//                {
//                    return Image.FromStream(ms);
//                }
//            }
//            catch (Exception ex)
//            {
//                // Handle or log the exception
//                Console.WriteLine($"Error converting binary to image: {ex.Message}");
//                return null; // or return a default image placeholder or handle the error accordingly
//            }
//        }
//    }
//}
