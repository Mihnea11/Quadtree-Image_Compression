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

        private void CompressButton_Click(object sender, EventArgs e)
        {
            QuadTree t = new QuadTree();

            Bitmap image = new Bitmap(PictureDisplay.Image);
            t.BuildTree(image, ref PictureDisplay);
        }
    }
}