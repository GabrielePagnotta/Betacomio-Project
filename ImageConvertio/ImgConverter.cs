using System.Drawing;
using System.Drawing.Imaging;

namespace ImageConvertio
{
    public class ImgConverter
    {
        // ImageConverter object used to convert byte arrays containing JPEG or PNG file images into 
        //  Bitmap objects. This is static and only gets instantiated once.
        private static readonly ImageConverter _imageConverter = new ImageConverter();


        /// <summary>
        /// Method to "convert" an Image object into a byte array, formatted in PNG file format, which 
        /// provides lossless compression. This can be used together with the GetImageFromByteArray() 
        /// method to provide a kind of serialization / deserialization. 
        /// </summary>
        /// <param name="picture">Image object, must be convertable to PNG format</param>
        /// <returns>byte array image of a PNG file containing the image</returns>
        public static byte[] CopyImageToByteArray(Image picture)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                picture.Save(memoryStream, ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }


        /// <summary>
        /// Method that uses the ImageConverter object in .Net Framework to convert a byte array, 
        /// presumably containing a JPEG or PNG file image, into a Bitmap object, which can also be 
        /// used as an Image object.
        /// </summary>
        /// <param name="byteArray">byte array containing JPEG or PNG file image or similar</param>
        /// <returns>Bitmap object if it works, else exception is thrown</returns>
        public static Bitmap GetImageFromByteArray(byte[] byteArray)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            using (Bitmap bm = (Bitmap)_imageConverter.ConvertFrom(byteArray))
            {
                if (bm != null && (bm.HorizontalResolution != (int)bm.HorizontalResolution ||
                   bm.VerticalResolution != (int)bm.VerticalResolution))
                {
                    // Correct a strange glitch that has been observed in the test program when converting 
                    //  from a PNG file image created by CopyImageToByteArray() - the dpi value "drifts" 
                    //  slightly away from the nominal integer value
                    bm.SetResolution((int)(bm.HorizontalResolution + 0.5f),
                                     (int)(bm.VerticalResolution + 0.5f));
                }

                return bm;
            }

        }


        public byte[] ConvertImageToByteArray(Image image)
        {

            if (image != null)
            {
                // Utilizza il metodo CopyImageToByteArray per convertire l'immagine in un array di byte
                byte[] byteArray = CopyImageToByteArray(image);

                image.Dispose();

                return byteArray;
            }
            else
            {
                return null;
            }
        }

        public void ConvertByteArrayToHexString(byte[] byteArray)
        {

            try
            {

                string hexadecimalString = BitConverter.ToString(byteArray).Replace("-", "");


            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore durante conversione: " + ex.Message);
            }


        }
    }
}


