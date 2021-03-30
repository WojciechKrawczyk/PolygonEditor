namespace PolygonEditor
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.contextMenuVertex = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuLineSegment = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalRestrictionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalRestrictionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lengthRestricionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRestrictionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuPolygon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deletePolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picBoxHor = new System.Windows.Forms.PictureBox();
            this.picBoxLen = new System.Windows.Forms.PictureBox();
            this.picBoxVer = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radioButtonLibAlg = new System.Windows.Forms.RadioButton();
            this.radioButtonBreAlg = new System.Windows.Forms.RadioButton();
            this.radioButtonWuAlg = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.contextMenuVertex.SuspendLayout();
            this.contextMenuLineSegment.SuspendLayout();
            this.contextMenuPolygon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxHor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxVer)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(12, 29);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1194, 712);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // contextMenuVertex
            // 
            this.contextMenuVertex.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuVertex.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteVertexToolStripMenuItem});
            this.contextMenuVertex.Name = "contextMenuVertex";
            this.contextMenuVertex.Size = new System.Drawing.Size(168, 28);
            // 
            // deleteVertexToolStripMenuItem
            // 
            this.deleteVertexToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteVertexToolStripMenuItem.Image")));
            this.deleteVertexToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteVertexToolStripMenuItem.Name = "deleteVertexToolStripMenuItem";
            this.deleteVertexToolStripMenuItem.Size = new System.Drawing.Size(167, 24);
            this.deleteVertexToolStripMenuItem.Text = "Delete Vertex";
            this.deleteVertexToolStripMenuItem.Click += new System.EventHandler(this.deleteVertexToolStripMenuItem_Click);
            // 
            // contextMenuLineSegment
            // 
            this.contextMenuLineSegment.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuLineSegment.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addVertexToolStripMenuItem,
            this.verticalRestrictionToolStripMenuItem,
            this.horizontalRestrictionToolStripMenuItem,
            this.lengthRestricionToolStripMenuItem,
            this.deleteRestrictionToolStripMenuItem});
            this.contextMenuLineSegment.Name = "contextMenuLineSegment";
            this.contextMenuLineSegment.Size = new System.Drawing.Size(227, 134);
            // 
            // addVertexToolStripMenuItem
            // 
            this.addVertexToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addVertexToolStripMenuItem.Image")));
            this.addVertexToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addVertexToolStripMenuItem.Name = "addVertexToolStripMenuItem";
            this.addVertexToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.addVertexToolStripMenuItem.Text = "Add Vertex";
            this.addVertexToolStripMenuItem.Click += new System.EventHandler(this.addVertexToolStripMenuItem_Click);
            // 
            // verticalRestrictionToolStripMenuItem
            // 
            this.verticalRestrictionToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("verticalRestrictionToolStripMenuItem.Image")));
            this.verticalRestrictionToolStripMenuItem.Name = "verticalRestrictionToolStripMenuItem";
            this.verticalRestrictionToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.verticalRestrictionToolStripMenuItem.Text = "Vertical Restriction";
            this.verticalRestrictionToolStripMenuItem.Click += new System.EventHandler(this.verticalRestrictionToolStripMenuItem_Click);
            // 
            // horizontalRestrictionToolStripMenuItem
            // 
            this.horizontalRestrictionToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("horizontalRestrictionToolStripMenuItem.Image")));
            this.horizontalRestrictionToolStripMenuItem.Name = "horizontalRestrictionToolStripMenuItem";
            this.horizontalRestrictionToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.horizontalRestrictionToolStripMenuItem.Text = "Horizontal Restriction";
            this.horizontalRestrictionToolStripMenuItem.Click += new System.EventHandler(this.horizontalRestrictionToolStripMenuItem_Click);
            // 
            // lengthRestricionToolStripMenuItem
            // 
            this.lengthRestricionToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("lengthRestricionToolStripMenuItem.Image")));
            this.lengthRestricionToolStripMenuItem.Name = "lengthRestricionToolStripMenuItem";
            this.lengthRestricionToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.lengthRestricionToolStripMenuItem.Text = "Length Restricion";
            this.lengthRestricionToolStripMenuItem.Click += new System.EventHandler(this.lengthRestricionToolStripMenuItem_Click);
            // 
            // deleteRestrictionToolStripMenuItem
            // 
            this.deleteRestrictionToolStripMenuItem.Enabled = false;
            this.deleteRestrictionToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteRestrictionToolStripMenuItem.Image")));
            this.deleteRestrictionToolStripMenuItem.Name = "deleteRestrictionToolStripMenuItem";
            this.deleteRestrictionToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.deleteRestrictionToolStripMenuItem.Text = "Delete Restriction";
            this.deleteRestrictionToolStripMenuItem.Click += new System.EventHandler(this.deleteRestrictionToolStripMenuItem_Click);
            // 
            // contextMenuPolygon
            // 
            this.contextMenuPolygon.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuPolygon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deletePolygonToolStripMenuItem});
            this.contextMenuPolygon.Name = "contextMenuPolygon";
            this.contextMenuPolygon.Size = new System.Drawing.Size(180, 28);
            // 
            // deletePolygonToolStripMenuItem
            // 
            this.deletePolygonToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deletePolygonToolStripMenuItem.Image")));
            this.deletePolygonToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deletePolygonToolStripMenuItem.Name = "deletePolygonToolStripMenuItem";
            this.deletePolygonToolStripMenuItem.Size = new System.Drawing.Size(179, 24);
            this.deletePolygonToolStripMenuItem.Text = "Delete Polygon";
            this.deletePolygonToolStripMenuItem.Click += new System.EventHandler(this.deletePolygonToolStripMenuItem_Click);
            // 
            // picBoxHor
            // 
            this.picBoxHor.BackColor = System.Drawing.Color.Transparent;
            this.picBoxHor.Image = ((System.Drawing.Image)(resources.GetObject("picBoxHor.Image")));
            this.picBoxHor.Location = new System.Drawing.Point(73, 40);
            this.picBoxHor.Name = "picBoxHor";
            this.picBoxHor.Size = new System.Drawing.Size(16, 16);
            this.picBoxHor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBoxHor.TabIndex = 3;
            this.picBoxHor.TabStop = false;
            this.picBoxHor.Visible = false;
            // 
            // picBoxLen
            // 
            this.picBoxLen.BackColor = System.Drawing.Color.Transparent;
            this.picBoxLen.Image = ((System.Drawing.Image)(resources.GetObject("picBoxLen.Image")));
            this.picBoxLen.Location = new System.Drawing.Point(106, 40);
            this.picBoxLen.Name = "picBoxLen";
            this.picBoxLen.Size = new System.Drawing.Size(16, 16);
            this.picBoxLen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBoxLen.TabIndex = 4;
            this.picBoxLen.TabStop = false;
            this.picBoxLen.Visible = false;
            // 
            // picBoxVer
            // 
            this.picBoxVer.BackColor = System.Drawing.Color.White;
            this.picBoxVer.Image = ((System.Drawing.Image)(resources.GetObject("picBoxVer.Image")));
            this.picBoxVer.Location = new System.Drawing.Point(36, 40);
            this.picBoxVer.Name = "picBoxVer";
            this.picBoxVer.Size = new System.Drawing.Size(16, 16);
            this.picBoxVer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBoxVer.TabIndex = 5;
            this.picBoxVer.TabStop = false;
            this.picBoxVer.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1382, 28);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // radioButtonLibAlg
            // 
            this.radioButtonLibAlg.AutoSize = true;
            this.radioButtonLibAlg.Location = new System.Drawing.Point(1212, 40);
            this.radioButtonLibAlg.Name = "radioButtonLibAlg";
            this.radioButtonLibAlg.Size = new System.Drawing.Size(136, 21);
            this.radioButtonLibAlg.TabIndex = 7;
            this.radioButtonLibAlg.TabStop = true;
            this.radioButtonLibAlg.Text = "Library Algorithm";
            this.radioButtonLibAlg.UseVisualStyleBackColor = true;
            this.radioButtonLibAlg.CheckedChanged += new System.EventHandler(this.radioButtonLibAlg_CheckedChanged);
            // 
            // radioButtonBreAlg
            // 
            this.radioButtonBreAlg.AutoSize = true;
            this.radioButtonBreAlg.Location = new System.Drawing.Point(1212, 67);
            this.radioButtonBreAlg.Name = "radioButtonBreAlg";
            this.radioButtonBreAlg.Size = new System.Drawing.Size(164, 21);
            this.radioButtonBreAlg.TabIndex = 8;
            this.radioButtonBreAlg.TabStop = true;
            this.radioButtonBreAlg.Text = "Bresenham Algorithm";
            this.radioButtonBreAlg.UseVisualStyleBackColor = true;
            this.radioButtonBreAlg.CheckedChanged += new System.EventHandler(this.radioButtonBreAlg_CheckedChanged);
            // 
            // radioButtonWuAlg
            // 
            this.radioButtonWuAlg.AutoSize = true;
            this.radioButtonWuAlg.Location = new System.Drawing.Point(1212, 94);
            this.radioButtonWuAlg.Name = "radioButtonWuAlg";
            this.radioButtonWuAlg.Size = new System.Drawing.Size(109, 21);
            this.radioButtonWuAlg.TabIndex = 9;
            this.radioButtonWuAlg.TabStop = true;
            this.radioButtonWuAlg.Text = "Wu Algoritm ";
            this.radioButtonWuAlg.UseVisualStyleBackColor = true;
            this.radioButtonWuAlg.CheckedChanged += new System.EventHandler(this.radioButtonWuAlg_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 753);
            this.Controls.Add(this.radioButtonWuAlg);
            this.Controls.Add(this.radioButtonBreAlg);
            this.Controls.Add(this.radioButtonLibAlg);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.picBoxVer);
            this.Controls.Add(this.picBoxLen);
            this.Controls.Add(this.picBoxHor);
            this.Controls.Add(this.pictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Polygon Editor";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.contextMenuVertex.ResumeLayout(false);
            this.contextMenuLineSegment.ResumeLayout(false);
            this.contextMenuPolygon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxHor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxVer)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuVertex;
        private System.Windows.Forms.ToolStripMenuItem deleteVertexToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuLineSegment;
        private System.Windows.Forms.ToolStripMenuItem addVertexToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuPolygon;
        private System.Windows.Forms.ToolStripMenuItem deletePolygonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticalRestrictionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizontalRestrictionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lengthRestricionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteRestrictionToolStripMenuItem;
        public System.Windows.Forms.PictureBox picBoxHor;
        public System.Windows.Forms.PictureBox picBoxLen;
        public System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.PictureBox picBoxVer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioButtonLibAlg;
        private System.Windows.Forms.RadioButton radioButtonBreAlg;
        private System.Windows.Forms.RadioButton radioButtonWuAlg;
    }
}

