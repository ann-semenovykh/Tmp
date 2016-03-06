﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using drawingPanel=DrawingPanel.DrawingPanel;

namespace TriadNSim.Forms
{
    public partial class frmSimulation : Form
    {

        private string m_sFileName = string.Empty;
        public frmSimulation()
        {
            InitializeComponent();

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
            Model md = (Model)tabControl1.SelectedTab.Tag;
            md.Run(GetEndModelTime());
            
        }
        private void tsbCalcStaticProp_Click(object sender, EventArgs e)
        {
            Model md = (Model)tabControl1.SelectedTab.Tag;
            md.Calc();
        }

        private void tabControl1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "AddPage")
            {
                string title = "TabPage " + (tabControl1.TabCount).ToString();
                TabPage myTabPage = new TabPage(title);
                ListView lv = new ListView();
                lv.Dock = DockStyle.Bottom;

                drawingPanel dp = new drawingPanel();
                dp.Dock = DockStyle.Fill;
                myTabPage.Controls.Add(dp);
                myTabPage.Controls.Add(lv);
                Model ob = new Model(myTabPage,lv,dp);
                myTabPage.Tag = ob;
                
                tabControl1.TabPages.Insert(tabControl1.TabCount - 1, myTabPage);
                tabControl1.SelectedTab = tabControl1.TabPages[tabControl1.TabCount - 2];

            }

        }

        private void tabControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }

        private void toolStripbtnLink_Click(object sender, EventArgs e)
        {
           // for (int i = 0; i < tabControl1.TabCount; i++)
            int i = tabControl1.SelectedIndex;
            {
                drawingPanel dp = (drawingPanel)tabControl1.TabPages[i].Controls["drawingPanel"];
                dp.CurrentTool = DrawingPanel.ToolType.ttLine;
            }
            toolStripbtnSelect.Checked = false;
            toolStripbtnLink.Checked = true;
        }

        private void toolStripbtnSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                drawingPanel dp = (drawingPanel)tabControl1.TabPages[i].Controls["drawingPanel"];
                dp.CurrentTool = DrawingPanel.ToolType.ttSelect;
            }
            toolStripbtnSelect.Checked = true;
            toolStripbtnLink.Checked = false;
        }

        public void Save(string fileName)
        {
            drawingPanel dp = (drawingPanel)tabControl1.SelectedTab.Controls["drawingPanel"];
            string sNewFileName = dp.Saver(fileName);
            if (sNewFileName != string.Empty)
            {
                this.m_sFileName = sNewFileName;
                this.Text = this.m_sFileName;
            }
        }
        private void toolStripbtnSave_Click(object sender, EventArgs e)
        {
            Save(m_sFileName);
        }

        public void Open()
        {
            drawingPanel dp = (drawingPanel)tabControl1.SelectedTab.Controls["drawingPanel"];
            string sNewFileName = dp.Loader();
            if (sNewFileName != string.Empty)
            {
                this.m_sFileName = sNewFileName;
               // this.Text = Util.ApplicationName + " [" + System.IO.Path.GetFileName(this.m_sFileName) + "]";
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
            drawingPanel dp = (drawingPanel)tabControl1.SelectedTab.Controls["drawingPanel"];
                
            try
            {
                string strZoom = toolStripcmbZoom.Text.Trim();
                if (strZoom.EndsWith("%")) strZoom = strZoom.Substring(0, strZoom.Length - 1);
                int value = Int32.Parse(strZoom);
                dp.Zoom=value / 100.0f;
            }
            catch
            {
                toolStripcmbZoom.Text = Convert.ToString(dp.Zoom * 100) + "%";
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
            if (tabControl1.SelectedTab.Name == "AddPage")
            {
                string title = "TabPage " + (tabControl1.TabCount).ToString();
                TabPage myTabPage = new TabPage(title);
                ListView lv = new ListView();
                lv.Dock = DockStyle.Bottom;

                drawingPanel dp = new drawingPanel();
                dp.Dock = DockStyle.Fill;
                myTabPage.Controls.Add(dp);
                myTabPage.Controls.Add(lv);

                tabControl1.TabPages.Insert(tabControl1.TabCount - 1, myTabPage);
                tabControl1.SelectedTab = tabControl1.TabPages[tabControl1.TabCount - 2];

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            new frmIProcedures().ShowDialog();
        }

        private void frmSimulation_Load(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Run(true);
        }

        private void btnDefine_Click(object sender, EventArgs e)
        {
            Model md = (Model)tabControl1.SelectedTab.Tag;
            md.Define(GetEndModelTime());
        
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Model md = (Model)tabControl1.SelectedTab.Tag;
            md.Reset();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            frmIConditions frm = new frmIConditions();
            frm.ShowDialog();
        }
        
    }
}