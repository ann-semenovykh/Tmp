namespace TriadNSim.Transformer
{
    partial class frmStartTransformation
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
            this.splitCustomize = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnChoose = new System.Windows.Forms.Button();
            this.btnChooseAll = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbErr = new System.Windows.Forms.CheckBox();
            this.cbDone = new System.Windows.Forms.CheckBox();
            this.cbBegin = new System.Windows.Forms.CheckBox();
            this.cbStart = new System.Windows.Forms.CheckBox();
            this.cbRules = new System.Windows.Forms.CheckBox();
            this.btnAddRule = new System.Windows.Forms.Button();
            this.txtRules = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitModelWork = new System.Windows.Forms.SplitContainer();
            this.dpModel = new DrawingPanel.DrawingPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataOut = new System.Windows.Forms.DataGridView();
            this.clmNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmErr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitCustomize)).BeginInit();
            this.splitCustomize.Panel1.SuspendLayout();
            this.splitCustomize.Panel2.SuspendLayout();
            this.splitCustomize.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitModelWork)).BeginInit();
            this.splitModelWork.Panel1.SuspendLayout();
            this.splitModelWork.Panel2.SuspendLayout();
            this.splitModelWork.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataOut)).BeginInit();
            this.SuspendLayout();
            // 
            // splitCustomize
            // 
            this.splitCustomize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCustomize.Location = new System.Drawing.Point(0, 0);
            this.splitCustomize.Name = "splitCustomize";
            // 
            // splitCustomize.Panel1
            // 
            this.splitCustomize.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitCustomize.Panel2
            // 
            this.splitCustomize.Panel2.Controls.Add(this.splitModelWork);
            this.splitCustomize.Size = new System.Drawing.Size(768, 433);
            this.splitCustomize.SplitterDistance = 218;
            this.splitCustomize.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.btnChoose);
            this.groupBox2.Controls.Add(this.btnChooseAll);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.btnAddRule);
            this.groupBox2.Controls.Add(this.txtRules);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(218, 433);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Задать трансформацию";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(59, 398);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(101, 23);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnChoose
            // 
            this.btnChoose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChoose.Location = new System.Drawing.Point(20, 164);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(182, 23);
            this.btnChoose.TabIndex = 6;
            this.btnChoose.Text = "Выбрать стартовый граф";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // btnChooseAll
            // 
            this.btnChooseAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseAll.Location = new System.Drawing.Point(33, 135);
            this.btnChooseAll.Name = "btnChooseAll";
            this.btnChooseAll.Size = new System.Drawing.Size(149, 23);
            this.btnChooseAll.TabIndex = 5;
            this.btnChooseAll.Text = "Выбрать весь граф";
            this.btnChooseAll.UseVisualStyleBackColor = true;
            this.btnChooseAll.Visible = false;
            this.btnChooseAll.Click += new System.EventHandler(this.btnChooseAll_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.cbErr);
            this.groupBox3.Controls.Add(this.cbDone);
            this.groupBox3.Controls.Add(this.cbBegin);
            this.groupBox3.Controls.Add(this.cbStart);
            this.groupBox3.Controls.Add(this.cbRules);
            this.groupBox3.Location = new System.Drawing.Point(12, 289);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 103);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ход трансформации";
            // 
            // cbErr
            // 
            this.cbErr.AutoCheck = false;
            this.cbErr.AutoSize = true;
            this.cbErr.Location = new System.Drawing.Point(7, 86);
            this.cbErr.Name = "cbErr";
            this.cbErr.Size = new System.Drawing.Size(174, 17);
            this.cbErr.TabIndex = 4;
            this.cbErr.Text = "Вывод выполнен без ошибок";
            this.cbErr.UseVisualStyleBackColor = true;
            // 
            // cbDone
            // 
            this.cbDone.AutoCheck = false;
            this.cbDone.AutoSize = true;
            this.cbDone.Location = new System.Drawing.Point(7, 68);
            this.cbDone.Name = "cbDone";
            this.cbDone.Size = new System.Drawing.Size(167, 17);
            this.cbDone.TabIndex = 3;
            this.cbDone.Text = "Целевая модель построена";
            this.cbDone.UseVisualStyleBackColor = true;
            this.cbDone.CheckedChanged += new System.EventHandler(this.cbRules_CheckedChanged);
            // 
            // cbBegin
            // 
            this.cbBegin.AutoCheck = false;
            this.cbBegin.AutoSize = true;
            this.cbBegin.Location = new System.Drawing.Point(7, 50);
            this.cbBegin.Name = "cbBegin";
            this.cbBegin.Size = new System.Drawing.Size(146, 17);
            this.cbBegin.TabIndex = 2;
            this.cbBegin.Text = "Трансформация начата";
            this.cbBegin.UseVisualStyleBackColor = true;
            this.cbBegin.CheckedChanged += new System.EventHandler(this.cbRules_CheckedChanged);
            // 
            // cbStart
            // 
            this.cbStart.AutoCheck = false;
            this.cbStart.AutoSize = true;
            this.cbStart.Location = new System.Drawing.Point(7, 33);
            this.cbStart.Name = "cbStart";
            this.cbStart.Size = new System.Drawing.Size(142, 17);
            this.cbStart.TabIndex = 1;
            this.cbStart.Text = "Задан стартовый граф";
            this.cbStart.UseVisualStyleBackColor = true;
            this.cbStart.CheckedChanged += new System.EventHandler(this.cbRules_CheckedChanged);
            // 
            // cbRules
            // 
            this.cbRules.AutoCheck = false;
            this.cbRules.AutoSize = true;
            this.cbRules.Location = new System.Drawing.Point(7, 16);
            this.cbRules.Name = "cbRules";
            this.cbRules.Size = new System.Drawing.Size(111, 17);
            this.cbRules.TabIndex = 0;
            this.cbRules.Text = "Правила заданы";
            this.cbRules.UseVisualStyleBackColor = true;
            this.cbRules.CheckedChanged += new System.EventHandler(this.cbRules_CheckedChanged);
            // 
            // btnAddRule
            // 
            this.btnAddRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRule.Location = new System.Drawing.Point(188, 36);
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Size = new System.Drawing.Size(24, 20);
            this.btnAddRule.TabIndex = 2;
            this.btnAddRule.Text = "...";
            this.btnAddRule.UseVisualStyleBackColor = true;
            this.btnAddRule.Click += new System.EventHandler(this.btnAddRule_Click);
            // 
            // txtRules
            // 
            this.txtRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRules.Enabled = false;
            this.txtRules.Location = new System.Drawing.Point(12, 36);
            this.txtRules.Name = "txtRules";
            this.txtRules.Size = new System.Drawing.Size(170, 20);
            this.txtRules.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выбрать правила";
            // 
            // splitModelWork
            // 
            this.splitModelWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitModelWork.Location = new System.Drawing.Point(0, 0);
            this.splitModelWork.Name = "splitModelWork";
            this.splitModelWork.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitModelWork.Panel1
            // 
            this.splitModelWork.Panel1.Controls.Add(this.dpModel);
            // 
            // splitModelWork.Panel2
            // 
            this.splitModelWork.Panel2.Controls.Add(this.groupBox1);
            this.splitModelWork.Size = new System.Drawing.Size(546, 433);
            this.splitModelWork.SplitterDistance = 328;
            this.splitModelWork.TabIndex = 0;
            // 
            // dpModel
            // 
            this.dpModel.A4 = true;
            this.dpModel.AllowDrop = true;
            this.dpModel.AutoScroll = true;
            this.dpModel.AutoScrollMinSize = new System.Drawing.Size(1024, 768);
            this.dpModel.BackColor = System.Drawing.Color.White;
            this.dpModel.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.dpModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dpModel.dx = 0;
            this.dpModel.dy = 0;
            this.dpModel.gridSize = 20;
            this.dpModel.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.dpModel.Location = new System.Drawing.Point(0, 0);
            this.dpModel.Name = "dpModel";
            this.dpModel.Size = new System.Drawing.Size(546, 328);
            this.dpModel.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.dpModel.StickToGrid = false;
            this.dpModel.TabIndex = 0;
            this.dpModel.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.dpModel.Zoom = 1F;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataOut);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(551, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Список ошибок";
            // 
            // dataOut
            // 
            this.dataOut.AllowUserToAddRows = false;
            this.dataOut.AllowUserToDeleteRows = false;
            this.dataOut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataOut.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmNum,
            this.clmErr});
            this.dataOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataOut.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataOut.Location = new System.Drawing.Point(3, 16);
            this.dataOut.Name = "dataOut";
            this.dataOut.ReadOnly = true;
            this.dataOut.Size = new System.Drawing.Size(545, 69);
            this.dataOut.TabIndex = 0;
            // 
            // clmNum
            // 
            this.clmNum.HeaderText = "№";
            this.clmNum.Name = "clmNum";
            this.clmNum.ReadOnly = true;
            this.clmNum.Width = 50;
            // 
            // clmErr
            // 
            this.clmErr.HeaderText = "Ошибка";
            this.clmErr.Name = "clmErr";
            this.clmErr.ReadOnly = true;
            this.clmErr.Width = 300;
            // 
            // frmStartTransformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 433);
            this.Controls.Add(this.splitCustomize);
            this.Name = "frmStartTransformation";
            this.Text = "Трансформация моделей";
            this.Load += new System.EventHandler(this.frmStartTransformation_Load);
            this.splitCustomize.Panel1.ResumeLayout(false);
            this.splitCustomize.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCustomize)).EndInit();
            this.splitCustomize.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.splitModelWork.Panel1.ResumeLayout(false);
            this.splitModelWork.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitModelWork)).EndInit();
            this.splitModelWork.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataOut)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitCustomize;
        private System.Windows.Forms.SplitContainer splitModelWork;
        private DrawingPanel.DrawingPanel dpModel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmErr;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAddRule;
        private System.Windows.Forms.TextBox txtRules;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbDone;
        private System.Windows.Forms.CheckBox cbBegin;
        private System.Windows.Forms.CheckBox cbStart;
        private System.Windows.Forms.CheckBox cbRules;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.Button btnChooseAll;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox cbErr;
    }
}