using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace SamMapTool
{
	public partial class SamMapToolMain : Form
	{
		public SamMapToolMain()
		{
			InitializeComponent();
			Init();
		}
		private void Init()
		{
			m_displayPoints = true;
			SetDrawPointsButtonState();

			m_now = DateTime.Now;
    	m_clickTimer = new Timer();
			m_clickTimer.Interval = 10;
			m_clickTimer.Tick += new EventHandler(ClickTimer_Tick);
			m_points = new List<EastingNorthingPoint>();
			m_clickMouseEventArgs = new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0);
			m_downMouseEventArgs = new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0);

			m_loadedImage = null;
			m_dragging = false;
			m_dragX = 0;
			m_dragY = 0;

			m_eastingZero = 0;
			m_northingZero = 0;
			m_eastingScale = 1;
			m_northingScale = 1;

			m_displayImageX = 0;
			m_displayImageY = 0;
			m_displayImageDisplayScale = 1.0f;
			m_displayImageWidth = this.picturebox_DisplayImage.Width;
			m_displayImageHeight = this.picturebox_DisplayImage.Height;
			m_displayImage = new Bitmap(m_displayImageWidth, m_displayImageHeight);
			m_displayGR = Graphics.FromImage(m_displayImage);
			m_displayGR.InterpolationMode = InterpolationMode.HighQualityBicubic;
			RefreshDisplayImage();

			m_detailImageTrack = true;
			SetDetailImageTrackButtonState();

			m_detailImageX = 0;
			m_detailImageY = 0;
			m_detailImageDisplayScale = 2.0f;
			m_detailImageWidth = this.pictureBox_DetailImage.Width;
			m_detailImageHeight = this.pictureBox_DetailImage.Height;
			m_detailImage = new Bitmap(m_detailImageWidth, m_detailImageHeight);
			m_detailGR = Graphics.FromImage(m_detailImage);
			m_detailGR.InterpolationMode = InterpolationMode.HighQualityBicubic;
			RefreshDetailImage();

			this.picturebox_DisplayImage.MouseMove += new MouseEventHandler(DisplayImage_MouseMove);
			this.picturebox_DisplayImage.MouseDown += new MouseEventHandler(DisplayImage_MouseDown);
			this.picturebox_DisplayImage.MouseUp += new MouseEventHandler(DisplayImage_MouseUp);
			this.picturebox_DisplayImage.MouseClick += new MouseEventHandler(DisplayImage_MouseClick);
			this.picturebox_DisplayImage.MouseDoubleClick += new MouseEventHandler(DisplayImage_MouseDoubleClick);

			this.KeyPress += new KeyPressEventHandler(this_KeyPress);

			this.DoubleBuffered = true;
		}
		private void button_Quit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		private void button_LoadImage_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

			openFileDialog1.Filter = "Image Files(*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
    	openFileDialog1.FilterIndex = 1 ;
    	openFileDialog1.RestoreDirectory = true ;

    	if(openFileDialog1.ShowDialog(this) != DialogResult.OK)
				return;

			ImageLoadFile(openFileDialog1.FileName);
			m_loadedImageWidth = m_loadedImage.Size.Width;
			m_loadedImageHeight = m_loadedImage.Size.Height;
		}
		private void button_DrawPoints_Click(object sender, EventArgs e)
		{
			ToggleDrawPoints();
		}
		private void button_DetailImageTrack_Click(object sender, EventArgs e)
		{
			ToggleDetailImageTrack();
		}
		private void button_EnterEastingNorthing_Click(object sender, EventArgs e)
		{
			int newEasting = Convert.ToInt32(this.text_Easting.Text);
			int newNorthing = Convert.ToInt32(this.text_Northing.Text);
			int pixelX = m_pointPixelX;
			int pixelY = m_pointPixelY;
			AddNewEastingNorthing(newEasting, newNorthing, pixelX, pixelY);
			SetStatusText(string.Format("Easting Northing Dialog: Added {0}, {1}", newEasting, newNorthing));
			m_detailImageTrack = true;
			SetDetailImageTrackButtonState();
		}
		private void ImageLoadFile(string fileName)
		{
			m_loadedImage = new Bitmap(fileName);
			m_displayImageX = 0;
			m_displayImageY = 0;
			m_displayImageDisplayScale = 1.0f;
			RefreshImages();

			SetStatusText(string.Format("Loaded '{0}' {1} x {2}", fileName, m_loadedImageWidth, m_loadedImageHeight));
		}
		private void RefreshImages()
		{
			RefreshDisplayImage();
			RefreshDetailImage();
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
		private void ComputeEastingNorthing()
		{
			float eastingPixel = m_sourceImagePixelX;
			float northingPixel = m_sourceImagePixelY;
			float easting = m_eastingZero + eastingPixel * m_eastingScale;
			float northing = m_northingZero + northingPixel * m_northingScale;
			m_easting = (int)Math.Round(easting);
			m_northing = (int)Math.Round(northing);
		}
		private void ComputeBestFitEastingNorthing()
		{
			int n = m_points.Count;
			if (n == 1)
			{
				SetStatusText("Need more than one Easting, Northing setting");
				return;
			}

			Vector2 sumX = new Vector2(0, 0);
			Vector2 sumY = new Vector2(0, 0);
			Vector2 sumXY = new Vector2(0, 0);
			Vector2 sumXX = new Vector2(0, 0);

			for (int i = 0; i < n; i++)
			{
				EastingNorthingPoint point = m_points[i];
				Vector2 pixel = point.Pixel;
				Vector2 eastingNorthing = point.EastingNorthing;

				// X = pixel
				// Y = eastingNorthing
				sumX.Add(pixel.X, pixel.Y);
				sumY.Add(eastingNorthing.X, eastingNorthing.Y);

				sumXY.Add(pixel.X * eastingNorthing.X, pixel.Y * eastingNorthing.Y);
				sumXX.Add(pixel.X * pixel.X, pixel.Y * pixel.Y);
			}

			// y = A * x + B
			// A = ( n*sum(x*y) - (sum(x)*sum(y))) / (n*sum(x^2) - (sum(x)*sum(x))
			// B = sum(x^2)*sum(y) - sum(x)*sum(x*y) / (n*sum(x^2) - (sum(x)*sum(x))

			// A = (sum(x*y) - (sum(x)*sum(y))/n) / (sum(x^2) - sum(x)*sum(x)/n)
			// B = sum(y)/n - A*(sum(x)/n)

			float denom;

			denom = (sumXX.X - (sumX.X * sumX.X)/n);
			m_eastingScale = (sumXY.X - (sumX.X * sumY.X)/n) / denom;
			m_eastingZero = ((sumY.X - m_eastingScale * sumX.X)) / n;

			denom = (sumXX.Y - (sumX.Y * sumX.Y)/n);
			m_northingScale = (sumXY.Y - (sumX.Y * sumY.Y)/n) / denom;
			m_northingZero = ((sumY.Y - m_northingScale * sumX.Y)) / n;
		}
		private void AddNewEastingNorthing(int newEasting, int newNorthing, int pixelX, int pixelY)
		{
			EastingNorthingPoint newPoint = new EastingNorthingPoint(newEasting, newNorthing, pixelX, pixelY);
			bool found = false;
			foreach (EastingNorthingPoint point in m_points)
			{
				if (point.PixelSame(newPoint))
				{
					point.EastingNorthing.X = newEasting;
					point.EastingNorthing.Y = newNorthing;
					found = true;
					MessageBox.Show("Found it");
					break;
				}
			}
			if (found == false)
			{
				m_points.Add(newPoint);
			}

			ComputeBestFitEastingNorthing();
			RefreshImages();
		}

		private void SetEastingNorthingText()
		{
			ComputeEastingNorthing();
			this.text_Easting.Text = m_easting.ToString();
			this.text_Northing.Text = m_northing.ToString();
		}
		private void UpdateDisplayImage(int x, int y)
		{
			m_dragX = x;
			m_dragY = y;
			RefreshDisplayImage();
		}
		private void UpdateDetailImage(int x, int y)
		{
			if (m_detailImageTrack)
			{
				m_detailImageX = x;
				m_detailImageY = y;
				RefreshDetailImage();
				SetImageXYText();
				SetEastingNorthingText();
			}
		}
		private void DisplayImage_MouseMove(object sender, MouseEventArgs e)
		{
			int curX = e.Location.X;
			int curY = e.Location.Y;
			if (m_dragging) 
			{
				m_displayImageX -= (curX - m_dragX);
				m_displayImageY -= (curY - m_dragY);
				UpdateDisplayImage(curX, curY);
			} 
			else if (m_detailImageTrack)
			{
				UpdateDetailImage(curX, curY);
			}
			bool debug = false;
			if (debug)
			{
				string text = e.Location.ToString();
				SetStatusText(text);
			}
		} 
		private void DisplayImage_SingleClick(MouseEventArgs e)
		{
			if (e.Button == MOUSE_BUTTON_DETAIL_LOCK_TOGGLE)
			{
				ToggleDetailImageTrack();
			}
			if (e.Button == MOUSE_BUTTON_ENTER_EASTING_NORTHING)
			{
				EnterEastingNorthing(false);
			}
			if ((e.Button == MOUSE_BUTTON_ENTER_EASTING_NORTHING) && (Control.ModifierKeys == Keys.Control))
			{
				EnterEastingNorthing(true);
			}
		}
		private void DisplayImage_DoubleClick(MouseEventArgs e)
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
			bool invert = false;
			if (displayScale < 1.0f)
			{
				invert = true;
				displayScale = 1.0f / displayScale;
				zoomAmount *= -1.0f;
			}
			displayScale += zoomAmount;
			if (displayScale == 0.0f)
			{
				if (invert)
				{
					displayScale = 1.0f;
				}
				else
				{
					invert = true;
					displayScale = 2.0f;
				}
			}
			displayScale = Math.Max(displayScale, 1.0f);
			displayScale = Math.Min(displayScale, 10.0f);
			if (invert)
			{
				displayScale = 1.0f/displayScale;
			}
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

/*
				int newX = m_sourceImagePixelX;
				int newY = m_sourceImagePixelY;
				if (newX != oldX)
					MessageBox.Show(string.Format("newX != oldX {0} != {1}", newX, oldX));
				if (newY != oldY)
					MessageBox.Show(string.Format("newY != oldY {0} != {1}", newY, oldY));
*/
				RefreshDisplayImage();
			}
		}
		private void ClickTimer_Tick(object sender, EventArgs e)
		{
			DateTime now = DateTime.Now;
			m_clickTimerMS += (now - m_now).Milliseconds;
			m_now = now;

			if (m_clickTimerMS >= SystemInformation.DoubleClickTime)
			{
				if (m_numClicks == 1)
				{
					DisplayImage_SingleClick(m_clickMouseEventArgs);
				}
				m_clickTimer.Stop();
				m_clickTimerMS = 0;
				m_numClicks = 0;
			}
		}
		private void DisplayImage_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MOUSE_BUTTON_DRAG)
			{
				m_dragging = true;
				m_dragX = e.Location.X;
				m_dragY = e.Location.Y;
				this.Cursor = Cursors.Cross;
				SetStatusText("Dragging Start " + e.Location.ToString());
			}
			m_downMouseEventArgs = e;
		}
		private void DisplayImage_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MOUSE_BUTTON_DRAG)
			{
				m_dragging = false;
				this.Cursor = Cursors.Default;

				int curX = e.Location.X;
				int curY = e.Location.Y;

				UpdateDisplayImage(curX, curY);
				UpdateDetailImage(curX, curY);

				SetStatusText("Dragging Stop");
			}
		}
		private void DisplayImage_MouseClick(object sender, MouseEventArgs e)
		{
			if (m_numClicks == 0)
			{
				bool validClick = true;
				if (m_downMouseEventArgs.Button != e.Button)
				{
					validClick = false;
				}
				if (m_downMouseEventArgs.Location != e.Location)
				{
					validClick = false;
				}
				if (validClick)
				{
					m_clickTimer.Start();
					m_numClicks = 1;
					m_now = DateTime.Now;
					m_clickTimerMS = 0;
					m_clickMouseEventArgs = e;
				}
			}
			else
			{
				m_numClicks += 1;
			}
		}
		private void DisplayImage_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			m_clickTimer.Stop();
			m_clickTimerMS = 0;
			m_numClicks = 0;
			DisplayImage_DoubleClick(e);
		}
		private void EnterEastingNorthing(bool useDialogBox)
		{
			if (useDialogBox)
			{
				EastingNorthingDialog eastingNorthingDialog = new EastingNorthingDialog(m_easting, m_northing);
				DialogResult result = eastingNorthingDialog.ShowDialog(this);
				if (result == DialogResult.OK)
				{
					if (eastingNorthingDialog.OK)
					{
						int newEasting = eastingNorthingDialog.Easting;
						int newNorthing = eastingNorthingDialog.Northing;
						int pixelX = m_sourceImagePixelX;
						int pixelY = m_sourceImagePixelY;
						AddNewEastingNorthing(newEasting, newNorthing, pixelX, pixelY);
						SetStatusText(string.Format("Easting Northing Dialog: Added {0}, {1}", newEasting, newNorthing));
					}
					else
					{
						SetStatusText("Easting Northing Dialog: Invalid Values");
					}
				}
				else
				{
					SetStatusText("Easting Northing Dialog: Cancelled");
				}
			}
			else
			{  
				m_detailImageTrack = false;
				m_pointPixelX = m_sourceImagePixelX;
				m_pointPixelY = m_sourceImagePixelY;
				SetDetailImageTrackButtonState();
				SetStatusText("Enter Easting and Northing the click 'Enter Easting Northing' button");
			}
		}
		private void this_KeyPress(object sender, KeyPressEventArgs k)
		{
			Rectangle screenRect = this.picturebox_DisplayImage.RectangleToScreen(this.picturebox_DisplayImage.ClientRectangle);
			if (screenRect.Contains(Control.MousePosition))
			{
				if (k.KeyChar == 'p')
				{
					ToggleDrawPoints();
				}
				if (k.KeyChar == 'l')
				{
					ToggleDetailImageTrack();
				}
			}
		}
		private void ToggleDrawPoints()
		{
			m_displayPoints ^= true;
			SetDrawPointsButtonState();
			RefreshImages();
			SetStatusText(string.Format("DisplayPoints:{0}", m_displayPoints));
		}
		private void SetDrawPointsButtonState()
		{
			if (m_displayPoints == true)
			{
				this.button_DrawPoints.Text = "Hide Points";
			}
			else
			{
				this.button_DrawPoints.Text = "Draw Points";
			}
		}
		private void ToggleDetailImageTrack()
		{
			m_detailImageTrack ^= true;
			SetDetailImageTrackButtonState();
		}
		private void SetDetailImageTrackButtonState()
		{
			if (m_detailImageTrack == true)
			{
				this.button_DetailImageTrack.Text = "Lock";
			}
			else
			{
				this.button_DetailImageTrack.Text = "Track";
			}
		}
		private void scroll_DetailImageScale_ValueChanged(object sender, EventArgs e)
		{
			int value = this.scroll_DetailImageScale.Value;
			m_detailImageDisplayScale = (float)(value);
			RefreshDetailImage();
			SetStatusText("ImageScale:" + value);
		}
		private void RefreshDisplayImage()
		{
			m_displayGR.Clear(Color.DarkGray);
			int mX = m_displayImageX;
			int mY = m_displayImageY;
			int sX = (int)((float)(mX) / m_displayImageDisplayScale);
			int sY = (int)((float)(mY) / m_displayImageDisplayScale);

			if (m_loadedImage != null)
			{
				int dX = 0;
				int dY = 0;
				int dW = m_displayImageWidth;
				int dH = m_displayImageHeight;
				Rectangle destRect = new Rectangle(dX, dY, dW, dH);

				int sW = (int)((float)(m_displayImageWidth) / m_displayImageDisplayScale);
				int sH = (int)((float)(m_displayImageHeight) / m_displayImageDisplayScale);
				Rectangle srcRect = new Rectangle(sX, sY, sW, sH);

				m_displayGR.DrawImage(m_loadedImage, destRect, srcRect, GraphicsUnit.Pixel);
			}
			if (m_displayPoints)
			{
				int pointWidth = 10;
				int pointHeight = 10;
				DrawPoints(m_displayGR, pointWidth, pointHeight, sX, sY, 1.0f/m_displayImageDisplayScale);
			}
			picturebox_DisplayImage.Image = m_displayImage;
		}
		private void DrawPoints(Graphics gr, int pointWidth, int pointHeight, int x0, int y0, float scale)
		{
			Pen pointColour = Pens.Yellow;
			int n = m_points.Count;
			for (int i = 0; i < n; i++)
			{
				EastingNorthingPoint point = m_points[i];
				Vector2 pixel = point.Pixel;

				int x = (int)((float)(pixel.X - x0) / scale) - pointWidth/2;
				int y = (int)((float)(pixel.Y - y0) / scale) - pointHeight/2;
				gr.DrawRectangle(pointColour, x, y, pointWidth, pointHeight);
				gr.DrawLine(pointColour, x, y, x + pointWidth, y + pointHeight);
				gr.DrawLine(pointColour, x, y + pointHeight, x + pointWidth, y);
			}
		}
		private void ComputeImageXY()
		{
			int mX = m_detailImageX + m_displayImageX;
			int mY = m_detailImageY + m_displayImageY;
			mX = (int)((float)(mX) / m_displayImageDisplayScale);
			mY = (int)((float)(mY) / m_displayImageDisplayScale);
			m_sourceImagePixelX = mX;
			m_sourceImagePixelY = mY;
			m_pointPixelX = m_sourceImagePixelX;
			m_pointPixelY = m_sourceImagePixelY;
		}
		private void RefreshDetailImage()
		{
			ComputeImageXY();

			m_detailGR.Clear(Color.DarkGray);
			int dW = m_detailImageWidth;
			int dH = m_detailImageHeight;
			int sW = (int)((float)(m_detailImageWidth) / (float)m_detailImageDisplayScale);
			int sH = (int)((float)(m_detailImageHeight) / (float)m_detailImageDisplayScale);
			int sX = m_sourceImagePixelX - (sW / 2);
			int sY = m_sourceImagePixelY - (sH / 2);
			if (m_loadedImage != null) 
			{
				int dX = 0;
				int dY = 0;
				Rectangle destRect = new Rectangle(dX, dY, dW, dH);

				Rectangle srcRect = new Rectangle(sX, sY, sW, sH);

				m_detailGR.DrawImage(m_loadedImage, destRect, srcRect, GraphicsUnit.Pixel);
			}

			if (m_displayPoints)
			{
				int pointWidth = 10;
				int pointHeight = 10;
				DrawPoints(m_detailGR, pointWidth, pointHeight, sX, sY, 1.0f/m_detailImageDisplayScale);
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
		private int m_pointPixelX;
		private int m_pointPixelY;

		private Bitmap m_detailImage;
		private Graphics m_detailGR;
		private int m_detailImageWidth;
		private int m_detailImageHeight;
		private float m_detailImageDisplayScale;
		private int m_detailImageX;
		private int m_detailImageY;

		private int m_dragX;
		private int m_dragY;
		private bool m_dragging;
		private bool m_detailImageTrack;

		private int m_easting;
		private int m_northing;
		private float m_eastingZero;
		private float m_northingZero;
		private float m_eastingScale;
		private float m_northingScale;
		private MouseButtons MOUSE_BUTTON_DRAG = MouseButtons.Left;
		private MouseButtons MOUSE_BUTTON_ENTER_EASTING_NORTHING = MouseButtons.Left;
		private MouseButtons MOUSE_BUTTON_DETAIL_LOCK_TOGGLE = MouseButtons.Right;

		private Timer m_clickTimer;
		private List<EastingNorthingPoint> m_points;
		private int m_clickTimerMS;
		private int m_numClicks = 0;
		private DateTime m_now;
		private MouseEventArgs m_clickMouseEventArgs;
		private MouseEventArgs m_downMouseEventArgs;
		private bool m_displayPoints;
	}
}