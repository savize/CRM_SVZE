namespace MomoCRM
{
    partial class MessageB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageB));
            this.Title = new System.Windows.Forms.Label();
            this.Desc = new System.Windows.Forms.Label();
            this.yesBtn = new System.Windows.Forms.Label();
            this.NoBtn = new System.Windows.Forms.Label();
            this.OkBtn = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(193)))), ((int)(((byte)(217)))));
            this.Title.Location = new System.Drawing.Point(37, 42);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(376, 29);
            this.Title.TabIndex = 0;
            this.Title.Text = "Title";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Desc
            // 
            this.Desc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.Desc.Location = new System.Drawing.Point(33, 99);
            this.Desc.Name = "Desc";
            this.Desc.Size = new System.Drawing.Size(380, 110);
            this.Desc.TabIndex = 1;
            this.Desc.Text = "Description";
            // 
            // yesBtn
            // 
            this.yesBtn.AutoSize = true;
            this.yesBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.yesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yesBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(193)))), ((int)(((byte)(217)))));
            this.yesBtn.Location = new System.Drawing.Point(128, 224);
            this.yesBtn.Name = "yesBtn";
            this.yesBtn.Size = new System.Drawing.Size(58, 29);
            this.yesBtn.TabIndex = 2;
            this.yesBtn.Text = "Yes";
            this.yesBtn.Click += new System.EventHandler(this.yesBtn_Click);
            // 
            // NoBtn
            // 
            this.NoBtn.AutoSize = true;
            this.NoBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NoBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.NoBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(193)))), ((int)(((byte)(217)))));
            this.NoBtn.Location = new System.Drawing.Point(275, 224);
            this.NoBtn.Name = "NoBtn";
            this.NoBtn.Size = new System.Drawing.Size(47, 29);
            this.NoBtn.TabIndex = 3;
            this.NoBtn.Text = "No";
            this.NoBtn.Click += new System.EventHandler(this.NoBtn_Click);
            // 
            // OkBtn
            // 
            this.OkBtn.AutoSize = true;
            this.OkBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OkBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OkBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(193)))), ((int)(((byte)(217)))));
            this.OkBtn.Location = new System.Drawing.Point(202, 224);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(50, 29);
            this.OkBtn.TabIndex = 4;
            this.OkBtn.Text = "OK";
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // MessageB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(0)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(450, 280);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.NoBtn);
            this.Controls.Add(this.yesBtn);
            this.Controls.Add(this.Desc);
            this.Controls.Add(this.Title);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MessageB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MessageB";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label Title;
        public System.Windows.Forms.Label Desc;
        public System.Windows.Forms.Label yesBtn;
        public System.Windows.Forms.Label NoBtn;
        public System.Windows.Forms.Label OkBtn;
    }
}