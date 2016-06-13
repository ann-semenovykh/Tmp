using DrawingPanel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using TriadNSim.Forms;

namespace TriadNSim.Transformer
{
    public partial class frmRules : Form
    {
        public Transformation transform;
        public ImageList img = new ImageList();
        public string fileName;
        public frmRules()
        {
            InitializeComponent();
            transform = new Transformation();
        }

        private void lstRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSource.Enabled = cmbTarget.Enabled = !(lstRules.Items.Count > 0);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Stream StreamWrite;
                SaveFileDialog DialogueSauver = new SaveFileDialog();
                DialogueSauver.DefaultExt = "tr";
                DialogueSauver.Title = "Сохранить правила трансформации";
                DialogueSauver.Filter = "TransformationRule files (*.tr)|*.tr|All files (*.*)|*.*";
                bool bOK = false;
                if (fileName != string.Empty)
                {
                    DialogueSauver.FileName = fileName;
                    bOK = true;
                }
                else if (DialogueSauver.ShowDialog() == DialogResult.OK)
                    bOK = true;
                if (bOK && (StreamWrite = DialogueSauver.OpenFile()) != null)
                {
                    BinaryFormatter BinaryWrite = new BinaryFormatter();
                    BinaryWrite.Serialize(StreamWrite, this.transform);
                    StreamWrite.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception:" + ex.ToString(), "Save error:");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmTransformation tr = new frmTransformation(transform.lstItem,this,null);
            this.Hide();
            tr.ShowDialog();
            this.Show();
        }

        private void frmRules_Load(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstRules.SelectedItems.Count == 0)
                MessageBox.Show("Необходимо выбрать правило для удаления", "Ошибка");
            else
                if (MessageBox.Show("Вы действительно хотите удалить правило?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    lstRules.Items.Remove(lstRules.SelectedItems[0]);
            
        }
        private void add_shapes(ArrayList dp,ArrayList rule)
        {
            foreach (BaseObject obj in rule)
            {
                dp.Add(obj);
            }
        }
        private void btnModify_Click(object sender, EventArgs e)
        {
            if (lstRules.SelectedItems.Count > 0)
            {
                TransformationRule edit = transform.Rules.Find(ex => ex.Name == lstRules.SelectedItems[0].Name);
                frmTransformation tr = new frmTransformation(transform.lstItem, this, edit.Name);
                add_shapes(tr.leftPart.Shapes, edit.leftPart);
                add_shapes(tr.rightPart.Shapes, edit.rightPart);
                tr.txtName.Text = edit.Name;
                this.Hide();
                tr.ShowDialog();
                this.Show();
            }
            else MessageBox.Show("Необходимо выбрать правило для изменения", "Ошибка");
        }
    }
}
