namespace Quadtree_Image_Compression
{
    partial class ImageCompressionForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageCompressionForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.PictureDisplay = new System.Windows.Forms.PictureBox();
            this.LoadButton = new System.Windows.Forms.Button();
            this.CompressButton = new System.Windows.Forms.Button();
            this.Bar = new System.Windows.Forms.Panel();
            this.Title = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.MenuOptions = new System.Windows.Forms.Panel();
            this.SaveImageButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CompressionTime = new System.Windows.Forms.Label();
            this.LoadingBar = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.CompressionRateSlider = new System.Windows.Forms.TrackBar();
            this.CompressionRate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DetailLevel = new System.Windows.Forms.TextBox();
            this.DetailLevelSlider = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureDisplay)).BeginInit();
            this.Bar.SuspendLayout();
            this.MenuOptions.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CompressionRateSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailLevelSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(782, 473);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(324, 479);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(324, 562);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 29);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // PictureDisplay
            // 
            this.PictureDisplay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PictureDisplay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PictureDisplay.Location = new System.Drawing.Point(275, 150);
            this.PictureDisplay.Name = "PictureDisplay";
            this.PictureDisplay.Size = new System.Drawing.Size(750, 500);
            this.PictureDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureDisplay.TabIndex = 0;
            this.PictureDisplay.TabStop = false;
            // 
            // LoadButton
            // 
            this.LoadButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LoadButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(54)))), ((int)(((byte)(59)))));
            this.LoadButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(132)))), ((int)(((byte)(124)))));
            this.LoadButton.FlatAppearance.BorderSize = 2;
            this.LoadButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.LoadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LoadButton.ForeColor = System.Drawing.SystemColors.Control;
            this.LoadButton.Location = new System.Drawing.Point(20, 234);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(210, 40);
            this.LoadButton.TabIndex = 1;
            this.LoadButton.Text = "Load Image";
            this.LoadButton.UseVisualStyleBackColor = false;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // CompressButton
            // 
            this.CompressButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CompressButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(54)))), ((int)(((byte)(59)))));
            this.CompressButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(132)))), ((int)(((byte)(124)))));
            this.CompressButton.FlatAppearance.BorderSize = 2;
            this.CompressButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CompressButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CompressButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CompressButton.ForeColor = System.Drawing.SystemColors.Control;
            this.CompressButton.Location = new System.Drawing.Point(20, 318);
            this.CompressButton.Name = "CompressButton";
            this.CompressButton.Size = new System.Drawing.Size(210, 40);
            this.CompressButton.TabIndex = 2;
            this.CompressButton.Text = "Compress Image";
            this.CompressButton.UseVisualStyleBackColor = false;
            this.CompressButton.Click += new System.EventHandler(this.CompressButton_Click);
            // 
            // Bar
            // 
            this.Bar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(54)))), ((int)(((byte)(59)))));
            this.Bar.Controls.Add(this.Title);
            this.Bar.Controls.Add(this.CloseButton);
            this.Bar.Dock = System.Windows.Forms.DockStyle.Top;
            this.Bar.Location = new System.Drawing.Point(0, 0);
            this.Bar.Name = "Bar";
            this.Bar.Size = new System.Drawing.Size(1050, 74);
            this.Bar.TabIndex = 5;
            this.Bar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownEvent);
            this.Bar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveEvent);
            this.Bar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUpEvent);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(369, 7);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(424, 59);
            this.Title.TabIndex = 1;
            this.Title.Text = "Image Compression";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(74)))), ((int)(((byte)(95)))));
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CloseButton.ForeColor = System.Drawing.SystemColors.Control;
            this.CloseButton.Location = new System.Drawing.Point(1013, 0);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(37, 37);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "X";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // MenuOptions
            // 
            this.MenuOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(54)))), ((int)(((byte)(59)))));
            this.MenuOptions.Controls.Add(this.SaveImageButton);
            this.MenuOptions.Controls.Add(this.LoadButton);
            this.MenuOptions.Controls.Add(this.CompressButton);
            this.MenuOptions.Dock = System.Windows.Forms.DockStyle.Left;
            this.MenuOptions.Location = new System.Drawing.Point(0, 74);
            this.MenuOptions.Name = "MenuOptions";
            this.MenuOptions.Size = new System.Drawing.Size(250, 676);
            this.MenuOptions.TabIndex = 6;
            // 
            // SaveImageButton
            // 
            this.SaveImageButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SaveImageButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(54)))), ((int)(((byte)(59)))));
            this.SaveImageButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(132)))), ((int)(((byte)(124)))));
            this.SaveImageButton.FlatAppearance.BorderSize = 2;
            this.SaveImageButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SaveImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveImageButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SaveImageButton.ForeColor = System.Drawing.SystemColors.Control;
            this.SaveImageButton.Location = new System.Drawing.Point(20, 402);
            this.SaveImageButton.Name = "SaveImageButton";
            this.SaveImageButton.Size = new System.Drawing.Size(210, 40);
            this.SaveImageButton.TabIndex = 3;
            this.SaveImageButton.Text = "Save Image";
            this.SaveImageButton.UseVisualStyleBackColor = false;
            this.SaveImageButton.Click += new System.EventHandler(this.SaveImageButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CompressionTime);
            this.panel1.Controls.Add(this.LoadingBar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(250, 653);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 97);
            this.panel1.TabIndex = 7;
            // 
            // CompressionTime
            // 
            this.CompressionTime.AutoSize = true;
            this.CompressionTime.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CompressionTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(54)))), ((int)(((byte)(59)))));
            this.CompressionTime.Location = new System.Drawing.Point(324, 0);
            this.CompressionTime.Name = "CompressionTime";
            this.CompressionTime.Size = new System.Drawing.Size(151, 31);
            this.CompressionTime.TabIndex = 6;
            this.CompressionTime.Text = "Elapsed time:";
            // 
            // LoadingBar
            // 
            this.LoadingBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LoadingBar.Location = new System.Drawing.Point(25, 3);
            this.LoadingBar.Name = "LoadingBar";
            this.LoadingBar.Size = new System.Drawing.Size(750, 29);
            this.LoadingBar.TabIndex = 5;
            this.LoadingBar.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // CompressionRateSlider
            // 
            this.CompressionRateSlider.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CompressionRateSlider.Location = new System.Drawing.Point(278, 92);
            this.CompressionRateSlider.Name = "CompressionRateSlider";
            this.CompressionRateSlider.Size = new System.Drawing.Size(205, 56);
            this.CompressionRateSlider.TabIndex = 8;
            this.CompressionRateSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.CompressionRateSlider.Scroll += new System.EventHandler(this.CompressionRateSlider_Scroll);
            // 
            // CompressionRate
            // 
            this.CompressionRate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CompressionRate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CompressionRate.Location = new System.Drawing.Point(489, 92);
            this.CompressionRate.MaxLength = 2;
            this.CompressionRate.Name = "CompressionRate";
            this.CompressionRate.Size = new System.Drawing.Size(42, 27);
            this.CompressionRate.TabIndex = 9;
            this.CompressionRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CompressionRate.TextChanged += new System.EventHandler(this.CompressionRate_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(54)))), ((int)(((byte)(59)))));
            this.label1.Location = new System.Drawing.Point(285, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 28);
            this.label1.TabIndex = 10;
            this.label1.Text = "Compression Rate";
            // 
            // DetailLevel
            // 
            this.DetailLevel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DetailLevel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DetailLevel.Location = new System.Drawing.Point(983, 92);
            this.DetailLevel.MaxLength = 2;
            this.DetailLevel.Name = "DetailLevel";
            this.DetailLevel.Size = new System.Drawing.Size(42, 27);
            this.DetailLevel.TabIndex = 11;
            this.DetailLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DetailLevel.TextChanged += new System.EventHandler(this.DetailLevel_TextChanged);
            // 
            // DetailLevelSlider
            // 
            this.DetailLevelSlider.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DetailLevelSlider.Location = new System.Drawing.Point(772, 92);
            this.DetailLevelSlider.Name = "DetailLevelSlider";
            this.DetailLevelSlider.Size = new System.Drawing.Size(205, 56);
            this.DetailLevelSlider.TabIndex = 12;
            this.DetailLevelSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.DetailLevelSlider.Scroll += new System.EventHandler(this.DetailLevelSlider_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(54)))), ((int)(((byte)(59)))));
            this.label2.Location = new System.Drawing.Point(778, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 28);
            this.label2.TabIndex = 13;
            this.label2.Text = "Detail level";
            // 
            // ImageCompressionForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(206)))), ((int)(((byte)(168)))));
            this.ClientSize = new System.Drawing.Size(1050, 750);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DetailLevelSlider);
            this.Controls.Add(this.DetailLevel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CompressionRate);
            this.Controls.Add(this.CompressionRateSlider);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MenuOptions);
            this.Controls.Add(this.Bar);
            this.Controls.Add(this.PictureDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImageCompressionForm";
            this.Text = "Image Compression";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureDisplay)).EndInit();
            this.Bar.ResumeLayout(false);
            this.Bar.PerformLayout();
            this.MenuOptions.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CompressionRateSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailLevelSlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBox1;
        private Button button1;
        private Button button2;
        private PictureBox PictureDisplay;
        private Button LoadButton;
        private Button CompressButton;
        private Panel Bar;
        private Button CloseButton;
        private Panel MenuOptions;
        private Label Title;
        private Button SaveImageButton;
        private Panel panel1;
        private ProgressBar LoadingBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label CompressionTime;
        private TrackBar CompressionRateSlider;
        private TextBox CompressionRate;
        private Label label1;
        private TextBox DetailLevel;
        private TrackBar DetailLevelSlider;
        private Label label2;
    }
}