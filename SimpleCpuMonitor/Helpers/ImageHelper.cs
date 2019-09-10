using System.Drawing;
using System.IO;

namespace SimpleCpuMonitor.Helpers
{
    /// <summary>
    /// Хелпео для Image
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// Преобразовать текст в изображение
        /// </summary>
        /// <param name="text">Текст</param>
        /// <param name="fontName">Название шрифта</param>
        /// <param name="fontSize">Размер шрифта</param>
        /// <param name="textMargin">Отступы</param>
        /// <returns></returns>
        public static byte[] TextToImage(string text, string fontName, int fontSize)
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

            using (image = new Bitmap((int)textSize.Width, (int)textSize.Height))
            {
                drawing = Graphics.FromImage(image);
                var color = Color.White;
                var textColor = Color.Black;
                drawing.Clear(color);
                Brush textBrush = new SolidBrush(textColor);
                drawing.DrawString(text, font, textBrush, 0, 0);
                drawing.Save();

                return image.ToByteArray();
            }

        }

        /// <summary>
        /// Преобразовать Image в массив байт
        /// </summary>
        /// <param name="image">Исходное изображение</param>
        /// <returns></returns>
        public static byte[] ToByteArray(this Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}
