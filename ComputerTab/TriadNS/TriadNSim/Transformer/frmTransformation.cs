using DrawingPanel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TriadNSim.Transformer;

namespace TriadNSim.Forms
{
    public partial class frmTransformation : Form
    {
        private ImageList list;
        private frmRules parent;
        private string edit;
        public frmTransformation(ImageList lv,frmRules par,string e)
        {
            InitializeComponent();
            parent = par;
            edit = e;
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton1_ButtonClick(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void frmTransformation_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text=="")
            {
                MessageBox.Show("Введите имя правила");
                return;
            }
            if (leftPart.Shapes.Count==0)
            {
                MessageBox.Show("Не определена правая часть правила");
                return;
            }
            if (parent.lstRules.Items.ContainsKey(txtName.Text))
            {
                MessageBox.Show("Правило с таким именем уже существует");
                return;
            }
            TransformationRule item = new TransformationRule();
            item.Name = txtName.Text;
            copy_shapes(leftPart.Shapes,item.leftPart);
            copy_shapes(rightPart.Shapes, item.rightPart);
            parent.transform.Rules.Add(item);
            parent.lstRules.Items.Add(item.Name);
            parent.lstRules.Items[parent.lstRules.Items.Count - 1].Selected = true;
            this.Close();
        }
        private void copy_shapes(ArrayList dp,ArrayList rule)
        {
            foreach (BaseObject obj in dp)
                rule.Add(obj);
        }
    }
}
