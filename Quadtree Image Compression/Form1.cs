using System;
using System.IO;
using System.Diagnostics;
using System.Threading;

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

        private void CompressButton_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            // <->

            QuadTree t = new QuadTree();

            Bitmap image = new Bitmap(PictureDisplay.Image);
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