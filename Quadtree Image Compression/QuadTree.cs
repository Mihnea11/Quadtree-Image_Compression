using System.Drawing.Imaging;

namespace Quadtree_Image_Compression
{
    internal class QuadTree
    {
        private QuadTreeNode? root;

        public Color ColorAverage(Bitmap image, Point startCorner, Point stopCorner)
        {
            long r = 0;
            long g = 0;
            long b = 0;

            int total = image.Width * image.Height;

            for (int i = 0; i < stopCorner.X; i++)
            {
                for (int j = startCorner.Y; j < stopCorner.Y; j++)
                {
                    Color clr = image.GetPixel(i, j);

                    r += clr.R;
                    g += clr.G;
                    b += clr.B;
                }
            }

            r /= total;
            g /= total;
            b /= total;

            return Color.FromArgb((int)r, (int)g, (int)b);
        }

        public QuadTree()
        {
            root = null;
        }

        public void BuildTree(Bitmap image, ref PictureBox box)
        {
            Color averageColor = ColorAverage(image, new Point(0, 0), new Point(image.Width, image.Height));

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
