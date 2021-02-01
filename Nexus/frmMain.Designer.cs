namespace Nexus
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.dgvQueue = new System.Windows.Forms.DataGridView();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.lblQueue = new System.Windows.Forms.Label();
            this.lblQuestionHeader = new System.Windows.Forms.Label();
            this.lblResponse1 = new System.Windows.Forms.Label();
            this.lblResponse2 = new System.Windows.Forms.Label();
            this.lblGetTemp = new System.Windows.Forms.Label();
            this.lblAdmin = new System.Windows.Forms.Label();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.lblResults = new System.Windows.Forms.Label();
            this.lblQuit = new System.Windows.Forms.Label();
            this.axDataqSdk1 = new AxDATAQSDKLib.AxDataqSdk();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axDataqSdk1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(29, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(199, 152);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Oswald SemiBold", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(234, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1664, 152);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "WELCOME TO THE GUELPH GLASS PLANT";
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Roboto", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(252, 173);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(1646, 204);
            this.lblMessage.TabIndex = 6;
            this.lblMessage.Text = "We are conducting a simple health screening questionnaire. Your participation is " +
    "important to help us take precautionary measures to protect you and everyone in " +
    "this building. Thank you for your time.";
            // 
            // dgvQueue
            // 
            this.dgvQueue.AllowUserToAddRows = false;
            this.dgvQueue.AllowUserToDeleteRows = false;
            this.dgvQueue.AllowUserToResizeColumns = false;
            this.dgvQueue.AllowUserToResizeRows = false;
            this.dgvQueue.BackgroundColor = System.Drawing.Color.Black;
            this.dgvQueue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvQueue.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQueue.ColumnHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Roboto", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvQueue.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvQueue.Location = new System.Drawing.Point(12, 463);
            this.dgvQueue.Name = "dgvQueue";
            this.dgvQueue.ReadOnly = true;
            this.dgvQueue.RowHeadersVisible = false;
            this.dgvQueue.RowHeadersWidth = 92;
            this.dgvQueue.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQueue.Size = new System.Drawing.Size(240, 211);
            this.dgvQueue.TabIndex = 7;
            this.dgvQueue.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvQueue_CellContentClick);
            // 
            // lblQuestion
            // 
            this.lblQuestion.BackColor = System.Drawing.Color.Black;
            this.lblQuestion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblQuestion.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.lblQuestion.Font = new System.Drawing.Font("Roboto", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion.ForeColor = System.Drawing.Color.White;
            this.lblQuestion.Location = new System.Drawing.Point(262, 463);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(1636, 461);
            this.lblQuestion.TabIndex = 9;
            this.lblQuestion.Paint += new System.Windows.Forms.PaintEventHandler(this.lblQuestion_Paint);
            // 
            // lblQueue
            // 
            this.lblQueue.AutoSize = true;
            this.lblQueue.Font = new System.Drawing.Font("Oswald SemiBold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQueue.ForeColor = System.Drawing.Color.White;
            this.lblQueue.Location = new System.Drawing.Point(1, 378);
            this.lblQueue.Name = "lblQueue";
            this.lblQueue.Size = new System.Drawing.Size(161, 82);
            this.lblQueue.TabIndex = 12;
            this.lblQueue.Text = "QUEUE";
            // 
            // lblQuestionHeader
            // 
            this.lblQuestionHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblQuestionHeader.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.lblQuestionHeader.Font = new System.Drawing.Font("Oswald SemiBold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestionHeader.ForeColor = System.Drawing.Color.White;
            this.lblQuestionHeader.Location = new System.Drawing.Point(248, 378);
            this.lblQuestionHeader.Name = "lblQuestionHeader";
            this.lblQuestionHeader.Size = new System.Drawing.Size(1650, 82);
            this.lblQuestionHeader.TabIndex = 13;
            // 
            // lblResponse1
            // 
            this.lblResponse1.BackColor = System.Drawing.Color.Transparent;
            this.lblResponse1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResponse1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.lblResponse1.Font = new System.Drawing.Font("Roboto", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResponse1.ForeColor = System.Drawing.Color.White;
            this.lblResponse1.Location = new System.Drawing.Point(262, 938);
            this.lblResponse1.Name = "lblResponse1";
            this.lblResponse1.Size = new System.Drawing.Size(1636, 59);
            this.lblResponse1.TabIndex = 14;
            this.lblResponse1.Click += new System.EventHandler(this.lblResponse1_Click);
            // 
            // lblResponse2
            // 
            this.lblResponse2.BackColor = System.Drawing.Color.Transparent;
            this.lblResponse2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResponse2.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.lblResponse2.Font = new System.Drawing.Font("Roboto", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResponse2.ForeColor = System.Drawing.Color.White;
            this.lblResponse2.Location = new System.Drawing.Point(262, 1006);
            this.lblResponse2.Name = "lblResponse2";
            this.lblResponse2.Size = new System.Drawing.Size(1636, 59);
            this.lblResponse2.TabIndex = 15;
            this.lblResponse2.Click += new System.EventHandler(this.lblResponse2_Click);
            // 
            // lblGetTemp
            // 
            this.lblGetTemp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGetTemp.Font = new System.Drawing.Font("Roboto", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGetTemp.ForeColor = System.Drawing.Color.White;
            this.lblGetTemp.Location = new System.Drawing.Point(12, 937);
            this.lblGetTemp.Name = "lblGetTemp";
            this.lblGetTemp.Size = new System.Drawing.Size(240, 60);
            this.lblGetTemp.TabIndex = 18;
            this.lblGetTemp.Text = "Temp Scan";
            this.lblGetTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGetTemp.Click += new System.EventHandler(this.lblGetTemp_Click);
            this.lblGetTemp.Paint += new System.Windows.Forms.PaintEventHandler(this.lblGetTemp_Paint);
            // 
            // lblAdmin
            // 
            this.lblAdmin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAdmin.Font = new System.Drawing.Font("Roboto", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdmin.ForeColor = System.Drawing.Color.White;
            this.lblAdmin.Location = new System.Drawing.Point(12, 1005);
            this.lblAdmin.Name = "lblAdmin";
            this.lblAdmin.Size = new System.Drawing.Size(240, 60);
            this.lblAdmin.TabIndex = 19;
            this.lblAdmin.Text = "Admin";
            this.lblAdmin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAdmin.Click += new System.EventHandler(this.lblAdmin_Click);
            this.lblAdmin.Paint += new System.Windows.Forms.PaintEventHandler(this.lblAdmin_Paint);
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AllowUserToResizeColumns = false;
            this.dgvResults.AllowUserToResizeRows = false;
            this.dgvResults.BackgroundColor = System.Drawing.Color.Black;
            this.dgvResults.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.ColumnHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Roboto", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvResults.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvResults.Location = new System.Drawing.Point(12, 768);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.RowHeadersWidth = 92;
            this.dgvResults.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.Size = new System.Drawing.Size(240, 156);
            this.dgvResults.TabIndex = 20;
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Font = new System.Drawing.Font("Oswald SemiBold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResults.ForeColor = System.Drawing.Color.White;
            this.lblResults.Location = new System.Drawing.Point(1, 682);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(200, 82);
            this.lblResults.TabIndex = 21;
            this.lblResults.Text = "RESULTS";
            // 
            // lblQuit
            // 
            this.lblQuit.Location = new System.Drawing.Point(0, 2);
            this.lblQuit.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblQuit.Name = "lblQuit";
            this.lblQuit.Size = new System.Drawing.Size(88, 77);
            this.lblQuit.TabIndex = 22;
            this.lblQuit.Text = "label1";
            this.lblQuit.Click += new System.EventHandler(this.lblQuit_Click);
            this.lblQuit.DoubleClick += new System.EventHandler(this.lblQuit_DoubleClick);
            // 
            // axDataqSdk1
            // 
            this.axDataqSdk1.Enabled = true;
            this.axDataqSdk1.Location = new System.Drawing.Point(234, 18);
            this.axDataqSdk1.Name = "axDataqSdk1";
            this.axDataqSdk1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axDataqSdk1.OcxState")));
            this.axDataqSdk1.Size = new System.Drawing.Size(100, 50);
            this.axDataqSdk1.TabIndex = 23;
            this.axDataqSdk1.NewData += new AxDATAQSDKLib._DDataqSdkEvents_NewDataEventHandler(this.axDataqSdk1_NewData);
            this.axDataqSdk1.ControlError += new AxDATAQSDKLib._DDataqSdkEvents_ControlErrorEventHandler(this.axDataqSdk1_ControlError);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1924, 1080);
            this.Controls.Add(this.axDataqSdk1);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.lblAdmin);
            this.Controls.Add(this.lblGetTemp);
            this.Controls.Add(this.lblResponse2);
            this.Controls.Add(this.lblResponse1);
            this.Controls.Add(this.lblQuestionHeader);
            this.Controls.Add(this.lblQueue);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.dgvQueue);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblQuit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nexus";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axDataqSdk1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.DataGridView dgvQueue;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Label lblQueue;
        private System.Windows.Forms.Label lblQuestionHeader;
        private System.Windows.Forms.Label lblResponse1;
        private System.Windows.Forms.Label lblResponse2;
        private System.Windows.Forms.Label lblGetTemp;
        private System.Windows.Forms.Label lblAdmin;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.Label lblQuit;
        private AxDATAQSDKLib.AxDataqSdk axDataqSdk1;
    }
}

