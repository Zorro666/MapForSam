using System;

namespace JakeTest
{
	public class EastingNorthingPoint
	{
		public EastingNorthingPoint()
		{
			m_pixel = new Vector2();
			m_eastingNorthing = new Vector2();
		}
		public EastingNorthingPoint(long easting, long northing, long pixelX, long pixelY)
		{
			m_pixel = new Vector2(pixelX, pixelY);
			m_eastingNorthing = new Vector2(easting, northing);
		}
		public bool PixelSame(EastingNorthingPoint src)
		{
			return m_pixel.Same(src.m_pixel);
		}
		public Vector2 Pixel
		{
			get { return m_pixel; }
		}
		public Vector2 EastingNorthing
		{
			get { return m_eastingNorthing; }
		}
		private Vector2 m_pixel;
		private Vector2 m_eastingNorthing;
	}
}

