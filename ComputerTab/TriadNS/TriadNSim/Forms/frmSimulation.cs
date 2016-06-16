using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using drawingPanel=DrawingPanel.DrawingPanel;
using System.Reflection;
using computerModel = ComputerModel.ComputerModel;
using petriNetModel = PetriNetModel.PetriNetModel;
using graphicalEditor = GraphicalEditor.GraphicalEditor;
using TriadNSim.Transformer;

namespace TriadNSim.Forms
{
    public partial class frmSimulation : Form
    {

        public frmSimulation()
        {
            InitializeComponent();
            ModelBrowser.Nodes.Add("root", "Модели");

        }
        public int GetEndModelTime()
        {
            int iResult;
            try
            {
                iResult = Int32.Parse(tstModelTime.Text);
            }
            catch
            {
                tstModelTime.Text = "100";
                iResult = 100;
            }
            return iResult;
        }
        private void toolStripbtnRun_ButtonClick(object sender, EventArgs e)
        {
            Run();
        }
        private void Run(bool bSelectSimCondition = false)
        {
            //m_oSimulation.Start(true);
            Model md = (Model)tabModels.SelectedTab.Tag;
            md.GetEndModelTime = GetEndModelTime();
            md.Run(GetEndModelTime());
            
        }
        private void tsbCalcStaticProp_Click(object sender, EventArgs e)
        {
            Model md = (Model)tabModels.SelectedTab.Tag;
            md.Calc();
        }

        private void tabControl1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            
        }
        
        public void newmodel(string model)
        {
            frmChoseModel frm = new frmChoseModel(new COWLOntologyManager("Ontologies\\Petri.owl"));
            if (model!=null||tabModels.SelectedTab.Name == "AddPage" && ((frm.ShowDialog()) == DialogResult.OK))
            {
                string title = "Модель " + (tabModels.TabCount-1).ToString();
                TabPage myTabPage = new TabPage(title);
                GraphicalEditor.GraphicalEditor editor = new GraphicalEditor.GraphicalEditor(); 
                
                object ob = null;
                string mod = model == null ? frm.Tag.ToString() : model;
                switch (mod)
                {
                    case "ComputerModel":
                        ob = new computerModel(myTabPage, editor);
                        break;
                    case "PetriNet":
                        ob = new petriNetModel(myTabPage, editor);
                        break;
                    default:
                        ob = new Model(myTabPage,editor);
                        break;
                }
                myTabPage.Tag = ob;

                tabModels.TabPages.Insert(tabModels.TabCount - 1, myTabPage);
                tabModels.SelectedTab = tabModels.TabPages[tabModels.TabCount - 2];
                ModelBrowser.Nodes[0].Nodes.Add(title, title);
            }
            
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            newmodel(null);
        }

        private void tabControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Delete)
               // tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }

        private void toolStripbtnLink_Click(object sender, EventArgs e)
        {
           // for (int i = 0; i < tabControl1.TabCount; i++)
            int i = tabModels.SelectedIndex;
            {
                graphicalEditor gp = (graphicalEditor)tabModels.SelectedTab.Controls["graphicalEditor"];
                drawingPanel dp = gp.dp;
                dp.CurrentTool = DrawingPanel.ToolType.ttLine;
            }
            toolStripbtnSelect.Checked = false;
            toolStripbtnLink.Checked = true;
        }

        private void toolStripbtnSelect_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < tabControl1.TabCount-1; i++)
            {
                //drawingPanel dp = (drawingPanel)tabControl1.TabPages[i].Controls["drawingPanel"];
                graphicalEditor gp = (graphicalEditor)tabModels.SelectedTab.Controls["graphicalEditor"];
                gp.dp.CurrentTool = DrawingPanel.ToolType.ttSelect;
            }
            toolStripbtnSelect.Checked = true;
            toolStripbtnLink.Checked = false;
        }

        public void Save(string fileName)
        {
            graphicalEditor gp = (graphicalEditor)tabModels.SelectedTab.Controls["graphicalEditor"];
            string sNewFileName = gp.dp.Saver(fileName);
            if (sNewFileName != string.Empty)
            {
                Model md = (Model)tabModels.SelectedTab.Tag;
                md.m_sFileName = sNewFileName;
                ModelBrowser.Nodes.Find(tabModels.SelectedTab.Text, true)[0].Text =System.IO.Path.GetFileName(sNewFileName);
                tabModels.SelectedTab.Text = System.IO.Path.GetFileName(sNewFileName);
            }

        }
        private void toolStripbtnSave_Click(object sender, EventArgs e)
        {
            Model md = (Model)tabModels.SelectedTab.Tag;
            Save(md.m_sFileName);
        }

        public void Open()
        {
            graphicalEditor gp = (graphicalEditor)tabModels.SelectedTab.Controls["graphicalEditor"];
            string sNewFileName = gp.dp.Loader();
            if (sNewFileName != string.Empty)
            {
                Model md = (Model)tabModels.SelectedTab.Tag;
                md.m_sFileName = sNewFileName;
               // this.Text = Util.ApplicationName + " [" + System.IO.Path.GetFileName(this.m_sFileName) + "]";
                tabModels.SelectedTab.Text =  System.IO.Path.GetFileName(md.m_sFileName);
                

            }
        }
        private void toolStripbtnOpen_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void toolStripbtnNew_Click(object sender, EventArgs e)
        {

        }

        public void UpdateZoom()
        {
            graphicalEditor gp = (graphicalEditor)tabModels.SelectedTab.Controls["graphicalEditor"];
                
            try
            {
                string strZoom = toolStripcmbZoom.Text.Trim();
                if (strZoom.EndsWith("%")) strZoom = strZoom.Substring(0, strZoom.Length - 1);
                int value = Int32.Parse(strZoom);
                gp.dp.Zoom=value / 100.0f;
            }
            catch
            {
                toolStripcmbZoom.Text = Convert.ToString(gp.dp.Zoom * 100) + "%";
            }
        }
        private void toolStripcmbZoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                UpdateZoom();
        }

        private void MainMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripcmbZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateZoom();
        }
        
        private void tabControl1_DoubleClick(object sender, EventArgs e)
        {
            newmodel(null);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            new frmIProcedures().ShowDialog();
        }

        private void frmSimulation_Load(object sender, EventArgs e)
        {
            string title = "Новая модель";
            TabPage myTabPage = new TabPage(title);
            GraphicalEditor.GraphicalEditor editor = new GraphicalEditor.GraphicalEditor();
            object ob = null;

            ob = new ComputerModel.ComputerModel(myTabPage, editor);
            myTabPage.Tag = ob;

            tabModels.TabPages.Insert(0, myTabPage);
            tabModels.SelectedTab = tabModels.TabPages[0];
            ModelBrowser.Nodes[0].Nodes.Add("Новая модель", "Новая модель");

            toolStripcmbZoom.SelectedText = "100%";
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Run(true);
        }

        private void btnDefine_Click(object sender, EventArgs e)
        {
            Model md = (Model)tabModels.SelectedTab.Tag;
            md.GetEndModelTime = GetEndModelTime();
            md.Define(GetEndModelTime());
        
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Model md = (Model)tabModels.SelectedTab.Tag;
            md.Reset();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            frmIConditions frm = new frmIConditions();
            frm.ShowDialog();
        }

        

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void miModel_Click(object sender, EventArgs e)
        {

        }

        private void tRANSFORMATIONToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void miSaveAs_Click(object sender, EventArgs e)
        {

        }

        private void miSave_Click(object sender, EventArgs e)
        {

        }

        private void makeRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRules rules = new frmRules();
            rules.ShowDialog();
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bYRULESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStartTransformation frm = new frmStartTransformation(this);
            frm.ShowDialog();
        }
        
    }
}
