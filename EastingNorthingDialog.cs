using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JakeTest
{
	public partial class EastingNorthingDialog : Form
	{
		public EastingNorthingDialog(int easting, int northing)
    {
      m_ok = false;
      m_easting = easting;
      m_northing = northing;
      InitializeComponent();
		  this.button_OK.Click += new EventHandler(OK_Click);
      this.button_Cancel.Click += new EventHandler(Cancel_Click);
    }
    public bool GetOK()
    {
      return m_ok;
    }
    public int GetEasting()
    {
      return m_easting;
    }
    public int GetNorthing()
    {
      return m_northing;
    }
		private void Reset()
		{
      m_ok = false;
      m_easting = -1;
      m_northing = -1;
		}
    private void OK_Click (object sender, EventArgs e)
		{
			m_ok = true;
			try
			{
				m_easting = Convert.ToInt32(this.text_Easting.Text);
				m_northing = Convert.ToInt32(this.text_Northing.Text);
			}
			catch
			{
				Reset();
			}
      this.Close();
    }
    private void Cancel_Click(object sender, EventArgs e)
    {
			Reset();
      this.Close();
    }
		private int m_easting;
    private int m_northing;
    private bool m_ok;
	}
}
