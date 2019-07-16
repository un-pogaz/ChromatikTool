using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Text;

namespace System.Drawing
{
    static public class DrawingExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Runtime.InteropServices.ExternalException"></exception>
        static public void SavePNG(this Bitmap bmp, string path)
        {
            bmp.Save(path, ImageFormat.Png);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="Runtime.InteropServices.ExternalException"></exception>
        static public void SaveJPG(this Bitmap bmp, string path, int percent)
        {
            if (percent < 0)
                throw new ArgumentOutOfRangeException("percent");
            if (percent > 100)
                throw new ArgumentOutOfRangeException("percent");
            
            path = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + ".jpg";

            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = new EncoderParameter(Imaging.Encoder.Quality, percent);

            bmp.Save(path, GetImageCodec(ImageFormat.Jpeg), myEncoderParameters);
        }

        static public ImageCodecInfo GetImageCodec(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
                if (codec.FormatID == format.Guid)
                    return codec;
            return null;
        }
    }
}
