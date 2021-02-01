namespace Nexus
{
    partial class frmAdmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdmin));
            this.grpTables = new System.Windows.Forms.GroupBox();
            this.optData = new System.Windows.Forms.RadioButton();
            this.optSystem = new System.Windows.Forms.RadioButton();
            this.optSettings = new System.Windows.Forms.RadioButton();
            this.optQuestions = new System.Windows.Forms.RadioButton();
            this.optCard = new System.Windows.Forms.RadioButton();
            this.cmdAccEnableCard = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.cmdAccEnable = new System.Windows.Forms.Button();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.grpTables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // grpTables
            // 
            this.grpTables.Controls.Add(this.optData);
            this.grpTables.Controls.Add(this.optSystem);
            this.grpTables.Controls.Add(this.optSettings);
            this.grpTables.Controls.Add(this.optQuestions);
            this.grpTables.Controls.Add(this.optCard);
            this.grpTables.Location = new System.Drawing.Point(12, 7);
            this.grpTables.Name = "grpTables";
            this.grpTables.Size = new System.Drawing.Size(401, 34);
            this.grpTables.TabIndex = 26;
            this.grpTables.TabStop = false;
            // 
            // optData
            // 
            this.optData.AutoSize = true;
            this.optData.Location = new System.Drawing.Point(92, 10);
            this.optData.Name = "optData";
            this.optData.Size = new System.Drawing.Size(69, 17);
            this.optData.TabIndex = 24;
            this.optData.Text = "Data Log";
            this.optData.UseVisualStyleBackColor = true;
            // 
            // optSystem
            // 
            this.optSystem.AutoSize = true;
            this.optSystem.Checked = true;
            this.optSystem.Location = new System.Drawing.Point(6, 10);
            this.optSystem.Name = "optSystem";
            this.optSystem.Size = new System.Drawing.Size(80, 17);
            this.optSystem.TabIndex = 23;
            this.optSystem.TabStop = true;
            this.optSystem.Text = "System Log";
            this.optSystem.UseVisualStyleBackColor = true;
            // 
            // optSettings
            // 
            this.optSettings.AutoSize = true;
            this.optSettings.Location = new System.Drawing.Point(331, 10);
            this.optSettings.Name = "optSettings";
            this.optSettings.Size = new System.Drawing.Size(63, 17);
            this.optSettings.TabIndex = 2;
            this.optSettings.Text = "Settings";
            this.optSettings.UseVisualStyleBackColor = true;
            // 
            // optQuestions
            // 
            this.optQuestions.AutoSize = true;
            this.optQuestions.Location = new System.Drawing.Point(253, 10);
            this.optQuestions.Name = "optQuestions";
            this.optQuestions.Size = new System.Drawing.Size(72, 17);
            this.optQuestions.TabIndex = 1;
            this.optQuestions.Text = "Questions";
            this.optQuestions.UseVisualStyleBackColor = true;
            // 
            // optCard
            // 
            this.optCard.AutoSize = true;
            this.optCard.Location = new System.Drawing.Point(167, 10);
            this.optCard.Name = "optCard";
            this.optCard.Size = new System.Drawing.Size(80, 17);
            this.optCard.TabIndex = 0;
            this.optCard.Text = "Card Status";
            this.optCard.UseVisualStyleBackColor = true;
            // 
            // cmdAccEnableCard
            // 
            this.cmdAccEnableCard.Location = new System.Drawing.Point(584, 13);
            this.cmdAccEnableCard.Name = "cmdAccEnableCard";
            this.cmdAccEnableCard.Size = new System.Drawing.Size(158, 28);
            this.cmdAccEnableCard.TabIndex = 25;
            this.cmdAccEnableCard.Text = "Toggle Single Card Status";
            this.cmdAccEnableCard.UseVisualStyleBackColor = true;
            this.cmdAccEnableCard.Visible = false;
            this.cmdAccEnableCard.Click += new System.EventHandler(this.cmdAccEnableCard_Click_1);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(12, 47);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(893, 522);
            this.dgvData.TabIndex = 24;
            // 
            // cmdAccEnable
            // 
            this.cmdAccEnable.Location = new System.Drawing.Point(748, 13);
            this.cmdAccEnable.Name = "cmdAccEnable";
            this.cmdAccEnable.Size = new System.Drawing.Size(158, 28);
            this.cmdAccEnable.TabIndex = 23;
            this.cmdAccEnable.Text = "Disable Global Card Control";
            this.cmdAccEnable.UseVisualStyleBackColor = true;
            this.cmdAccEnable.Visible = false;
            this.cmdAccEnable.Click += new System.EventHandler(this.cmdAccEnable_Click_1);
            // 
            // cmdLoad
            // 
            this.cmdLoad.Location = new System.Drawing.Point(420, 13);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(158, 28);
            this.cmdLoad.TabIndex = 27;
            this.cmdLoad.Text = "Load Data";
            this.cmdLoad.UseVisualStyleBackColor = true;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click_1);
            // 
            // frmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 585);
            this.Controls.Add(this.grpTables);
            this.Controls.Add(this.cmdAccEnableCard);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.cmdAccEnable);
            this.Controls.Add(this.cmdLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nexus - Admin";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmAdmin_Load);
            this.grpTables.ResumeLayout(false);
            this.grpTables.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTables;
        private System.Windows.Forms.RadioButton optData;
        private System.Windows.Forms.RadioButton optSystem;
        private System.Windows.Forms.RadioButton optSettings;
        private System.Windows.Forms.RadioButton optQuestions;
        private System.Windows.Forms.RadioButton optCard;
        private System.Windows.Forms.Button cmdAccEnableCard;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button cmdAccEnable;
        private System.Windows.Forms.Button cmdLoad;
    }
}