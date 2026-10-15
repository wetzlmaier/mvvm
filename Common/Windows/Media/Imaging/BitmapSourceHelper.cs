using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Common.Windows.Media.Imaging
{
    public static class BitmapSourceHelper
    {
        public static BitmapSource? LoadImage(Image? source)
        {
            if (source == null)
                return null;

            return LoadBitmap(new Bitmap(source));
        }

        public static BitmapSource? LoadBitmap(Bitmap? source)
        {
            if (source == null)
                return null;

            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(source.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }

        public static Bitmap? SaveBitmap(BitmapSource? source)
        {
            if (source == null)
                return null;

            using var outStream = new MemoryStream();

            BitmapEncoder enc = new BmpBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(source));
            enc.Save(outStream);
            var bitmap = new Bitmap(outStream);

            return new Bitmap(bitmap);
        }

        public static BitmapSource LoadBitmap(string filename)
        {
            using var stream = File.OpenRead(filename);
            return LoadBitmap(stream);
        }

        public static BitmapSource LoadBitmap(Stream stream)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = stream;
            image.EndInit();

            return image;
        }
    }
}
