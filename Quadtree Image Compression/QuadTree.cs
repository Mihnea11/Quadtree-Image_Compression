using System.Drawing.Imaging;

namespace Quadtree_Image_Compression
{
    internal class QuadTree
    {
        private QuadTreeNode? root;

        private static Color NearestCluserColor(Color inputColor)
        {
            var inputRed = Convert.ToDouble(inputColor.R);
            var inputGreen = Convert.ToDouble(inputColor.G);
            var inputBlue = Convert.ToDouble(inputColor.B);
            var colors = new List<Color>();

            foreach (var knownColor in Enum.GetValues(typeof(KnownColor)))
            {
                var color = Color.FromKnownColor((KnownColor)knownColor);
                if (!color.IsSystemColor)
                {
                    colors.Add(color);
                }
            }

            var nearestColor = Color.Empty;
            var distance = 500.0;

            foreach (var color in colors)
            {
                var testRed = Math.Pow(Convert.ToDouble(color.R) - inputRed, 2.0);
                var testGreen = Math.Pow(Convert.ToDouble(color.G) - inputGreen, 2.0);
                var testBlue = Math.Pow(Convert.ToDouble(color.B) - inputBlue, 2.0);
                var tempDistance = Math.Sqrt(testBlue + testGreen + testRed);

                if (tempDistance == 0.0)
                {
                    return color;
                }
                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    nearestColor = color;
                }
            }

            return nearestColor;
        }

        public static Color DominantColor(Bitmap bitMap)
        {
            var colorIncidence = new Dictionary<int, int>();

            for (var x = 0; x < bitMap.Size.Width; x++)
            {
                for (var y = 0; y < bitMap.Size.Height; y++)
                {
                    var pixelColor = bitMap.GetPixel(x, y).ToArgb();
                    if (colorIncidence.Keys.Contains(pixelColor))
                    {
                        colorIncidence[pixelColor]++;
                    }
                    else
                    {
                        colorIncidence.Add(pixelColor, 1);
                    }
                }
            }

            return Color.FromArgb(colorIncidence.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value).First().Key);
        }

        public QuadTree()
        {
            root = null;
        }

        public void BuildTree(Bitmap image, ref PictureBox box)
        {
            Color averageColor = DominantColor(image);

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
