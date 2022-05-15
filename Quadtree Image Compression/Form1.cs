using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Text;

namespace Quadtree_Image_Compression
{
    public partial class ImageCompressionForm : Form
    {

        public ImageCompressionForm()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select Image";
            fileDialog.Filter = "Image File (*.jpg; *.jpeg; *.bmp; *.gif;) |*.jpg; *.jpeg; *.bmp; *.gif;";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap image = new Bitmap(fileDialog.FileName);
                PictureDisplay.Image = image;
            }
        }

        private Dictionary<Color, int> createHistogram(Bitmap bm)
        {
            // Store the histogram in a dictionary          
            Dictionary<Color, int> histogram = new Dictionary<Color, int>();

            for (int x = 0; x < bm.Width; x++)
            {
                for (int y = 0; y < bm.Height; y++)
                {
                    // Get pixel color 
                    Color c = bm.GetPixel(x, y);

                    // If it exists in our 'histogram' increment the corresponding value, or add new
                    if (histogram.ContainsKey(c))
                        histogram[c] = histogram[c] + 1;
                    else
                        histogram.Add(c, 1);
                }
            }

            return histogram;

        }

        public List<int> getListColor(Dictionary<Color, int> histogram, string colorName)
        {
            //Create a list for the frequency of each "COLOR range [0, 255] thing ?"      |     (we need another explain)
            List<int> frequencyColor = new List<int>(new int[255]) { 0 };

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
                            frequencyColor[dataValue] += histogram[key];
                            break;
                        }
                    case "Green":
                        {
                            dataValue = g;
                            frequencyColor[dataValue] += histogram[key];
                            break;
                        }
                    case "Blue":
                        {
                            dataValue = b;
                            frequencyColor[dataValue] += histogram[key];
                            break;
                        }
                }
            }

            return frequencyColor;

        }

        private void PrintListToFile(List<int> frequencyColor, string filePath, string fileName)
        {
            string path = filePath + fileName;
            try
            {
                if (File.Exists(path))
                    File.Delete(path);

                StreamWriter sw = new StreamWriter(path, true, Encoding.ASCII);

                foreach (var color in frequencyColor)
                {
                    sw.WriteLine(color);
                }

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

        }

        private double getValue(List<int> frequencyColor, long total)
        {
            double sum = 0;

            for (int i = 0; i < 255; i++)
                sum = sum + (i * frequencyColor[i]);

            return sum / total;
        }

        private double getError(List<int> frequencyColor, long total, double value)
        {
            double sum = 0;

            for (int i = 0; i < 255; i++)
                sum = sum + (frequencyColor[i] * ((value - i) * (value - i)));

            return sum / total;
        }

        //Save data logs (in a *.txt file)
        private void CreateLogsFile(string filePath, Bitmap bm, Image image, Dictionary<Color, int> histogram)
        {
            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);

                StreamWriter sw = new StreamWriter(filePath, true, Encoding.ASCII);

                int cosmin = 0;

                //Write out the frequency for each color.
                foreach (Color key in histogram.Keys)
                {
                    byte r = key.R;
                    byte g = key.G;
                    byte b = key.B;

                    if (r == 3)
                        cosmin += histogram[key];

                    sw.WriteLine(key.ToString() + ": " + histogram[key]);
                    sw.WriteLine("Red: " + r + " | Green: " + g + " | Blue: " + b + " | No. of appears: " + histogram[key]);
                    sw.WriteLine();
                }

                sw.WriteLine(cosmin);

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        private double weightedAverage(List<int> frequencyColor, long total)
        {
            double error, value;

            error = value = 0;

            if (total > 0)
            {
                value = getValue(frequencyColor, total);
                error = getError(frequencyColor, total, value);
                error = Math.Pow(error, 0.5);
            }

            return error;
        }

        private double getDetail(List<int> frequencyColorRed, List<int> frequencyColorGreen, List<int> frequencyColorBlue, long total)
        {
            double redDetail = weightedAverage(frequencyColorRed, total);
            double greenDetail = weightedAverage(frequencyColorGreen, total);
            double blueDetail = weightedAverage(frequencyColorBlue, total);

            return redDetail * 0.2989 + greenDetail * 0.5870 + blueDetail * 0.1140;
        }

        private void CompressButton_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            // <->

            QuadTree t = new QuadTree();

            Bitmap image = new Bitmap(PictureDisplay.Image);

            Dictionary<Color, int> histogram = createHistogram(image);

            List<int> frequencyColorRed = getListColor(histogram, "Red");
            List<int> frequencyColorGreen = getListColor(histogram, "Green");
            List<int> frequencyColorBlue = getListColor(histogram, "Blue");

            PrintListToFile(frequencyColorRed, "E:\\", "outputRed.txt");
            PrintListToFile(frequencyColorGreen, "E:\\", "outputGreen.txt");
            PrintListToFile(frequencyColorBlue, "E:\\", "outputBlue.txt");

            //se va rescrie mai tarziu unde trebuie (vezi GitHub (o_o) )
            long total = histogram.Sum(v => v.Value);

            double detail = getDetail(frequencyColorRed, frequencyColorGreen, frequencyColorBlue, total);

            Color averageColor = t.ColorAverage(image, new Point(0, 0), new Point(image.Width, image.Height));

            /*
                double REDvalue = getValue(frequencyColorRed, total);

                double REDerror = getError(frequencyColorRed, total, REDvalue);

                double GREENvalue = getValue(frequencyColorGreen, total);

                double GREENerror = getError(frequencyColorGreen, total, GREENvalue);

                double BLUEvalue = getValue(frequencyColorBlue, total);

                double BLUEerror = getError(frequencyColorBlue, total, BLUEvalue);

                double detail_intensity = REDerror * 0.2989 + GREENerror * 0.5870 + BLUEerror * 0.1140;
            */

            t.BuildTree(image, ref PictureDisplay);

            // <->

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}.{1:00}s", ts.Seconds, ts.Milliseconds / 10);

            if (ts.Minutes >= 1)
                elapsedTime = String.Format("{0:00}min si {1:00}.{2:00}s", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            else if (ts.Hours >= 1)
                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            elapsedTimeLabel.Text = elapsedTime;
        }

        private void ImageCompressionForm_Load(object sender, EventArgs e)
        {

        }
    }
}