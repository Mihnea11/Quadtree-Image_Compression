using System.Drawing.Imaging;

namespace Quadtree_Image_Compression
{
    internal class Picture
    {
        private byte[] RGBdata;
        public int Width { get; init; }
        public int Height { get; init; }

        private void LoadPixelData(Bitmap image)
        {
            var rectangle = new Rectangle(0, 0, image.Width, image.Height);
            var bitmapData = image.LockBits(rectangle, ImageLockMode.ReadOnly, image.PixelFormat);

            IntPtr linePointer = bitmapData.Scan0;

            var bytes = Math.Abs(bitmapData.Stride) * image.Height;
            RGBdata = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(linePointer, RGBdata, 0, bytes);

            image.UnlockBits(bitmapData);
        }
        #pragma warning disable CS8618
        public Picture(Bitmap image)
        {
            Width = image.Width;
            Height = image.Height;

            LoadPixelData(image);
        }
        #pragma warning restore CS8618
        public Color GetPixel(int x, int y)
        {
            var red = RGBdata[(x * 4) + (y * 4 * Width) + 2];
            var green = RGBdata[(x * 4) + (y * 4 * Width) + 1];
            var blue = RGBdata[(x * 4) + (y * 4 * Width) + 0];

            return Color.FromArgb(red, green, blue);
        }
        public void SetPixel(int x, int y, Color color)
        {
            RGBdata[(x * 4) + (y * 4 * Width) + 2] = color.R;
            RGBdata[(x * 4) + (y * 4 * Width) + 1] = color.G;
            RGBdata[(x * 4) + (y * 4 * Width) + 0] = color.B;
        }
        public void SetBitmap(Bitmap image)
        {
            var rectangle = new Rectangle(0, 0, image.Width, image.Height);
            var bmpData = image.LockBits(rectangle, System.Drawing.Imaging.ImageLockMode.WriteOnly, image.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            var bytes = Math.Abs(bmpData.Stride) * image.Height;
            System.Runtime.InteropServices.Marshal.Copy(RGBdata, 0, ptr, bytes);

            image.UnlockBits(bmpData);
        }
    }
}