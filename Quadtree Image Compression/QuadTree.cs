using System.Collections.Concurrent;
using System.Drawing.Imaging;

namespace Quadtree_Image_Compression
{
    internal class QuadTree
    {
        private QuadTreeNode root;
        private List<QuadTreeNode> compressedImage;
        private double detailTreshold;
        private int maxDepth;

        public delegate void QuadTreeStepUpdate(ImageCompressionSteps newStep);
        public event QuadTreeStepUpdate ProgressChanged;

        private static readonly double RedPercentage = 0.2989;
        private static readonly double GreenPercentage = 0.5870;
        private static readonly double BluePercentage = 0.1140;

        public QuadTree(double detailTreshold = 5, int maxDepth = 10)
        {
            this.detailTreshold = detailTreshold;
            this.maxDepth = maxDepth;
        }
        public double DetailTreshold
        {
            get { return detailTreshold; }
            set { detailTreshold = value; }
        }
        public int MaxDepth
        {
            get { return maxDepth; }
            set { maxDepth = value; }
        }
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
            var colorFrequency = new List<int>(new int[255]) { 0 };

            histogram.ToList().ForEach(p =>
            {
                int dataValue = 0;
                switch (colorName)
                {
                    case "Red":
                        dataValue = p.Key.R;
                        break;
                    case "Green":
                        dataValue = p.Key.G;
                        break;
                    case "Blue":
                        dataValue = p.Key.B;
                        break;
                }
                colorFrequency[dataValue] += p.Value;
            }
            );

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
                sum = sum + (colorFrequency[i] * ((value - i) * (value - i)) / total);
            }

            return sum;
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
        private void SplitQuadrant(Picture image, QuadTreeNode startingQuadrant)
        {
            var queue = new ConcurrentQueue<QuadTreeNode>();
            queue.Enqueue(startingQuadrant);

            while (queue.Count > 0)
            {
                if (queue.TryDequeue(out var node))
                {
                    NotifyNewStep(ImageCompressionSteps.AnalyzingImageNode);

                    var results = FindAverageColor(image, node.LeftCorner, node.RightCorner);
                    node.NodeColor = results.Item1;

                    var total = (node.RightCorner.X - node.LeftCorner.X) * (node.RightCorner.Y - node.LeftCorner.Y);

                    var greenFrequency = FindColorFrequencies(results.Item2, "Green");
                    node.NodeError = GreenPercentage * FindWeightedAverage(greenFrequency, total);

                    if (node.NodeError <= detailTreshold)
                    {
                        var redFrequency = FindColorFrequencies(results.Item2, "Red");
                        node.NodeError += RedPercentage * FindWeightedAverage(redFrequency, total);
                    }

                    if (node.NodeError <= detailTreshold)
                    {
                        var blueFrequency = FindColorFrequencies(results.Item2, "Blue");
                        node.NodeError += BluePercentage * FindWeightedAverage(blueFrequency, total);
                    }

                    if (node.NodeError > detailTreshold && node.NodeDepth < maxDepth)
                    {
                        NotifyNewStep(ImageCompressionSteps.SplitNode);

                        node.SplitNode();

                        node.GetChildren().ToList().ForEach(queue.Enqueue);
                    }
                }
            }
        }
        private void SplitTree(Picture image)
        {
            var quadrants = root.GetChildren();

            ThreadStart first = delegate { new QuadTree(detailTreshold, maxDepth).SplitQuadrant(image, quadrants[0]); };
            ThreadStart second = delegate { new QuadTree(detailTreshold, maxDepth).SplitQuadrant(image, quadrants[1]); };
            ThreadStart third = delegate { new QuadTree(detailTreshold, maxDepth).SplitQuadrant(image, quadrants[2]); };
            ThreadStart fourth = delegate { new QuadTree(detailTreshold, maxDepth).SplitQuadrant(image, quadrants[3]); };

            Thread firstQuadrant = new Thread(first);
            Thread secondQuadrant = new Thread(second);
            Thread thirdQuadrant = new Thread(third);
            Thread fourthQuadrant = new Thread(fourth);

            firstQuadrant.Start();
            secondQuadrant.Start();
            thirdQuadrant.Start();
            fourthQuadrant.Start();

            firstQuadrant.Join();
            secondQuadrant.Join();
            thirdQuadrant.Join();
            fourthQuadrant.Join();
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

                    node.GetChildren().ToList().ForEach(queue.Enqueue);
                }
            }
        }
        private void BuildImage(Picture image)
        {
            foreach (QuadTreeNode node in compressedImage)
            {
                for (int i = node.LeftCorner.X; i < node.RightCorner.X; i++)
                {
                    for (int j = node.LeftCorner.Y; j < node.RightCorner.Y; j++)
                    {
                        image.SetPixel(i, j, node.NodeColor);
                    }
                }
            }
        }
        public void BuildTree(Bitmap image, ref PictureBox box)
        {
            root = new QuadTreeNode();
            compressedImage = new List<QuadTreeNode>();

            NotifyNewStep(ImageCompressionSteps.None);

            root.LeftCorner = new Point(0, 0);
            root.RightCorner = new Point(image.Width, image.Height);
            root.NodeDepth = 0;

            NotifyNewStep(ImageCompressionSteps.LoadingImage);
            var picture = new Picture(image);
            NotifyNewStep(ImageCompressionSteps.ImageLoaded);

            NotifyNewStep(ImageCompressionSteps.SplitQuadTree);
            root.SplitNode();
            SplitTree(picture);
            NotifyNewStep(ImageCompressionSteps.QuadTreeCompleted);

            NotifyNewStep(ImageCompressionSteps.StartCompressingImage);
            FindCompressedImage();
            NotifyNewStep(ImageCompressionSteps.ImageCompressed);

            NotifyNewStep(ImageCompressionSteps.BuildImageInMemory);
            BuildImage(picture);
            NotifyNewStep(ImageCompressionSteps.ImageCompleted);

            NotifyNewStep(ImageCompressionSteps.StartWriteImageToBitmap);
            picture.SetBitmap(image);
            NotifyNewStep(ImageCompressionSteps.BitmapCompleted);

            box.Image = image;
            NotifyNewStep(ImageCompressionSteps.Completed);
        }
        private void NotifyNewStep(ImageCompressionSteps newStep)
        {
            ProgressChanged?.Invoke(newStep);
        }
    }
}