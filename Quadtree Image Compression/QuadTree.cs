using System.Drawing.Imaging;

namespace Quadtree_Image_Compression
{
    internal class QuadTree
    {
        private QuadTreeNode root;
        private List<QuadTreeNode> compressedImage;
        private double maxError;
        private int maxDepth;

        private Tuple<Color, Dictionary<Color, int>> FindColorAverage(Bitmap image, Point startCorner, Point stopCorner)
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

        private double FindColorValue(List<int> frequencyColor, long total)
        {
            double sum = 0;

            for (int i = 0; i < 255; i++)
            {
                sum = sum + (i * frequencyColor[i]);
            }

            return sum / total;
        }

        private double FindColorError(List<int> frequencyColor, long total, double value)
        {
            double sum = 0;

            for (int i = 0; i < 255; i++)
            {
                sum = sum + (frequencyColor[i] * ((value - i) * (value - i)));
            }

            return sum / total;
        }

        private double FindWeightedAverage(List<int> frequencyColor, long total)
        {
            double error, value;

            error = value = 0;

            if (total > 0)
            {
                value = FindColorValue(frequencyColor, total);
                error = FindColorError(frequencyColor, total, value);
                error = Math.Pow(error, 0.5);
            }

            return error;
        }

        private double FindDetail(List<int> frequencyColorRed, List<int> frequencyColorGreen, List<int> frequencyColorBlue, long total)
        {
            double redDetail = FindWeightedAverage(frequencyColorRed, total);
            double greenDetail = FindWeightedAverage(frequencyColorGreen, total);
            double blueDetail = FindWeightedAverage(frequencyColorBlue, total);

            return redDetail * 0.2989 + greenDetail * 0.5870 + blueDetail * 0.1140;
        }

        private void SplitTree(Bitmap image)
        {
            Queue<QuadTreeNode> queue = new Queue<QuadTreeNode>();

            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                QuadTreeNode node = queue.Dequeue();

                var results = FindColorAverage(image, node.LeftCorner, node.RightCorner);
                var redFrequency = FindColorFrequencies(results.Item2, "Red");
                var greenFrequency = FindColorFrequencies(results.Item2, "Green");
                var blueFrequency = FindColorFrequencies(results.Item2, "Blue");
                var detail = FindDetail(redFrequency, greenFrequency, blueFrequency, image.Width * image.Height);

                node.NodeColor = results.Item1;
                node.NodeError = detail;

                if(node.NodeError > maxError && node.NodeDepth < maxDepth)
                {
                    node.SplitNode();

                    node.GetChildrenIndex(0).NodeDepth = node.NodeDepth + 1;
                    node.GetChildrenIndex(1).NodeDepth = node.NodeDepth + 1;
                    node.GetChildrenIndex(2).NodeDepth = node.NodeDepth + 1;
                    node.GetChildrenIndex(3).NodeDepth = node.NodeDepth + 1;

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

        private void BuildImage(ref Bitmap image)
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
            maxError = 3;
            maxDepth = 10;
        }

        public void BuildTree(Bitmap image, ref PictureBox box)
        {
            root.LeftCorner = new Point(0, 0);
            root.RightCorner = new Point(image.Width, image.Height);
            root.NodeDepth = 0;

            SplitTree(image);
            FindCompressedImage();

            BuildImage(ref image);
            box.Image = image;
        }
    }
}
