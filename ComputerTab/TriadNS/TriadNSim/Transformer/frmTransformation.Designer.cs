namespace TriadNSim.Forms
{
    partial class frmTransformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTransformation));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.leftPart = new DrawingPanel.DrawingPanel();
            this.rightPart = new DrawingPanel.DrawingPanel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lstItems = new System.Windows.Forms.ListView();
            this.MainToolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripbtnSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripbtnLink = new System.Windows.Forms.ToolStripButton();
            this.toolStripcmbZoom = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.MainToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(859, 435);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(859, 460);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.MainToolBar);
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(859, 435);
            this.splitContainer1.SplitterDistance = 301;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.leftPart);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.rightPart);
            this.splitContainer3.Size = new System.Drawing.Size(859, 301);
            this.splitContainer3.SplitterDistance = 418;
            this.splitContainer3.TabIndex = 0;
            // 
            // leftPart
            // 
            this.leftPart.A4 = true;
            this.leftPart.AllowDrop = true;
            this.leftPart.AutoScroll = true;
            this.leftPart.AutoScrollMinSize = new System.Drawing.Size(1024, 768);
            this.leftPart.BackColor = System.Drawing.Color.White;
            this.leftPart.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.leftPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPart.dx = 0;
            this.leftPart.dy = 0;
            this.leftPart.gridSize = 20;
            this.leftPart.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.leftPart.Location = new System.Drawing.Point(0, 0);
            this.leftPart.Name = "leftPart";
            this.leftPart.Size = new System.Drawing.Size(418, 301);
            this.leftPart.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.leftPart.StickToGrid = false;
            this.leftPart.TabIndex = 0;
            this.leftPart.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.leftPart.Zoom = 1F;
            // 
            // rightPart
            // 
            this.rightPart.A4 = true;
            this.rightPart.AllowDrop = true;
            this.rightPart.AutoScroll = true;
            this.rightPart.AutoScrollMinSize = new System.Drawing.Size(1024, 768);
            this.rightPart.BackColor = System.Drawing.Color.White;
            this.rightPart.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.rightPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPart.dx = 0;
            this.rightPart.dy = 0;
            this.rightPart.gridSize = 20;
            this.rightPart.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.rightPart.Location = new System.Drawing.Point(0, 0);
            this.rightPart.Name = "rightPart";
            this.rightPart.Size = new System.Drawing.Size(437, 301);
            this.rightPart.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.rightPart.StickToGrid = false;
            this.rightPart.TabIndex = 0;
            this.rightPart.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.rightPart.Zoom = 1F;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lstItems);
            this.splitContainer2.Size = new System.Drawing.Size(859, 130);
            this.splitContainer2.SplitterDistance = 286;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(286, 130);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Правила трансформации";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.Location = new System.Drawing.Point(152, 95);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(116, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отменить";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Location = new System.Drawing.Point(12, 95);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(116, 30);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 401);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(168, 32);
            this.button2.TabIndex = 3;
            this.button2.Text = "Отменить";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 363);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(168, 32);
            this.button1.TabIndex = 2;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Имя правила:";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtName.Location = new System.Drawing.Point(12, 48);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(256, 21);
            this.txtName.TabIndex = 0;
            // 
            // lstItems
            // 
            this.lstItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstItems.Location = new System.Drawing.Point(0, 0);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(569, 130);
            this.lstItems.TabIndex = 0;
            this.lstItems.UseCompatibleStateImageBehavior = false;
            // 
            // MainToolBar
            // 
            this.MainToolBar.Dock = System.Windows.Forms.DockStyle.None;
            this.MainToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripbtnSelect,
            this.toolStripButton1,
            this.toolStripbtnLink,
            this.toolStripcmbZoom});
            this.MainToolBar.Location = new System.Drawing.Point(3, 0);
            this.MainToolBar.Name = "MainToolBar";
            this.MainToolBar.Size = new System.Drawing.Size(204, 25);
            this.MainToolBar.TabIndex = 4;
            this.MainToolBar.Text = "toolStrip1";
            // 
            // toolStripbtnSelect
            // 
            this.toolStripbtnSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripbtnSelect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripbtnSelect.Image")));
            this.toolStripbtnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripbtnSelect.Name = "toolStripbtnSelect";
            this.toolStripbtnSelect.Size = new System.Drawing.Size(23, 22);
            this.toolStripbtnSelect.Text = "Выделение";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Соединить";
            // 
            // toolStripbtnLink
            // 
            this.toolStripbtnLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripbtnLink.Image = ((System.Drawing.Image)(resources.GetObject("toolStripbtnLink.Image")));
            this.toolStripbtnLink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripbtnLink.Name = "toolStripbtnLink";
            this.toolStripbtnLink.Size = new System.Drawing.Size(23, 22);
            this.toolStripbtnLink.Text = "Соединить";
            // 
            // toolStripcmbZoom
            // 
            this.toolStripcmbZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toolStripcmbZoom.Items.AddRange(new object[] {
            "25%",
            "50%",
            "75%",
            "100%",
            "150%",
            "200%",
            "500%"});
            this.toolStripcmbZoom.MergeIndex = 3;
            this.toolStripcmbZoom.Name = "toolStripcmbZoom";
            this.toolStripcmbZoom.Size = new System.Drawing.Size(121, 25);
            this.toolStripcmbZoom.ToolTipText = "Масштаб";
            // 
            // frmTransformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 460);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "frmTransformation";
            this.Text = "Определить правило";
            this.Load += new System.EventHandler(this.frmTransformation_Load);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.MainToolBar.ResumeLayout(false);
            this.MainToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip MainToolBar;
        private System.Windows.Forms.ToolStripButton toolStripbtnSelect;
        private System.Windows.Forms.ToolStripButton toolStripbtnLink;
        private System.Windows.Forms.ToolStripComboBox toolStripcmbZoom;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ListView lstItems;
        public DrawingPanel.DrawingPanel leftPart;
        public DrawingPanel.DrawingPanel rightPart;
        public System.Windows.Forms.TextBox txtName;

    }
}