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
			m_displayImageWidth = this.picturebox_DisplayImage.Width;
			m_displayImageHeight = this.picturebox_DisplayImage.Height;
			m_displayImage = new Bitmap(m_displayImageWidth, m_displayImageHeight);
			m_displayGR = Graphics.FromImage(m_displayImage);
			m_displayGR.InterpolationMode = InterpolationMode.HighQualityBicubic;
			UpdateDisplayImage();

			m_detailImageX = 0;
			m_detailImageY = 0;
			m_detailImageDisplayScale = 2.0f;
			m_detailImageWidth = this.pictureBox_DetailImage.Width;
			m_detailImageHeight = this.pictureBox_DetailImage.Height;
			m_detailImage = new Bitmap(m_detailImageWidth, m_detailImageHeight);
			m_detailGR = Graphics.FromImage(m_detailImage);
			m_detailGR.InterpolationMode = InterpolationMode.HighQualityBicubic;
			UpdateDetailImage();

			this.picturebox_DisplayImage.MouseMove += new MouseEventHandler(DisplayImage_MouseMove);
			this.picturebox_DisplayImage.MouseDown += new MouseEventHandler(DisplayImage_MouseDown);
			this.picturebox_DisplayImage.MouseUp += new MouseEventHandler(DisplayImage_MouseUp);
			this.picturebox_DisplayImage.MouseDoubleClick += new MouseEventHandler(DisplayImage_MouseDoubleClick);
			this.button_Quit.Click += new EventHandler(Quit_Click);
			this.button_LoadImage.Click += new EventHandler(LoadFile_Click);
			this.scroll_DetailImageScale.ValueChanged += new EventHandler(DetailImageScale_Changed);

			this.DoubleBuffered = true;
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

			SetStatusText(string.Format("Loaded '{0}' {1} x {2}", fileName, m_loadedImageWidth, m_loadedImageHeight));
		}
		private void SetStatusText(string status)
		{
			this.text_Status.Text = status;
		}
		private void SetImageXYText()
		{
			this.text_ImageX.Text = m_sourceImagePixelX.ToString();
			this.text_ImageY.Text = m_sourceImagePixelY.ToString();
		}
		private void DisplayImage_MouseMove (object sender, MouseEventArgs e)
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
				SetImageXYText();
			}
			string text = e.Location.ToString();
			SetStatusText(text);
		} 
		private void DisplayImage_MouseDown(object sender, MouseEventArgs e)
		{
			m_dragging = true;
			m_dragX = e.Location.X;
			m_dragY = e.Location.Y;
			this.Cursor = Cursors.Cross;
			SetStatusText("Dragging Start " + e.Location.ToString());
		}
		private void DisplayImage_MouseUp(object sender, MouseEventArgs e)
		{
			m_dragging = false;
			this.Cursor = Cursors.Default;

			int curX = e.Location.X;
			int curY = e.Location.Y;

			m_dragX = curX;
			m_dragY = curY;
			UpdateDisplayImage();

			m_detailImageX = curX;
			m_detailImageY = curY;
			UpdateDetailImage();

			SetStatusText("Dragging Stop");
		}
		private void DisplayImage_MouseDoubleClick (object sender, MouseEventArgs e)
		{
			float displayScale = m_displayImageDisplayScale;
			SetStatusText("Double-click:" + e.Button);
			float zoomAmount = 0.0f;
			if (e.Button == MouseButtons.Left)
			{
				zoomAmount = 1.0f;
			}
			else if (e.Button == MouseButtons.Right)
			{
				zoomAmount = -1.0f;
			}
			if (Control.ModifierKeys == Keys.Shift)
			{
				zoomAmount *= -1.0f;
			}
			displayScale += zoomAmount;
			displayScale = Math.Max(displayScale, 1.0f);
			displayScale = Math.Min(displayScale, 10.0f);
			if (displayScale != m_displayImageDisplayScale)
			{
				ComputeImageXY();
				int oldX = m_sourceImagePixelX;
				int oldY = m_sourceImagePixelY;
				m_displayImageDisplayScale = displayScale;
				ComputeImageXY();
				m_displayImageX -= (int)((float)(m_sourceImagePixelX - oldX) * m_displayImageDisplayScale);
				m_displayImageY -= (int)((float)(m_sourceImagePixelY - oldY) * m_displayImageDisplayScale);
				SetStatusText("ImageDisplayScale:" + m_displayImageDisplayScale);
				ComputeImageXY();

				int newX = m_sourceImagePixelX;
				int newY = m_sourceImagePixelY;
				if (newX != oldX)
					MessageBox.Show(string.Format("newX != oldX {0} != {1}", newX, oldX));
				if (newY != oldY)
					MessageBox.Show(string.Format("newY != oldY {0} != {1}", newY, oldY));
			}
		}
		private void DetailImageScale_Changed(object sender, EventArgs e)
		{
			int value = this.scroll_DetailImageScale.Value;
			m_detailImageDisplayScale = (float)(value);
			UpdateDetailImage();
			SetStatusText("ImageScale:" + value);
		}
		private void UpdateDisplayImage()
		{
			m_displayGR.Clear(Color.DarkGray);
			if (m_loadedImage != null) 
			{
				int mX = m_displayImageX;
				int mY = m_displayImageY;

				int dX = 0;
				int dY = 0;
				int dW = m_displayImageWidth;
				int dH = m_displayImageHeight;
				Rectangle destRect = new Rectangle(dX, dY, dW, dH);

				int sW = (int)((float)(m_displayImageWidth) / m_displayImageDisplayScale);
				int sH = (int)((float)(m_displayImageHeight) / m_displayImageDisplayScale);
				int sX = (int)((float)(mX) / m_displayImageDisplayScale);
				int sY = (int)((float)(mY) / m_displayImageDisplayScale);
				Rectangle srcRect = new Rectangle(sX, sY, sW, sH);

				m_displayGR.DrawImage(m_loadedImage, destRect, srcRect, GraphicsUnit.Pixel);
			}
			picturebox_DisplayImage.Image = m_displayImage;
		}
		private void ComputeImageXY()
		{
			int mX = m_detailImageX + m_displayImageX;
			int mY = m_detailImageY + m_displayImageY;
			mX = (int)((float)(mX) / m_displayImageDisplayScale);
			mY = (int)((float)(mY) / m_displayImageDisplayScale);
			m_sourceImagePixelX = mX;
			m_sourceImagePixelY = mY;
		}
		private void UpdateDetailImage()
		{
			ComputeImageXY();

			m_detailGR.Clear(Color.DarkGray);
			int dW = m_detailImageWidth;
			int dH = m_detailImageHeight;
			if (m_loadedImage != null) 
			{
				int dX = 0;
				int dY = 0;
				Rectangle destRect = new Rectangle(dX, dY, dW, dH);

				int sW = (int)((float)(m_detailImageWidth) / m_detailImageDisplayScale);
				int sH = (int)((float)(m_detailImageHeight) / m_detailImageDisplayScale);
				int sX = m_sourceImagePixelX - (sW / 2);
				int sY = m_sourceImagePixelY - (sH / 2);
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

			pictureBox_DetailImage.Image = m_detailImage;
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
		private int m_sourceImagePixelX;
		private int	m_sourceImagePixelY;

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