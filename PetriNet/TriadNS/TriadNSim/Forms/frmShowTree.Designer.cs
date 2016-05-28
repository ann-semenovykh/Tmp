namespace TriadNSim.Forms
{
    partial class frmShowTree
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cbSafety = new System.Windows.Forms.CheckBox();
            this.cbLimit = new System.Windows.Forms.CheckBox();
            this.cbReserv = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(410, 285);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbReserv);
            this.groupBox1.Controls.Add(this.cbLimit);
            this.groupBox1.Controls.Add(this.cbSafety);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(410, 97);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Свойства сетей Петри";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
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
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(410, 386);
            this.splitContainer1.SplitterDistance = 285;
            this.splitContainer1.TabIndex = 2;
            // 
            // cbSafety
            // 
            this.cbSafety.AutoCheck = false;
            this.cbSafety.AutoSize = true;
            this.cbSafety.Location = new System.Drawing.Point(13, 29);
            this.cbSafety.Name = "cbSafety";
            this.cbSafety.Size = new System.Drawing.Size(98, 17);
            this.cbSafety.TabIndex = 0;
            this.cbSafety.Text = "Безопасность";
            this.cbSafety.UseVisualStyleBackColor = true;
            // 
            // cbLimit
            // 
            this.cbLimit.AutoCheck = false;
            this.cbLimit.AutoSize = true;
            this.cbLimit.Location = new System.Drawing.Point(139, 29);
            this.cbLimit.Name = "cbLimit";
            this.cbLimit.Size = new System.Drawing.Size(109, 17);
            this.cbLimit.TabIndex = 1;
            this.cbLimit.Text = "Ограниченность";
            this.cbLimit.UseVisualStyleBackColor = true;
            // 
            // cbReserv
            // 
            this.cbReserv.AutoCheck = false;
            this.cbReserv.AutoSize = true;
            this.cbReserv.Location = new System.Drawing.Point(279, 29);
            this.cbReserv.Name = "cbReserv";
            this.cbReserv.Size = new System.Drawing.Size(86, 17);
            this.cbReserv.TabIndex = 2;
            this.cbReserv.Text = "Сохранение";
            this.cbReserv.UseVisualStyleBackColor = true;
            // 
            // frmShowTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 386);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmShowTree";
            this.Text = "Дерево достижимости";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmShowTree_FormClosing);
            this.Load += new System.EventHandler(this.frmShowTree_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox cbReserv;
        private System.Windows.Forms.CheckBox cbLimit;
        private System.Windows.Forms.CheckBox cbSafety;

    }
}