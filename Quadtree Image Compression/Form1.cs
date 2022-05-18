using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Text;

namespace Quadtree_Image_Compression
{
    public partial class ImageCompressionForm : Form
    {
        private bool mouseDown;
        private Point mouseOffset;
        private QuadTree tree;
        private Stopwatch timer;

        public ImageCompressionForm()
        {
            InitializeComponent();

            tree = new QuadTree();
            tree.ProgressChanged += CompressImageNewStepUpdated;

            timer = new Stopwatch();

            CompressButton.Enabled = false;
            SaveImageButton.Enabled = false;
            LoadingBar.Visible = false;
            CompressionTime.Visible = false;
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

                CompressButton.Enabled = true;
                SaveImageButton.Enabled = false;
                CompressionTime.Visible = false;
                LoadingBar.Value = 0;
            }
        }
        private void CompressButton_Click(object sender, EventArgs e)
        {
            timer.Reset();
            LoadButton.Enabled = false;
            CompressButton.Enabled = false;
            SaveImageButton.Enabled = false;
            LoadingBar.Visible = true;

            CompressionRate.Text = CompressionRateSlider.Value.ToString();
            DetailLevel.Text = DetailLevelSlider.Value.ToString();

            tree.DetailTreshold = CompressionRateSlider.Value;
            tree.MaxDepth = DetailLevelSlider.Value;

            backgroundWorker1.RunWorkerAsync();
        }   
        private void SaveImageButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "jpg(*.jpg)|.*jpg";

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                PictureDisplay.Image.Save(saveFileDialog.FileName + ".jpg");
            }
        }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            timer.Start();
            Bitmap image = new Bitmap(PictureDisplay.Image);
            tree.BuildTree(image, ref PictureDisplay);
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            LoadingBar.Value = 100;
            LoadButton.Enabled = true;
            SaveImageButton.Enabled = true;
            LoadingBar.Visible = false;
            timer.Stop();

            CompressionTime.Visible = true;
            TimeSpan timeSpan = timer.Elapsed;
            string elapsedTime = String.Format("{0:00}.{1:00}s", timeSpan.Seconds, timeSpan.Milliseconds / 10);
            if (timeSpan.Minutes >= 1)
            {
                elapsedTime = String.Format("{0:00}min si {1:00}.{2:00}s", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
            }
            else if (timeSpan.Hours >= 1)
            {
                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
            }
            CompressionTime.Text = "Elapsed time: " + elapsedTime;
            CompressionTime.Location = new Point(panel1.Width / 2 - CompressionTime.Width / 2, 0);
        }
        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            LoadingBar.Value = e.ProgressPercentage;
        }
        private void CompressImageNewStepUpdated(ImageCompressionSteps newStep)
        {
            var progressInPercentage = LoadingBar.Value;

            switch (newStep)
            {
                case ImageCompressionSteps.LoadingImage:
                    progressInPercentage = 5;
                    break;

                case ImageCompressionSteps.ImageLoaded:
                case ImageCompressionSteps.SplitQuadTree:
                    progressInPercentage = 15;
                    break;

                case ImageCompressionSteps.AnalyzingImageNode:
                    if (progressInPercentage < 80)
                    {
                        progressInPercentage++;
                    }
                    break;

                case ImageCompressionSteps.QuadTreeCompleted:
                    progressInPercentage = 80;
                    break;

                case ImageCompressionSteps.SplitNode:
                    break;

                case ImageCompressionSteps.StartCompressingImage:
                    progressInPercentage = 81;
                    break;

                case ImageCompressionSteps.ImageCompressed:
                case ImageCompressionSteps.BuildImageInMemory:
                    progressInPercentage = 90;
                    break;

                case ImageCompressionSteps.ImageCompleted:
                case ImageCompressionSteps.StartWriteImageToBitmap:
                    progressInPercentage = 95;
                    break;

                case ImageCompressionSteps.BitmapCompleted:
                case ImageCompressionSteps.Completed:
                    progressInPercentage = 100;
                    break;
            }

            backgroundWorker1.ReportProgress(progressInPercentage);
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void MouseDownEvent(object sender, MouseEventArgs e)
        {
            mouseOffset.X = e.X;
            mouseOffset.Y = e.Y;
            mouseDown = true;
        }
        private void MouseMoveEvent(object sender, MouseEventArgs e)
        {
            if(mouseDown == true)
            {
                Point currentPosition = PointToScreen(e.Location);
                Location = new Point(currentPosition.X - mouseOffset.X, currentPosition.Y - mouseOffset.Y);
            }
        }
        private void MouseUpEvent(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        private void CompressionRateSlider_Scroll(object sender, EventArgs e)
        {
            CompressionRate.Text = CompressionRateSlider.Value.ToString();
        }
        private void CompressionRate_TextChanged(object sender, EventArgs e)
        {
            int value = 0;

            if(CompressionRate.Text != "")
            {
                var isNumber = int.TryParse(CompressionRate.Text, out value);
                if(isNumber == false)
                {
                    value = 0;
                }
                else if(value < 0)
                {
                    value = 0;
                }
                else if(value > 10)
                {
                    value = 10;
                }
            }

            CompressionRateSlider.Value = value;
        }
        private void DetailLevelSlider_Scroll(object sender, EventArgs e)
        {
            DetailLevel.Text = DetailLevelSlider.Value.ToString();
        }
        private void DetailLevel_TextChanged(object sender, EventArgs e)
        {
            int value = 0;

            if (DetailLevel.Text != "")
            {
                var isNumber = int.TryParse(DetailLevel.Text, out value);
                if (isNumber == false)
                {
                    value = 0;
                }
                else if (value < 0)
                {
                    value = 0;
                }
                else if (value > 10)
                {
                    value = 10;
                }
            }

            DetailLevelSlider.Value = value;
        }
    }
}