using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace JakeTest
{
	public partial class JakeTestMain : Form
	{
		public JakeTestMain()
		{
			InitializeComponent();
			Init();
		}
		private void Init()
		{
			m_loadedImage = null;
			m_dragging = false;
			m_dragX = 0;
			m_dragY = 0;

			m_displayImageX = 0;
			m_displayImageY = 0;
			m_displayImageDisplayScale = 1.0f;
			m_displayImageWidth = this.pictureBox1.Width;
			m_displayImageHeight = this.pictureBox1.Height;
			m_displayImage = new Bitmap(m_displayImageWidth, m_displayImageHeight);
			m_displayGR = Graphics.FromImage(m_displayImage);
			m_displayGR.InterpolationMode = InterpolationMode.HighQualityBicubic;
			UpdateDisplayImage();

			m_detailImageX = 0;
			m_detailImageY = 0;
			m_detailImageDisplayScale = 2.0f;
			m_detailImageWidth = this.pictureBox2.Width;
			m_detailImageHeight = this.pictureBox2.Height;
			m_detailImage = new Bitmap(m_detailImageWidth, m_detailImageHeight);
			m_detailGR = Graphics.FromImage(m_detailImage);
			m_detailGR.InterpolationMode = InterpolationMode.HighQualityBicubic;
			UpdateDetailImage();

			this.pictureBox1.MouseMove += new MouseEventHandler(Image_MouseMove);
			this.pictureBox1.MouseDown += new MouseEventHandler(Image_MouseDown);
			this.pictureBox1.MouseUp += new MouseEventHandler(Image_MouseUp);
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

    	if(openFileDialog1.ShowDialog() != DialogResult.OK)
				return;

			ImageLoadFile(openFileDialog1.FileName);
			m_loadedImageWidth = m_loadedImage.Size.Width;
			m_loadedImageHeight = m_loadedImage.Size.Height;
		}
		private void ImageLoadFile(string fileName)
		{
			m_loadedImage = new Bitmap(fileName);
			m_displayImageX = 0;
			m_displayImageY = 0;
			m_displayImageDisplayScale = 1.0f;
			UpdateDisplayImage();
			UpdateDetailImage();

			SetStatus(string.Format("Loaded '{0}' {1} x {2}", fileName, m_loadedImageWidth, m_loadedImageHeight));
		}
		private void SetStatus(string status)
		{
			this.textBox1.Text = status;
		}
		private void Image_MouseMove (object sender, MouseEventArgs e)
		{
			int curX = e.Location.X;
			int curY = e.Location.Y;
			if (m_dragging) 
			{
				m_displayImageX -= (curX - m_dragX);
				m_displayImageY -= (curY - m_dragY);
				m_dragX = curX;
				m_dragY = curY;
				UpdateDisplayImage();
			} 
			else 
			{
				m_detailImageX = curX;
				m_detailImageY = curY;
				UpdateDetailImage();
			}
			string text = e.Location.ToString();
			SetStatus(text);
		} 
		private void Image_MouseDown(object sender, MouseEventArgs e)
		{
			m_dragging = true;
			m_dragX = e.Location.X;
			m_dragY = e.Location.Y;
			SetStatus("Dragging Start " + e.Location.ToString());
		}
		private void Image_MouseUp(object sender, MouseEventArgs e)
		{
			m_dragging = false;
			SetStatus("Dragging Stop");
		}
		private void UpdateDisplayImage()
		{
			m_displayGR.Clear(Color.DarkGray);
			int mx = m_displayImageX;
			int my = m_displayImageY;
			int drawW = (int)((float)(m_displayImageWidth) / m_displayImageDisplayScale);
			int drawH = (int)((float)(m_displayImageHeight) / m_displayImageDisplayScale);
			int dx = 0;
			int dy = 0;
			int sx = mx;
			int sy = my;
			if (m_loadedImage != null) 
			{
				m_displayGR.DrawImage(m_loadedImage, 
			                      	new Rectangle(dx, dy, drawW, drawH),
			                      	new Rectangle(sx, sy, drawW, drawH),
			                      	GraphicsUnit.Pixel);
			}
			pictureBox1.Image = m_displayImage;
		}
		private void UpdateDetailImage()
		{
			m_detailGR.Clear(Color.DarkGray);
			int dW = m_detailImageWidth;
			int dH = m_detailImageHeight;
			if (m_loadedImage != null) 
			{
				int mX = m_detailImageX;
				int mY = m_detailImageY;

				int dX = 0;
				int dY = 0;
				Rectangle destRect = new Rectangle(dX, dY, dW, dH);

				int sW = (int)((float)(m_detailImageWidth) / m_detailImageDisplayScale);
				int sH = (int)((float)(m_detailImageHeight) / m_detailImageDisplayScale);
				int sX = mX - (sW / 2);
				int sY = mY - (sH / 2);
				Rectangle srcRect = new Rectangle(sX, sY, sW, sH);

				m_detailGR.DrawImage(m_loadedImage, destRect, srcRect, GraphicsUnit.Pixel);
			}

			int halfW = dW / 2;
			int halfH = dH / 2;
			int cursorOffset = 3;
			int cursorLen = 10;
			Pen cursorColour = Pens.DarkRed;
    	m_detailGR.DrawLine(cursorColour, halfW, halfH - cursorOffset, halfW, halfH - cursorOffset - cursorLen);
    	m_detailGR.DrawLine(cursorColour, halfW, halfH + cursorOffset, halfW, halfH + cursorOffset + cursorLen);
    	m_detailGR.DrawLine(cursorColour, halfW - cursorOffset, halfH, halfW - cursorOffset - cursorLen, halfH);
    	m_detailGR.DrawLine(cursorColour, halfW + cursorOffset, halfH, halfW + cursorOffset + cursorLen, halfH);

			pictureBox2.Image = m_detailImage;
		}
		private Bitmap m_loadedImage;
		private int m_loadedImageWidth;
		private int m_loadedImageHeight;

		private Bitmap m_displayImage;
		private Graphics m_displayGR;
		private int m_displayImageWidth;
		private int m_displayImageHeight;
		private float m_displayImageDisplayScale;
		private int m_displayImageX;
		private int m_displayImageY;

		private Bitmap m_detailImage;
		private Graphics m_detailGR;
		private int m_detailImageWidth;
		private int m_detailImageHeight;
		private float m_detailImageDisplayScale;
		private int m_detailImageX;
		private int m_detailImageY;

		private bool m_dragging;
		private int m_dragX;
		private int m_dragY;
	}
}
