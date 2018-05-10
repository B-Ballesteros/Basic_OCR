using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static string ToBase64(this Image image)
        {
            string result;
            using (var mStream = new MemoryStream())
            {
                image.Save(mStream, ImageFormat.Png);
                var binaryImage = mStream.ToArray();
                result = Convert.ToBase64String(binaryImage);
            }
            return result;
        }
    }
}
