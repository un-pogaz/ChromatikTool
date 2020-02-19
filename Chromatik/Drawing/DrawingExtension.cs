using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Text;

namespace System.Drawing
{
    /// <summary>
    /// Static extension class for <see cref="Image"/>
    /// </summary>
    static public class DrawingExtension
    {
        /// <summary>
        /// Save a PNG image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="path"></param>
        static public void SavePNG(this Image img, string path)
        {
            img.Save(path, ImageFormat.Png);
        }

        /// <summary>
        /// Save a JPG image with 90% compression.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="path"></param>
        static public void SaveJPG(this Image img, string path)
        {
            img.SaveJPG(path, 90);
        }
        /// <summary>
        /// Save a JPG image with a specified percentage compression.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="path"></param>
        /// <param name="compression">Percentage of compression</param>
        static public void SaveJPG(this Image img, string path, int compression)
        {
            if (compression < 1)
                compression = 1;
            if (compression > 100)
                compression = 100;

            path = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + ".jpg";

            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = new EncoderParameter(Imaging.Encoder.Quality, compression);

            img.Save(path, ImageFormat.Jpeg.GetImageCodec(), myEncoderParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        static public ImageCodecInfo GetImageCodec(this ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
                if (codec.FormatID == format.Guid)
                    return codec;
            return null;
        }
    }
}
