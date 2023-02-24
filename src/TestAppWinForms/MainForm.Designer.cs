namespace TestAppWinForms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblWindowInfo = new System.Windows.Forms.Label();
            this.cbFollowMouse = new System.Windows.Forms.CheckBox();
            this.tbWindowInfo = new System.Windows.Forms.TextBox();
            this.lblMousePos = new System.Windows.Forms.Label();
            this.tbMousePos = new System.Windows.Forms.TextBox();
            this.lblWindowPos = new System.Windows.Forms.Label();
            this.tbWindowPos = new System.Windows.Forms.TextBox();
            this.timerFollow = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblWindowInfo
            // 
            this.lblWindowInfo.AutoSize = true;
            this.lblWindowInfo.Location = new System.Drawing.Point(12, 9);
            this.lblWindowInfo.Name = "lblWindowInfo";
            this.lblWindowInfo.Size = new System.Drawing.Size(145, 15);
            this.lblWindowInfo.TabIndex = 0;
            this.lblWindowInfo.Text = "Window Title and Process:";
            // 
            // cbFollowMouse
            // 
            this.cbFollowMouse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFollowMouse.AutoSize = true;
            this.cbFollowMouse.CheckAlign = System.Drawing.ContentAlignment.TopRight;
            this.cbFollowMouse.Location = new System.Drawing.Point(260, 8);
            this.cbFollowMouse.Name = "cbFollowMouse";
            this.cbFollowMouse.Size = new System.Drawing.Size(68, 19);
            this.cbFollowMouse.TabIndex = 1;
            this.cbFollowMouse.Text = "Enabled";
            this.cbFollowMouse.UseVisualStyleBackColor = true;
            this.cbFollowMouse.CheckedChanged += new System.EventHandler(this.cbFollowMouse_CheckedChanged);
            // 
            // tbWindowInfo
            // 
            this.tbWindowInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWindowInfo.Location = new System.Drawing.Point(12, 27);
            this.tbWindowInfo.Multiline = true;
            this.tbWindowInfo.Name = "tbWindowInfo";
            this.tbWindowInfo.ReadOnly = true;
            this.tbWindowInfo.Size = new System.Drawing.Size(316, 72);
            this.tbWindowInfo.TabIndex = 2;
            // 
            // lblMousePos
            // 
            this.lblMousePos.AutoSize = true;
            this.lblMousePos.Location = new System.Drawing.Point(12, 102);
            this.lblMousePos.Name = "lblMousePos";
            this.lblMousePos.Size = new System.Drawing.Size(92, 15);
            this.lblMousePos.TabIndex = 3;
            this.lblMousePos.Text = "Mouse Position:";
            // 
            // tbMousePos
            // 
            this.tbMousePos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMousePos.Location = new System.Drawing.Point(12, 120);
            this.tbMousePos.Multiline = true;
            this.tbMousePos.Name = "tbMousePos";
            this.tbMousePos.ReadOnly = true;
            this.tbMousePos.Size = new System.Drawing.Size(316, 72);
            this.tbMousePos.TabIndex = 4;
            // 
            // lblWindowPos
            // 
            this.lblWindowPos.AutoSize = true;
            this.lblWindowPos.Location = new System.Drawing.Point(12, 195);
            this.lblWindowPos.Name = "lblWindowPos";
            this.lblWindowPos.Size = new System.Drawing.Size(136, 15);
            this.lblWindowPos.TabIndex = 5;
            this.lblWindowPos.Text = "Active Window Position:";
            // 
            // tbWindowPos
            // 
            this.tbWindowPos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWindowPos.Location = new System.Drawing.Point(12, 213);
            this.tbWindowPos.Multiline = true;
            this.tbWindowPos.Name = "tbWindowPos";
            this.tbWindowPos.ReadOnly = true;
            this.tbWindowPos.Size = new System.Drawing.Size(316, 72);
            this.tbWindowPos.TabIndex = 6;
            // 
            // timerFollow
            // 
            this.timerFollow.Interval = 250;
            this.timerFollow.Tick += new System.EventHandler(this.timerFollow_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 297);
            this.Controls.Add(this.tbWindowPos);
            this.Controls.Add(this.lblWindowPos);
            this.Controls.Add(this.tbMousePos);
            this.Controls.Add(this.lblMousePos);
            this.Controls.Add(this.tbWindowInfo);
            this.Controls.Add(this.cbFollowMouse);
            this.Controls.Add(this.lblWindowInfo);
            this.Name = "MainForm";
            this.Text = "Window Spy";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblWindowInfo;
        private CheckBox cbFollowMouse;
        private TextBox tbWindowInfo;
        private Label lblMousePos;
        private TextBox tbMousePos;
        private Label lblWindowPos;
        private TextBox tbWindowPos;
        private System.Windows.Forms.Timer timerFollow;
    }
}
