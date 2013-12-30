using System;

namespace SamMapTool
{
	public class Vector2
	{
		public Vector2()
		{
			m_x = 0;
			m_y = 0;
		}
		public Vector2(long x, long y)
		{
			m_x = x;
			m_y = y;
		}
		public long X
		{
			get { return m_x; }
			set { m_x = value; }
		}
		public long Y
		{
			get { return m_y; }
			set { m_y = value; }
		}
		public bool Same(Vector2 other)
		{
			if (m_x != other.X)
			{
				return false;
			}
			if (m_y != other.Y)
			{
				return false;
			}
			return true;
		}
		public void Add(long x, long y)
		{
			m_x += x;
			m_y += y;
		}

		private long m_x;
		private long m_y;
	}
}

