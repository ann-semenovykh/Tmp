namespace TriadNSim.Forms
{
    partial class frmSimulation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSimulation));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ModelBrowser = new System.Windows.Forms.TreeView();
            this.tabModels = new System.Windows.Forms.TabControl();
            this.AddPage = new System.Windows.Forms.TabPage();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.miSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miModel = new System.Windows.Forms.ToolStripMenuItem();
            this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tRANSFORMATIONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bYRULESToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.MainToolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripcmbZoom = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tstModelTime = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.miNew = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.miSave = new System.Windows.Forms.ToolStripMenuItem();
            this.miPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.miPrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripbtnNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripbtnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripbtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripbtnSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripbtnLink = new System.Windows.Forms.ToolStripButton();
            this.tsbCalcStaticProp = new System.Windows.Forms.ToolStripButton();
            this.btnDefine = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripbtnRun = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabModels.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.MainToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(775, 364);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(775, 413);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.MainMenu);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.MainToolBar);
            this.toolStripContainer1.TopToolStripPanel.Click += new System.EventHandler(this.toolStripContainer1_TopToolStripPanel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.tabModels);
            this.splitContainer1.Size = new System.Drawing.Size(775, 364);
            this.splitContainer1.SplitterDistance = 155;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ModelBrowser);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(155, 364);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Браузер моделей";
            // 
            // ModelBrowser
            // 
            this.ModelBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ModelBrowser.Location = new System.Drawing.Point(3, 16);
            this.ModelBrowser.Name = "ModelBrowser";
            this.ModelBrowser.Size = new System.Drawing.Size(149, 345);
            this.ModelBrowser.TabIndex = 1;
            // 
            // tabModels
            // 
            this.tabModels.Controls.Add(this.AddPage);
            this.tabModels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabModels.Location = new System.Drawing.Point(0, 0);
            this.tabModels.Name = "tabModels";
            this.tabModels.SelectedIndex = 0;
            this.tabModels.Size = new System.Drawing.Size(616, 364);
            this.tabModels.TabIndex = 0;
            this.tabModels.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabModels.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            this.tabModels.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.tabControl1_ControlAdded);
            this.tabModels.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabControl1_KeyDown);
            this.tabModels.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tabControl1_KeyPress);
            // 
            // AddPage
            // 
            this.AddPage.Location = new System.Drawing.Point(4, 22);
            this.AddPage.Name = "AddPage";
            this.AddPage.Padding = new System.Windows.Forms.Padding(3);
            this.AddPage.Size = new System.Drawing.Size(608, 338);
            this.AddPage.TabIndex = 1;
            this.AddPage.Text = "  *";
            this.AddPage.UseVisualStyleBackColor = true;
            // 
            // MainMenu
            // 
            this.MainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miEdit,
            this.miModel,
            this.tRANSFORMATIONToolStripMenuItem,
            this.miHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(775, 24);
            this.MainMenu.TabIndex = 5;
            this.MainMenu.Text = "MainMenu";
            this.MainMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MainMenu_ItemClicked);
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNew,
            this.miOpen,
            this.toolStripSeparator,
            this.miSave,
            this.miSaveAs,
            this.toolStripSeparator3,
            this.miPrint,
            this.miPrintPreview,
            this.toolStripSeparator4,
            this.miExit});
            this.miFile.Name = "miFile";
            this.miFile.Size = new System.Drawing.Size(48, 20);
            this.miFile.Text = "&Файл";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(230, 6);
            // 
            // miSaveAs
            // 
            this.miSaveAs.Name = "miSaveAs";
            this.miSaveAs.Size = new System.Drawing.Size(233, 22);
            this.miSaveAs.Text = "Сохранить &как...";
            this.miSaveAs.Click += new System.EventHandler(this.miSaveAs_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(230, 6);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(230, 6);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(233, 22);
            this.miExit.Text = "&Выход";
            // 
            // miEdit
            // 
            this.miEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator5,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator6,
            this.selectAllToolStripMenuItem});
            this.miEdit.Name = "miEdit";
            this.miEdit.Size = new System.Drawing.Size(59, 20);
            this.miEdit.Text = "&Правка";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.undoToolStripMenuItem.Text = "&Отменить";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.redoToolStripMenuItem.Text = "&Повторить";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(178, 6);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(178, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.selectAllToolStripMenuItem.Text = "Вы&делить все";
            // 
            // miModel
            // 
            this.miModel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customizeToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.miModel.Name = "miModel";
            this.miModel.Size = new System.Drawing.Size(62, 20);
            this.miModel.Text = "&Модель";
            this.miModel.Click += new System.EventHandler(this.miModel_Click);
            // 
            // customizeToolStripMenuItem
            // 
            this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            this.customizeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.customizeToolStripMenuItem.Text = "&Customize";
            this.customizeToolStripMenuItem.Click += new System.EventHandler(this.customizeToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // tRANSFORMATIONToolStripMenuItem
            // 
            this.tRANSFORMATIONToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bYRULESToolStripMenuItem,
            this.makeRulesToolStripMenuItem});
            this.tRANSFORMATIONToolStripMenuItem.Name = "tRANSFORMATIONToolStripMenuItem";
            this.tRANSFORMATIONToolStripMenuItem.Size = new System.Drawing.Size(110, 20);
            this.tRANSFORMATIONToolStripMenuItem.Text = "&Трансформация";
            this.tRANSFORMATIONToolStripMenuItem.Click += new System.EventHandler(this.tRANSFORMATIONToolStripMenuItem_Click);
            // 
            // bYRULESToolStripMenuItem
            // 
            this.bYRULESToolStripMenuItem.Name = "bYRULESToolStripMenuItem";
            this.bYRULESToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.bYRULESToolStripMenuItem.Text = "By Rules";
            this.bYRULESToolStripMenuItem.Click += new System.EventHandler(this.bYRULESToolStripMenuItem_Click);
            // 
            // makeRulesToolStripMenuItem
            // 
            this.makeRulesToolStripMenuItem.Name = "makeRulesToolStripMenuItem";
            this.makeRulesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.makeRulesToolStripMenuItem.Text = "Make Rules";
            this.makeRulesToolStripMenuItem.Click += new System.EventHandler(this.makeRulesToolStripMenuItem_Click);
            // 
            // miHelp
            // 
            this.miHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAbout});
            this.miHelp.Name = "miHelp";
            this.miHelp.Size = new System.Drawing.Size(68, 20);
            this.miHelp.Text = "&Помощь";
            // 
            // miAbout
            // 
            this.miAbout.Name = "miAbout";
            this.miAbout.Size = new System.Drawing.Size(158, 22);
            this.miAbout.Text = "&О программе...";
            // 
            // MainToolBar
            // 
            this.MainToolBar.Dock = System.Windows.Forms.DockStyle.None;
            this.MainToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripbtnNew,
            this.toolStripbtnOpen,
            this.toolStripbtnSave,
            this.toolStripSeparator1,
            this.toolStripbtnSelect,
            this.toolStripbtnLink,
            this.toolStripcmbZoom,
            this.toolStripSeparator2,
            this.tsbCalcStaticProp,
            this.btnDefine,
            this.toolStripButton2,
            this.tstModelTime,
            this.toolStripbtnRun,
            this.toolStripSeparator8,
            this.toolStripButton1,
            this.toolStripButton3});
            this.MainToolBar.Location = new System.Drawing.Point(3, 24);
            this.MainToolBar.Name = "MainToolBar";
            this.MainToolBar.Size = new System.Drawing.Size(467, 25);
            this.MainToolBar.TabIndex = 2;
            this.MainToolBar.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            this.toolStripcmbZoom.SelectedIndexChanged += new System.EventHandler(this.toolStripcmbZoom_SelectedIndexChanged);
            this.toolStripcmbZoom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripcmbZoom_KeyDown);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tstModelTime
            // 
            this.tstModelTime.Name = "tstModelTime";
            this.tstModelTime.Size = new System.Drawing.Size(50, 25);
            this.tstModelTime.Text = "100";
            this.tstModelTime.ToolTipText = "Конечное время моделирования";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // miNew
            // 
            this.miNew.Image = ((System.Drawing.Image)(resources.GetObject("miNew.Image")));
            this.miNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.miNew.Name = "miNew";
            this.miNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.miNew.Size = new System.Drawing.Size(233, 22);
            this.miNew.Text = "&Новый";
            // 
            // miOpen
            // 
            this.miOpen.Image = ((System.Drawing.Image)(resources.GetObject("miOpen.Image")));
            this.miOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.miOpen.Name = "miOpen";
            this.miOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.miOpen.Size = new System.Drawing.Size(233, 22);
            this.miOpen.Text = "&Открыть";
            // 
            // miSave
            // 
            this.miSave.Image = ((System.Drawing.Image)(resources.GetObject("miSave.Image")));
            this.miSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.miSave.Name = "miSave";
            this.miSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.miSave.Size = new System.Drawing.Size(233, 22);
            this.miSave.Text = "&Сохранить";
            this.miSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // miPrint
            // 
            this.miPrint.Image = ((System.Drawing.Image)(resources.GetObject("miPrint.Image")));
            this.miPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.miPrint.Name = "miPrint";
            this.miPrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.miPrint.Size = new System.Drawing.Size(233, 22);
            this.miPrint.Text = "&Печать";
            // 
            // miPrintPreview
            // 
            this.miPrintPreview.Image = ((System.Drawing.Image)(resources.GetObject("miPrintPreview.Image")));
            this.miPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.miPrintPreview.Name = "miPrintPreview";
            this.miPrintPreview.Size = new System.Drawing.Size(233, 22);
            this.miPrintPreview.Text = "П&редварительный просмотр";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.cutToolStripMenuItem.Text = "&Вырезать";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.copyToolStripMenuItem.Text = "&Копировать";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.pasteToolStripMenuItem.Text = "В&ставить";
            // 
            // toolStripbtnNew
            // 
            this.toolStripbtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripbtnNew.Image = ((System.Drawing.Image)(resources.GetObject("toolStripbtnNew.Image")));
            this.toolStripbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripbtnNew.Name = "toolStripbtnNew";
            this.toolStripbtnNew.Size = new System.Drawing.Size(23, 22);
            this.toolStripbtnNew.Text = "Создать";
            this.toolStripbtnNew.Click += new System.EventHandler(this.toolStripbtnNew_Click);
            // 
            // toolStripbtnOpen
            // 
            this.toolStripbtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripbtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripbtnOpen.Image")));
            this.toolStripbtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripbtnOpen.Name = "toolStripbtnOpen";
            this.toolStripbtnOpen.Size = new System.Drawing.Size(23, 22);
            this.toolStripbtnOpen.Text = "Открыть";
            this.toolStripbtnOpen.Click += new System.EventHandler(this.toolStripbtnOpen_Click);
            // 
            // toolStripbtnSave
            // 
            this.toolStripbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripbtnSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripbtnSave.Image")));
            this.toolStripbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripbtnSave.Name = "toolStripbtnSave";
            this.toolStripbtnSave.Size = new System.Drawing.Size(23, 22);
            this.toolStripbtnSave.Text = "Сохранить";
            this.toolStripbtnSave.Click += new System.EventHandler(this.toolStripbtnSave_Click);
            // 
            // toolStripbtnSelect
            // 
            this.toolStripbtnSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripbtnSelect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripbtnSelect.Image")));
            this.toolStripbtnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripbtnSelect.Name = "toolStripbtnSelect";
            this.toolStripbtnSelect.Size = new System.Drawing.Size(23, 22);
            this.toolStripbtnSelect.Text = "Выделение";
            this.toolStripbtnSelect.Click += new System.EventHandler(this.toolStripbtnSelect_Click);
            // 
            // toolStripbtnLink
            // 
            this.toolStripbtnLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripbtnLink.Image = ((System.Drawing.Image)(resources.GetObject("toolStripbtnLink.Image")));
            this.toolStripbtnLink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripbtnLink.Name = "toolStripbtnLink";
            this.toolStripbtnLink.Size = new System.Drawing.Size(23, 22);
            this.toolStripbtnLink.Text = "Соединить";
            this.toolStripbtnLink.Click += new System.EventHandler(this.toolStripbtnLink_Click);
            // 
            // tsbCalcStaticProp
            // 
            this.tsbCalcStaticProp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCalcStaticProp.Image = ((System.Drawing.Image)(resources.GetObject("tsbCalcStaticProp.Image")));
            this.tsbCalcStaticProp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCalcStaticProp.Name = "tsbCalcStaticProp";
            this.tsbCalcStaticProp.Size = new System.Drawing.Size(23, 22);
            this.tsbCalcStaticProp.Text = "Вычислить статические характеристики";
            this.tsbCalcStaticProp.Click += new System.EventHandler(this.tsbCalcStaticProp_Click);
            // 
            // btnDefine
            // 
            this.btnDefine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDefine.Image = global::TriadNSim.Properties.Resources.define;
            this.btnDefine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDefine.Name = "btnDefine";
            this.btnDefine.Size = new System.Drawing.Size(23, 22);
            this.btnDefine.Text = "Доопределить";
            this.btnDefine.Click += new System.EventHandler(this.btnDefine_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::TriadNSim.Properties.Resources.reset;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Сбросить поведение";
            this.toolStripButton2.ToolTipText = "Сбросить поведение всех элементов";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripbtnRun
            // 
            this.toolStripbtnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripbtnRun.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.toolStripbtnRun.Image = ((System.Drawing.Image)(resources.GetObject("toolStripbtnRun.Image")));
            this.toolStripbtnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripbtnRun.Name = "toolStripbtnRun";
            this.toolStripbtnRun.Size = new System.Drawing.Size(32, 22);
            this.toolStripbtnRun.Text = "Моделировать";
            this.toolStripbtnRun.ButtonClick += new System.EventHandler(this.toolStripbtnRun_ButtonClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItem1.Text = "Выбрать условия моделирования";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::TriadNSim.Properties.Resources.Spy;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Информационные процедуры";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::TriadNSim.Properties.Resources.simc;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Условия моделироавния";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // frmSimulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 413);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "frmSimulation";
            this.Text = "TriadNS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSimulation_Load);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabModels.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.MainToolBar.ResumeLayout(false);
            this.MainToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem miFile;
        private System.Windows.Forms.ToolStripMenuItem miNew;
        private System.Windows.Forms.ToolStripMenuItem miOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem miSave;
        private System.Windows.Forms.ToolStripMenuItem miSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem miPrint;
        private System.Windows.Forms.ToolStripMenuItem miPrintPreview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem miEdit;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miModel;
        private System.Windows.Forms.ToolStripMenuItem customizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miHelp;
        private System.Windows.Forms.ToolStripMenuItem miAbout;
        private System.Windows.Forms.ToolStrip MainToolBar;
        private System.Windows.Forms.ToolStripButton toolStripbtnNew;
        private System.Windows.Forms.ToolStripButton toolStripbtnOpen;
        private System.Windows.Forms.ToolStripButton toolStripbtnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripbtnSelect;
        private System.Windows.Forms.ToolStripButton toolStripbtnLink;
        private System.Windows.Forms.ToolStripComboBox toolStripcmbZoom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbCalcStaticProp;
        private System.Windows.Forms.ToolStripButton btnDefine;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripTextBox tstModelTime;
        private System.Windows.Forms.ToolStripSplitButton toolStripbtnRun;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage AddPage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView ModelBrowser;
        private System.Windows.Forms.ToolStripMenuItem tRANSFORMATIONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bYRULESToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeRulesToolStripMenuItem;
        public System.Windows.Forms.TabControl tabModels;

    }
}

