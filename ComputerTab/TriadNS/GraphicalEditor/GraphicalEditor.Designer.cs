namespace GraphicalEditor
{
    partial class GraphicalEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphicalEditor));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dp = new DrawingPanel.DrawingPanel();
            this.lv = new System.Windows.Forms.ListView();
            this.img = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnDynamic = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAnalize = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(418, 310);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.RightToolStripPanel
            // 
            this.toolStripContainer1.RightToolStripPanel.Click += new System.EventHandler(this.toolStripContainer1_RightToolStripPanel_Click);
            this.toolStripContainer1.Size = new System.Drawing.Size(418, 335);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.TopToolStripPanel.Click += new System.EventHandler(this.toolStripContainer1_TopToolStripPanel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dp);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lv);
            this.splitContainer1.Size = new System.Drawing.Size(418, 310);
            this.splitContainer1.SplitterDistance = 257;
            this.splitContainer1.TabIndex = 1;
            // 
            // dp
            // 
            this.dp.A4 = true;
            this.dp.AllowDrop = true;
            this.dp.AutoScroll = true;
            this.dp.AutoScrollMinSize = new System.Drawing.Size(1024, 768);
            this.dp.BackColor = System.Drawing.Color.White;
            this.dp.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.dp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dp.dx = 0;
            this.dp.dy = 0;
            this.dp.gridSize = 20;
            this.dp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.dp.Location = new System.Drawing.Point(0, 0);
            this.dp.Name = "dp";
            this.dp.Size = new System.Drawing.Size(418, 257);
            this.dp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.dp.StickToGrid = false;
            this.dp.TabIndex = 0;
            this.dp.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.dp.Zoom = 1F;
            // 
            // lv
            // 
            this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv.Location = new System.Drawing.Point(0, 0);
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(418, 49);
            this.lv.TabIndex = 0;
            this.lv.UseCompatibleStateImageBehavior = false;
            // 
            // img
            // 
            this.img.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.img.ImageSize = new System.Drawing.Size(16, 16);
            this.img.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDynamic,
            this.toolStripSeparator1,
            this.btnAnalize});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(95, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // btnDynamic
            // 
            this.btnDynamic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnDynamic.Image")));
            this.btnDynamic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDynamic.Name = "btnDynamic";
            this.btnDynamic.Size = new System.Drawing.Size(23, 22);
            this.btnDynamic.Text = "Динамический объект";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAnalize
            // 
            this.btnAnalize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAnalize.Image = ((System.Drawing.Image)(resources.GetObject("btnAnalize.Image")));
            this.btnAnalize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAnalize.Name = "btnAnalize";
            this.btnAnalize.Size = new System.Drawing.Size(23, 22);
            this.btnAnalize.Text = "Средство анализа";
            // 
            // GraphicalEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "GraphicalEditor";
            this.Size = new System.Drawing.Size(418, 335);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ToolStripContainer toolStripContainer1;
        public System.Windows.Forms.SplitContainer splitContainer1;
        public DrawingPanel.DrawingPanel dp;
        public System.Windows.Forms.ListView lv;
        private System.Windows.Forms.ImageList img;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnDynamic;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnAnalize;

    }
}
