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
            this.cbReserv = new System.Windows.Forms.CheckBox();
            this.cbLimit = new System.Windows.Forms.CheckBox();
            this.cbSafety = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.lbActive = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.treeView1.Size = new System.Drawing.Size(255, 386);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lbActive);
            this.groupBox1.Controls.Add(this.cbActive);
            this.groupBox1.Controls.Add(this.cbReserv);
            this.groupBox1.Controls.Add(this.cbLimit);
            this.groupBox1.Controls.Add(this.cbSafety);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(151, 386);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Свойства сетей Петри";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // cbReserv
            // 
            this.cbReserv.AutoCheck = false;
            this.cbReserv.AutoSize = true;
            this.cbReserv.Location = new System.Drawing.Point(13, 75);
            this.cbReserv.Name = "cbReserv";
            this.cbReserv.Size = new System.Drawing.Size(86, 17);
            this.cbReserv.TabIndex = 2;
            this.cbReserv.Text = "Сохранение";
            this.cbReserv.UseVisualStyleBackColor = true;
            // 
            // cbLimit
            // 
            this.cbLimit.AutoCheck = false;
            this.cbLimit.AutoSize = true;
            this.cbLimit.Location = new System.Drawing.Point(13, 52);
            this.cbLimit.Name = "cbLimit";
            this.cbLimit.Size = new System.Drawing.Size(109, 17);
            this.cbLimit.TabIndex = 1;
            this.cbLimit.Text = "Ограниченность";
            this.cbLimit.UseVisualStyleBackColor = true;
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
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(410, 386);
            this.splitContainer1.SplitterDistance = 255;
            this.splitContainer1.TabIndex = 2;
            // 
            // cbActive
            // 
            this.cbActive.AutoCheck = false;
            this.cbActive.AutoSize = true;
            this.cbActive.CausesValidation = false;
            this.cbActive.Location = new System.Drawing.Point(13, 99);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(85, 17);
            this.cbActive.TabIndex = 3;
            this.cbActive.Text = "Активность";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // lbActive
            // 
            this.lbActive.FormattingEnabled = true;
            this.lbActive.Location = new System.Drawing.Point(13, 147);
            this.lbActive.Name = "lbActive";
            this.lbActive.Size = new System.Drawing.Size(123, 121);
            this.lbActive.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Неактивные переходы:";
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
        private System.Windows.Forms.ListBox lbActive;
        private System.Windows.Forms.CheckBox cbActive;
        private System.Windows.Forms.Label label1;

    }
}