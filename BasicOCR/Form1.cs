using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicOCR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void selectImage()
        {
            if (ofDialog.ShowDialog() == DialogResult.OK)
            {
                var image = Image.FromFile(ofDialog.FileName);
                if (image == null)
                {
                    throw new Exception("Not a image");
                }
                imageBox.Image = image;

            } else
            {
                imageBox.Image = null;
            }
        }

        private void doOCR()
        {
            if (imageBox.Image != null)
            {
                var engine = new OCREngine();
                resultTextBox.Text = engine.Recognize(imageBox.Image);
            } else
            {
                throw new Exception("Image not selected");
            }
        }

        private void imageBox_Click(object sender, EventArgs e)
        {
            try
            {
                selectImage();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            try
            {
                doOCR();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void closeBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void minimizeBox_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
