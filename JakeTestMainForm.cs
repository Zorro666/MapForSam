using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JakeTest
{
	public partial class JakeTestMain : Form
	{
		public JakeTestMain()
		{
			InitializeComponent();
			InitMe();
		}
		private void InitMe ()
		{
			Bitmap flag = new Bitmap(200, 100);
			Graphics flagGraphics = Graphics.FromImage(flag);
			int red = 0;
			int white = 11;
			while (white <= 100) 
			{
				flagGraphics.FillRectangle(Brushes.Red, 0, red, 200, 10);
				flagGraphics.FillRectangle(Brushes.White, 0, white, 200, 10);
				red += 20;
				white += 20;
			}
			this.pictureBox1.Image = flag;
			this.button1.Click += new EventHandler(Quit_Click);
			this.button2.Click += new EventHandler(LoadFile_Click);
		}
		private void Quit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		private void LoadFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

			openFileDialog1.Filter = "Image Files(*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
    		openFileDialog1.FilterIndex = 1 ;
    		openFileDialog1.RestoreDirectory = true ;

    		if(openFileDialog1.ShowDialog() == DialogResult.OK)
					ImageLoadFile(openFileDialog1.FileName);
		}
		private void ImageLoadFile(string fileName)
		{
			m_bitmap = new Bitmap(fileName);
			this.pictureBox1.Image = m_bitmap;

			SetStatus(string.Format("Loaded '{0}' {1} x {2}", fileName, m_bitmap.Size.Width, m_bitmap.Size.Height));
		}
		private void SetStatus(string status)
		{
			this.textBox1.Text = status;
		}
		private Bitmap m_bitmap;
	}
}
