using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using System.Drawing.Imaging;

using System.Net;
using System.Collections.Specialized;
using System.IO;


namespace CameraCapture
{
    public partial class Authentication : Form
    {
        private VideoCapture capture;
        

        private bool pictureinprogress;

        private void processfram(object sender, EventArgs args)
        {
            Image<Bgr, byte> Imageframe = capture.QueryFrame().ToImage<Bgr, byte>();

            imageBox1.Image = Imageframe;

        }

        public Authentication()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (capture == null)
            {
                try
                {
                    capture = new VideoCapture();

                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            if (capture != null)
            {
                if (pictureinprogress)
                {
                   
                    Application.Idle -= processfram;
                }
                else
                {
                   
                    Application.Idle += processfram;

                }
                pictureinprogress = !pictureinprogress;
            }

        }

     
        private void ReleaseData()
        {
            if (capture != null)
            {
                capture.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            imageBox2.Image = imageBox1.Image;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "JPG(*.JPG|*.jpg";
            if (save.ShowDialog() == DialogResult.OK)
            {
                int width = Convert.ToInt32(imageBox2.Width);
                int height = Convert.ToInt32(imageBox2.Height);
                Bitmap bit = new Bitmap(width, height);
                imageBox2.DrawToBitmap(bit, new Rectangle(0, 0, Width, Height));
                bit.Save(save.FileName, ImageFormat.Jpeg);

            }
        }
    }
}