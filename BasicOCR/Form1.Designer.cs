namespace BasicOCR
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.convertButton = new System.Windows.Forms.Button();
            this.ofDialog = new System.Windows.Forms.OpenFileDialog();
            this.minimizeBox = new System.Windows.Forms.PictureBox();
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.closeBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.minimizeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // resultTextBox
            // 
            this.resultTextBox.Location = new System.Drawing.Point(230, 40);
            this.resultTextBox.Multiline = true;
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.Size = new System.Drawing.Size(200, 200);
            this.resultTextBox.TabIndex = 1;
            // 
            // convertButton
            // 
            this.convertButton.Location = new System.Drawing.Point(185, 260);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(80, 25);
            this.convertButton.TabIndex = 2;
            this.convertButton.Text = "Convert";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            // 
            // ofDialog
            // 
            this.ofDialog.FileName = "openFileDialog1";
            // 
            // minimizeBox
            // 
            this.minimizeBox.Image = ((System.Drawing.Image)(resources.GetObject("minimizeBox.Image")));
            this.minimizeBox.Location = new System.Drawing.Point(400, 10);
            this.minimizeBox.Name = "minimizeBox";
            this.minimizeBox.Size = new System.Drawing.Size(20, 20);
            this.minimizeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.minimizeBox.TabIndex = 3;
            this.minimizeBox.TabStop = false;
            this.minimizeBox.Click += new System.EventHandler(this.minimizeBox_Click);
            // 
            // imageBox
            // 
            this.imageBox.BackgroundImage = global::BasicOCR.Properties.Resources.placeholder_image;
            this.imageBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imageBox.InitialImage = global::BasicOCR.Properties.Resources.placeholder_image;
            this.imageBox.Location = new System.Drawing.Point(20, 40);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(200, 200);
            this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox.TabIndex = 0;
            this.imageBox.TabStop = false;
            this.imageBox.Click += new System.EventHandler(this.imageBox_Click);
            // 
            // closeBox
            // 
            this.closeBox.Image = global::BasicOCR.Properties.Resources.close;
            this.closeBox.Location = new System.Drawing.Point(425, 10);
            this.closeBox.Name = "closeBox";
            this.closeBox.Size = new System.Drawing.Size(20, 20);
            this.closeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.closeBox.TabIndex = 4;
            this.closeBox.TabStop = false;
            this.closeBox.Click += new System.EventHandler(this.closeBox_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 300);
            this.Controls.Add(this.closeBox);
            this.Controls.Add(this.minimizeBox);
            this.Controls.Add(this.convertButton);
            this.Controls.Add(this.resultTextBox);
            this.Controls.Add(this.imageBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Basic OCR";
            ((System.ComponentModel.ISupportInitialize)(this.minimizeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imageBox;
        private System.Windows.Forms.TextBox resultTextBox;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.OpenFileDialog ofDialog;
        private System.Windows.Forms.PictureBox minimizeBox;
        private System.Windows.Forms.PictureBox closeBox;
    }
}

