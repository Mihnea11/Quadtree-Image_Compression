using System.Drawing.Imaging;

namespace Quadtree_Image_Compression
{
    internal class QuadTree
    {
        private QuadTreeNode? root;
        private List<Bitmap> imageProgression;
        
        private Color FindAverageColor(Bitmap image, Point start, Point stop)
        {
            BitmapData imageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int stride = imageData.Stride;
            IntPtr scan0 = imageData.Scan0;

            long[] totals = new long[] { 0, 0, 0 };

            int width = image.Width;
            int height = image.Height;

            unsafe
            {
                byte* pixels = (byte*)(void*)scan0;

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        for (int color = 0; color < 3; color++)
                        {
                            int index = (i * stride) + j * 4 + color;

                            totals[color] += pixels[index];
                        }
                    }
                }
            }

            int R = (int)(totals[0] / (width * height));
            int G = (int)(totals[1] / (width * height));
            int B = (int)(totals[2] / (width * height));

            image.UnlockBits(imageData);

            return Color.FromArgb(R, G, B);
        }

        public QuadTree()
        {
            root = null;
            imageProgression = new List<Bitmap>();
        }

        public void BuildTree(Bitmap image, ref PictureBox box)
        {
            Color averageColor = FindAverageColor(image, new Point(0, 0), new Point(image.Width, image.Height));

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    image.SetPixel(i, j, averageColor);
                }
            }

            box.Image = image;
        }
    }
}
