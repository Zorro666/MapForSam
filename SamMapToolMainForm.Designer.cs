// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace SamMapTool
{
    
    #region Windows Form Designer generated code
    public partial class SamMapToolMain
    {
        private void InitializeComponent()
        {
            this.panel_Top = new System.Windows.Forms.Panel();
            this.button_Quit = new System.Windows.Forms.Button();
            this.picturebox_DisplayImage = new System.Windows.Forms.PictureBox();
            this.text_Status = new System.Windows.Forms.TextBox();
            this.button_LoadImage = new System.Windows.Forms.Button();
            this.pictureBox_DetailImage = new System.Windows.Forms.PictureBox();
            this.scroll_DetailImageScale = new System.Windows.Forms.HScrollBar();
            this.text_Northing = new System.Windows.Forms.TextBox();
            this.label_Northing = new System.Windows.Forms.Label();
            this.label_Easting = new System.Windows.Forms.Label();
            this.text_Easting = new System.Windows.Forms.TextBox();
            this.label_Origin = new System.Windows.Forms.Label();
            this.text_ImageX = new System.Windows.Forms.TextBox();
            this.text_ImageY = new System.Windows.Forms.TextBox();
            this.label_ImageY = new System.Windows.Forms.Label();
            this.button_DetailImageTrack = new System.Windows.Forms.Button();
            this.button_DrawPoints = new System.Windows.Forms.Button();
            this.button_EnterEastingNorthing = new System.Windows.Forms.Button();
            this.label_ImageX = new System.Windows.Forms.Label();
            this.text_OriginX = new System.Windows.Forms.TextBox();
            this.text_OriginY = new System.Windows.Forms.TextBox();
            this.label_Scale = new System.Windows.Forms.Label();
            this.text_ScaleX = new System.Windows.Forms.TextBox();
            this.text_ScaleY = new System.Windows.Forms.TextBox();
            this.panel_Top.SuspendLayout();
            // 
            // panel_Top
            // 
            this.panel_Top.Name = "panel_Top";
            this.panel_Top.Location = new System.Drawing.Point(16, 24);
            this.panel_Top.TabIndex = 0;
            this.panel_Top.Controls.Add(this.button_Quit);
            this.panel_Top.Controls.Add(this.button_LoadImage);
            this.panel_Top.Controls.Add(this.button_DetailImageTrack);
            this.panel_Top.Controls.Add(this.button_DrawPoints);
            this.panel_Top.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_Top.Size = new System.Drawing.Size(728, 48);
            this.panel_Top.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel_Top.Text = "panel1";
            // 
            // button_Quit
            // 
            this.button_Quit.Name = "button_Quit";
            this.button_Quit.ForeColor = System.Drawing.SystemColors.InfoText;
            this.button_Quit.Location = new System.Drawing.Point(8, 12);
            this.button_Quit.TabIndex = 0;
            this.button_Quit.Size = new System.Drawing.Size(75, 24);
            this.button_Quit.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.button_Quit.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button_Quit.Text = "Quit";
            this.button_Quit.UseVisualStyleBackColor = true;
            this.button_Quit.Click += new System.EventHandler(this.button_Quit_Click);
            // 
            // picturebox_DisplayImage
            // 
            this.picturebox_DisplayImage.Name = "picturebox_DisplayImage";
            this.picturebox_DisplayImage.Location = new System.Drawing.Point(16, 80);
            this.picturebox_DisplayImage.Image = null;
            this.picturebox_DisplayImage.TabIndex = 0;
            this.picturebox_DisplayImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picturebox_DisplayImage.Size = new System.Drawing.Size(512, 512);
            this.picturebox_DisplayImage.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.picturebox_DisplayImage.Text = "pictureBox1";
            // 
            // text_Status
            // 
            this.text_Status.Name = "text_Status";
            this.text_Status.TabStop = false;
            this.text_Status.ForeColor = System.Drawing.SystemColors.WindowText;
            this.text_Status.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_Status.Location = new System.Drawing.Point(24, 608);
            this.text_Status.TabIndex = 1;
            this.text_Status.Size = new System.Drawing.Size(720, 20);
            this.text_Status.ReadOnly = true;
            this.text_Status.BackColor = System.Drawing.SystemColors.Window;
            this.text_Status.Text = "";
            // 
            // button_LoadImage
            // 
            this.button_LoadImage.Name = "button_LoadImage";
            this.button_LoadImage.ForeColor = System.Drawing.SystemColors.InfoText;
            this.button_LoadImage.Location = new System.Drawing.Point(96, 12);
            this.button_LoadImage.TabIndex = 1;
            this.button_LoadImage.Size = new System.Drawing.Size(75, 24);
            this.button_LoadImage.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.button_LoadImage.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button_LoadImage.Text = "Load Image";
            this.button_LoadImage.UseVisualStyleBackColor = true;
            this.button_LoadImage.Click += new System.EventHandler(this.button_LoadImage_Click);
            // 
            // pictureBox_DetailImage
            // 
            this.pictureBox_DetailImage.Name = "pictureBox_DetailImage";
            this.pictureBox_DetailImage.Location = new System.Drawing.Point(536, 80);
            this.pictureBox_DetailImage.Image = null;
            this.pictureBox_DetailImage.TabIndex = 2;
            this.pictureBox_DetailImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_DetailImage.Size = new System.Drawing.Size(200, 200);
            this.pictureBox_DetailImage.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox_DetailImage.Text = "pictureBox2";
            // 
            // scroll_DetailImageScale
            // 
            this.scroll_DetailImageScale.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.scroll_DetailImageScale.Tag = "";
            this.scroll_DetailImageScale.Size = new System.Drawing.Size(184, 29);
            this.scroll_DetailImageScale.Name = "scroll_DetailImageScale";
            this.scroll_DetailImageScale.Value = 4;
            this.scroll_DetailImageScale.LargeChange = 1;
            this.scroll_DetailImageScale.Location = new System.Drawing.Point(544, 288);
            this.scroll_DetailImageScale.Maximum = 10;
            this.scroll_DetailImageScale.Minimum = 2;
            this.scroll_DetailImageScale.TabIndex = 2;
            // 
            // text_Northing
            // 
            this.text_Northing.Name = "text_Northing";
            this.text_Northing.ForeColor = System.Drawing.SystemColors.WindowText;
            this.text_Northing.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_Northing.Location = new System.Drawing.Point(616, 424);
            this.text_Northing.TabIndex = 5;
            this.text_Northing.Size = new System.Drawing.Size(96, 20);
            this.text_Northing.MaxLength = 6;
            this.text_Northing.BackColor = System.Drawing.SystemColors.Window;
            this.text_Northing.Text = "";
            // 
            // label_Northing
            // 
            this.label_Northing.Name = "label_Northing";
            this.label_Northing.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label_Northing.Location = new System.Drawing.Point(552, 424);
            this.label_Northing.Image = null;
            this.label_Northing.TabIndex = 5;
            this.label_Northing.Size = new System.Drawing.Size(56, 20);
            this.label_Northing.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label_Northing.Text = "Northing:";
            // 
            // label_Easting
            // 
            this.label_Easting.Name = "label_Easting";
            this.label_Easting.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label_Easting.Location = new System.Drawing.Point(552, 392);
            this.label_Easting.Image = null;
            this.label_Easting.TabIndex = 5;
            this.label_Easting.Size = new System.Drawing.Size(56, 20);
            this.label_Easting.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label_Easting.Text = "Easting:";
            // 
            // text_Easting
            // 
            this.text_Easting.Name = "text_Easting";
            this.text_Easting.ForeColor = System.Drawing.SystemColors.WindowText;
            this.text_Easting.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_Easting.Location = new System.Drawing.Point(616, 392);
            this.text_Easting.TabIndex = 4;
            this.text_Easting.Size = new System.Drawing.Size(96, 20);
            this.text_Easting.MaxLength = 6;
            this.text_Easting.BackColor = System.Drawing.SystemColors.Window;
            this.text_Easting.Text = "";
            // 
            // label_Origin
            // 
            this.label_Origin.Name = "label_Origin";
            this.label_Origin.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label_Origin.Location = new System.Drawing.Point(536, 490);
            this.label_Origin.Image = null;
            this.label_Origin.TabIndex = 5;
            this.label_Origin.Size = new System.Drawing.Size(48, 20);
            this.label_Origin.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label_Origin.Text = "Origin:";
            // 
            // text_ImageX
            // 
            this.text_ImageX.Name = "text_ImageX";
            this.text_ImageX.TabStop = false;
            this.text_ImageX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.text_ImageX.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_ImageX.Location = new System.Drawing.Point(616, 332);
            this.text_ImageX.TabIndex = 4;
            this.text_ImageX.Size = new System.Drawing.Size(96, 20);
            this.text_ImageX.ReadOnly = true;
            this.text_ImageX.MaxLength = 4;
            this.text_ImageX.Text = "";
            // 
            // text_ImageY
            // 
            this.text_ImageY.Name = "text_ImageY";
            this.text_ImageY.TabStop = false;
            this.text_ImageY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.text_ImageY.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_ImageY.Location = new System.Drawing.Point(616, 364);
            this.text_ImageY.TabIndex = 5;
            this.text_ImageY.Size = new System.Drawing.Size(96, 20);
            this.text_ImageY.ReadOnly = true;
            this.text_ImageY.MaxLength = 4;
            this.text_ImageY.Text = "";
            // 
            // label_ImageY
            // 
            this.label_ImageY.Name = "label_ImageY";
            this.label_ImageY.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label_ImageY.Location = new System.Drawing.Point(552, 362);
            this.label_ImageY.Image = null;
            this.label_ImageY.TabIndex = 5;
            this.label_ImageY.Size = new System.Drawing.Size(56, 22);
            this.label_ImageY.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label_ImageY.Text = "Image Y:";
            // 
            // button_DetailImageTrack
            // 
            this.button_DetailImageTrack.Name = "button_DetailImageTrack";
            this.button_DetailImageTrack.ForeColor = System.Drawing.SystemColors.InfoText;
            this.button_DetailImageTrack.Location = new System.Drawing.Point(552, 8);
            this.button_DetailImageTrack.TabIndex = 2;
            this.button_DetailImageTrack.Size = new System.Drawing.Size(70, 24);
            this.button_DetailImageTrack.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.button_DetailImageTrack.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button_DetailImageTrack.Text = "Lock";
            this.button_DetailImageTrack.UseVisualStyleBackColor = true;
            this.button_DetailImageTrack.Click += new System.EventHandler(this.button_DetailImageTrack_Click);
            // 
            // button_DrawPoints
            // 
            this.button_DrawPoints.Name = "button_DrawPoints";
            this.button_DrawPoints.ForeColor = System.Drawing.SystemColors.InfoText;
            this.button_DrawPoints.Location = new System.Drawing.Point(632, 8);
            this.button_DrawPoints.TabIndex = 3;
            this.button_DrawPoints.Size = new System.Drawing.Size(80, 24);
            this.button_DrawPoints.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.button_DrawPoints.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button_DrawPoints.Text = "Draw Points";
            this.button_DrawPoints.UseVisualStyleBackColor = true;
            this.button_DrawPoints.Click += new System.EventHandler(this.button_DrawPoints_Click);
            // 
            // button_EnterEastingNorthing
            // 
            this.button_EnterEastingNorthing.Name = "button_EnterEastingNorthing";
            this.button_EnterEastingNorthing.ForeColor = System.Drawing.SystemColors.InfoText;
            this.button_EnterEastingNorthing.Location = new System.Drawing.Point(568, 456);
            this.button_EnterEastingNorthing.TabIndex = 6;
            this.button_EnterEastingNorthing.Size = new System.Drawing.Size(152, 24);
            this.button_EnterEastingNorthing.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.button_EnterEastingNorthing.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button_EnterEastingNorthing.Text = "Enter Easting Northing";
            this.button_EnterEastingNorthing.UseVisualStyleBackColor = true;
            this.button_EnterEastingNorthing.Click += new System.EventHandler(this.button_EnterEastingNorthing_Click);
            // 
            // label_ImageX
            // 
            this.label_ImageX.Name = "label_ImageX";
            this.label_ImageX.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label_ImageX.Location = new System.Drawing.Point(552, 332);
            this.label_ImageX.Image = null;
            this.label_ImageX.TabIndex = 5;
            this.label_ImageX.Size = new System.Drawing.Size(56, 20);
            this.label_ImageX.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label_ImageX.Text = "Image X:";
            // 
            // text_OriginX
            // 
            this.text_OriginX.Name = "text_OriginX";
            this.text_OriginX.TabStop = false;
            this.text_OriginX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.text_OriginX.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_OriginX.Location = new System.Drawing.Point(592, 488);
            this.text_OriginX.TabIndex = 4;
            this.text_OriginX.Size = new System.Drawing.Size(72, 20);
            this.text_OriginX.ReadOnly = true;
            this.text_OriginX.MaxLength = 4;
            this.text_OriginX.Text = "";
            // 
            // text_OriginY
            // 
            this.text_OriginY.Name = "text_OriginY";
            this.text_OriginY.TabStop = false;
            this.text_OriginY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.text_OriginY.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_OriginY.Location = new System.Drawing.Point(670, 488);
            this.text_OriginY.TabIndex = 4;
            this.text_OriginY.Size = new System.Drawing.Size(72, 20);
            this.text_OriginY.ReadOnly = true;
            this.text_OriginY.MaxLength = 4;
            this.text_OriginY.Text = "";
            // 
            // label_Scale
            // 
            this.label_Scale.Name = "label_Scale";
            this.label_Scale.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label_Scale.Location = new System.Drawing.Point(536, 520);
            this.label_Scale.Image = null;
            this.label_Scale.TabIndex = 5;
            this.label_Scale.Size = new System.Drawing.Size(48, 20);
            this.label_Scale.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label_Scale.Text = "Scale:";
            // 
            // text_ScaleX
            // 
            this.text_ScaleX.Name = "text_ScaleX";
            this.text_ScaleX.TabStop = false;
            this.text_ScaleX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.text_ScaleX.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_ScaleX.Location = new System.Drawing.Point(592, 518);
            this.text_ScaleX.TabIndex = 4;
            this.text_ScaleX.Size = new System.Drawing.Size(72, 20);
            this.text_ScaleX.ReadOnly = true;
            this.text_ScaleX.MaxLength = 4;
            this.text_ScaleX.Text = "";
            // 
            // text_ScaleY
            // 
            this.text_ScaleY.Name = "text_ScaleY";
            this.text_ScaleY.TabStop = false;
            this.text_ScaleY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.text_ScaleY.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_ScaleY.Location = new System.Drawing.Point(670, 518);
            this.text_ScaleY.TabIndex = 4;
            this.text_ScaleY.Size = new System.Drawing.Size(72, 20);
            this.text_ScaleY.ReadOnly = true;
            this.text_ScaleY.MaxLength = 4;
            this.text_ScaleY.Text = "";
            // 
            // SamMapToolMain
            // 
            this.Name = "SamMapToolMain";
            this.ClientSize = new System.Drawing.Size(758, 645);
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Controls.Add(this.panel_Top);
            this.Controls.Add(this.picturebox_DisplayImage);
            this.Controls.Add(this.text_Status);
            this.Controls.Add(this.pictureBox_DetailImage);
            this.Controls.Add(this.scroll_DetailImageScale);
            this.Controls.Add(this.text_Northing);
            this.Controls.Add(this.label_Northing);
            this.Controls.Add(this.label_Easting);
            this.Controls.Add(this.text_Easting);
            this.Controls.Add(this.label_Origin);
            this.Controls.Add(this.text_ImageX);
            this.Controls.Add(this.text_ImageY);
            this.Controls.Add(this.label_ImageY);
            this.Controls.Add(this.button_EnterEastingNorthing);
            this.Controls.Add(this.label_ImageX);
            this.Controls.Add(this.text_OriginX);
            this.Controls.Add(this.text_OriginY);
            this.Controls.Add(this.label_Scale);
            this.Controls.Add(this.text_ScaleX);
            this.Controls.Add(this.text_ScaleY);
            this.KeyPreview = true;
            this.Text = "Sam's Map Tool";
            this.panel_Top.ResumeLayout(false);
        }
        private System.Windows.Forms.Panel panel_Top;
        private System.Windows.Forms.Button button_Quit;
        private System.Windows.Forms.PictureBox picturebox_DisplayImage;
        private System.Windows.Forms.TextBox text_Status;
        private System.Windows.Forms.Button button_LoadImage;
        private System.Windows.Forms.PictureBox pictureBox_DetailImage;
        private System.Windows.Forms.HScrollBar scroll_DetailImageScale;
        private System.Windows.Forms.TextBox text_Northing;
        private System.Windows.Forms.Label label_Northing;
        private System.Windows.Forms.Label label_Easting;
        private System.Windows.Forms.TextBox text_Easting;
        private System.Windows.Forms.Label label_Origin;
        private System.Windows.Forms.TextBox text_ImageX;
        private System.Windows.Forms.TextBox text_ImageY;
        private System.Windows.Forms.Label label_ImageY;
        private System.Windows.Forms.Button button_DetailImageTrack;
        private System.Windows.Forms.Button button_DrawPoints;
        private System.Windows.Forms.Button button_EnterEastingNorthing;
        private System.Windows.Forms.Label label_ImageX;
        private System.Windows.Forms.TextBox text_OriginX;
        private System.Windows.Forms.TextBox text_OriginY;
        private System.Windows.Forms.Label label_Scale;
        private System.Windows.Forms.TextBox text_ScaleX;
        private System.Windows.Forms.TextBox text_ScaleY;
    }
    #endregion
}
