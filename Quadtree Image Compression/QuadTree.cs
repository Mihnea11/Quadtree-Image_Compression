using System.Drawing.Imaging;

namespace Quadtree_Image_Compression
{
    internal class QuadTree
    {
        private QuadTreeNode root;
        private List<QuadTreeNode> compressedImage;
        private double detailTreshold;
        private int maxDepth;

        private Tuple<Color, Dictionary<Color, int>> FindAverageColor(Picture image, Point startCorner, Point stopCorner)
        {
            Dictionary<Color, int> histogram = new Dictionary<Color, int>();
            double r = 0;
            double g = 0;
            double b = 0;

            int total = (stopCorner.X - startCorner.X) * (stopCorner.Y - startCorner.Y);

            for (int i = startCorner.X; i < stopCorner.X; i++)
            {
                for (int j = startCorner.Y; j < stopCorner.Y; j++)
                {
                    Color color = image.GetPixel(i, j);

                    r += color.R;
                    g += color.G;
                    b += color.B;

                    if (histogram.ContainsKey(color))
                    {
                        histogram[color] = histogram[color] + 1;
                    }
                    else
                    {
                        histogram.Add(color, 1);
                    }
                }
            }

            r /= total;
            g /= total;
            b /= total;

            Color average = Color.FromArgb((int)r, (int)g, (int)b);
            return new Tuple<Color, Dictionary<Color, int>>(average, histogram);
        }

        public List<int> FindColorFrequencies(Dictionary<Color, int> histogram, string colorName)
        {
            List<int> colorFrequency = new List<int>(new int[255]) { 0 };

            foreach (Color key in histogram.Keys)
            {
                byte r = key.R;
                byte g = key.G;
                byte b = key.B;

                int dataValue = 0;

                switch (colorName)
                {
                    case "Red":
                        {
                            dataValue = r;
                            colorFrequency[dataValue] += histogram[key];
                            break;
                        }
                    case "Green":
                        {
                            dataValue = g;
                            colorFrequency[dataValue] += histogram[key];
                            break;
                        }
                    case "Blue":
                        {
                            dataValue = b;
                            colorFrequency[dataValue] += histogram[key];
                            break;
                        }
                }
            }

            return colorFrequency;
        }

        private double FindColorValue(List<int> colorFrequency, long total)
        {
            double sum = 0;

            for (int i = 0; i < 255; i++)
            {
                sum = sum + (i * colorFrequency[i]);
            }

            return sum / total;
        }

        private double FindColorIntensity(List<int> colorFrequency, long total, double value)
        {
            double sum = 0;

            for (int i = 0; i < 255; i++)
            {
                sum = sum + (colorFrequency[i] * ((value - i) * (value - i)));
            }

            return sum / total;
        }

        private double FindWeightedAverage(List<int> colorFrequency, long total)
        {
            double intesity, value;

            intesity = value = 0;

            if (total > 0)
            {
                value = FindColorValue(colorFrequency, total);
                intesity = FindColorIntensity(colorFrequency, total, value);
                intesity = Math.Pow(intesity, 0.5);
            }

            return intesity;
        }

        private double FindZoneDetail(List<int> redFrequency, List<int> greenFrequency, List<int> blueFrequency, long total)
        {
            double redIntensity = FindWeightedAverage(redFrequency, total);
            double greenIntensity = FindWeightedAverage(greenFrequency, total);
            double blueIntensity = FindWeightedAverage(blueFrequency, total);

            return redIntensity * 0.2989 + greenIntensity * 0.5870 + blueIntensity * 0.1140;
        }

        private void SplitTree(Picture image)
        {
            Queue<QuadTreeNode> queue = new Queue<QuadTreeNode>();

            queue.Enqueue(root.GetChildrenIndex(0));
            queue.Enqueue(root.GetChildrenIndex(1));
            queue.Enqueue(root.GetChildrenIndex(2));
            queue.Enqueue(root.GetChildrenIndex(3));

            while (queue.Count > 0)
            {
                QuadTreeNode node = queue.Dequeue();

                var results = FindAverageColor(image, node.LeftCorner, node.RightCorner);
                var redFrequency = FindColorFrequencies(results.Item2, "Red");
                var greenFrequency = FindColorFrequencies(results.Item2, "Green");
                var blueFrequency = FindColorFrequencies(results.Item2, "Blue");
                var zoneDetail = FindZoneDetail(redFrequency, greenFrequency, blueFrequency, (node.RightCorner.X - node.LeftCorner.X) * (node.RightCorner.Y - node.LeftCorner.Y));

                node.NodeColor = results.Item1;
                node.NodeError = zoneDetail;

                if(node.NodeError > detailTreshold && node.NodeDepth < maxDepth)
                {
                    node.SplitNode();

                    queue.Enqueue(node.GetChildrenIndex(0));
                    queue.Enqueue(node.GetChildrenIndex(1));
                    queue.Enqueue(node.GetChildrenIndex(2));
                    queue.Enqueue(node.GetChildrenIndex(3));
                }
            }
        }

        private void FindCompressedImage()
        {
            Queue<QuadTreeNode> queue = new Queue<QuadTreeNode>();

            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                QuadTreeNode node = queue.Dequeue();

                if (node != null)
                {
                    if (node.IsLeaf == true)
                    {
                        compressedImage.Add(node);
                    }

                    queue.Enqueue(node.GetChildrenIndex(0));
                    queue.Enqueue(node.GetChildrenIndex(1));
                    queue.Enqueue(node.GetChildrenIndex(2));
                    queue.Enqueue(node.GetChildrenIndex(3));
                }
            }
        }

        private void BuildImage(Picture image)
        {
            foreach (QuadTreeNode node in compressedImage)
            {
                for (int i = node.LeftCorner.X; i < node.RightCorner.X; i++)
                {
                    for(int j = node.LeftCorner.Y; j < node.RightCorner.Y; j++)
                    {
                        image.SetPixel(i, j, node.NodeColor);
                    }
                }
            }
        }

        public QuadTree()
        {
            root = new QuadTreeNode();
            compressedImage = new List<QuadTreeNode>();
            detailTreshold = 10;
            maxDepth = 6;
        }

        public Bitmap BuildTree(Bitmap image)
        {
            Picture picture = new Picture(image);

            root = new QuadTreeNode();
            root.LeftCorner = new Point(0, 0);
            root.RightCorner = new Point(picture.Width, picture.Height);

            root.SplitNode();
            SplitTree(picture);
            FindCompressedImage();
            BuildImage(picture);

            picture.SetBitmap(image);
            return image;
        }
    }
}