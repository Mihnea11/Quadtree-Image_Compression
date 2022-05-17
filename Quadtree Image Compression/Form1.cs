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
        private Bitmap? initialImage;
        private Bitmap? compressedImage;

        public ImageCompressionForm()
        {
            InitializeComponent();
            initialImage = null;
            compressedImage = null;
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
            if(PictureDisplay.Image == null)
            {
                return;
            }

            initialImage = new Bitmap(PictureDisplay.Image);
            compressedImage = null;
            QuadTree t = new QuadTree();
            Stopwatch timer = new Stopwatch();

            timer.Start();
            compressedImage = t.BuildTree(initialImage);
            timer.Stop();

            PictureDisplay.Image = compressedImage;

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

            elapsedTimeLabel.Text = elapsedTime;
        }

        private void SaveImageButton_Click(object sender, EventArgs e)
        {
            if(compressedImage == null)
            {
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "jpg(*.jpg)|.*jpg";

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                PictureDisplay.Image.Save(saveFileDialog.FileName + ".jpg");
            }
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
    }
}