using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace SamMapTool
{
	public partial class HelpDialog : Form
	{
		public HelpDialog()
		{
			InitializeComponent();
		}
		private void button_OK_Click (object sender, EventArgs e)
		{
			Close();
		}
		private void onLoad (object sender, System.EventArgs e)
		{
			try
			{
				_assembly = Assembly.GetExecutingAssembly();
				_rtfStreamReader = new StreamReader(_assembly.GetManifestResourceStream("SamMapTool.help.rtf"));
			}
			catch
			{
				MessageBox.Show("Error accessing resources!");
			}		
			this.richTextBox1.Rtf = _rtfStreamReader.ReadToEnd();
			this.richTextBox1.ReadOnly = true;
/*
			while (_rtfStreamReader.EndOfStream == false)
			{
				this.richTextBox1.AppendText(_rtfStreamReader.ReadLine());
			}
*/
		}

    Assembly _assembly;
    StreamReader _rtfStreamReader;
	}
}
