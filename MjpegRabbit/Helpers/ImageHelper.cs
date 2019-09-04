using System.Drawing;
using System.IO;

namespace SimpleCpuMonitor.Helpers
{
    public static class ImageHelper
    {
        public static byte[] TextToImage(string text, string fontName, int fontSize, int textMargin = 0)
        {
            SizeF textSize;
            Image image;
            Graphics drawing;
            var font = new Font(fontName, fontSize);
            using (image = new Bitmap(1,1))
            {
                using (drawing = Graphics.FromImage(image))
                {
                    textSize = drawing.MeasureString(text, font);
                }
            }


            using (image = new Bitmap((int)textSize.Width + textMargin, (int)textSize.Height + textMargin))
            {
                drawing = Graphics.FromImage(image);
                var color = Color.White;
                var textColor = Color.Black;
                drawing.Clear(color);
                Brush textBrush = new SolidBrush(textColor);
                drawing.DrawString(text, font, textBrush, 0, 0);
                drawing.Save();

                return ImageToByteArray(image);
            }

        }

        public  static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}
