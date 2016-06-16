using System;
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
    public partial class frmStartTransformation : Form
    {
        public frmStartTransformation(frmSimulation par)
        {
            InitializeComponent();
            parent = par;
        }
        frmSimulation parent;
        Transformation transform;
        private void btnAddRule_Click(object sender, EventArgs e)
        {
            try
            {
                Stream StreamRead;
                OpenFileDialog DialogueCharger = new OpenFileDialog();
                DialogueCharger.DefaultExt = "tr";
                DialogueCharger.Title = "Загрузка правил трансформации";
                DialogueCharger.Filter = "TransformationRule files (*.tr)|*.tr|All files (*.*)|*.*";
                if (DialogueCharger.ShowDialog() == DialogResult.OK)
                {
                    if ((StreamRead = DialogueCharger.OpenFile()) != null)
                    {
                        BinaryFormatter BinaryRead = new BinaryFormatter();
                        this.transform = (Transformation)BinaryRead.Deserialize(StreamRead);
                        StreamRead.Close();
                        if (transform.sourceModel == dpModel.model)
                        {
                            txtRules.Text = System.IO.Path.GetFileName(DialogueCharger.FileName);
                            cbRules.Checked = true;
                            btnAddRule.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Выбранные правила не соответсвуют исходной модели", "Ошибка");
                            transform = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception:" + ex.ToString(), "Load error:");
            }
        }

        private void cbRules_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void txtChoose_Click(object sender, EventArgs e)
        {
            
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            switch (btnChoose.Text)
            {
                case "Выбрать стартовый граф":
                btnChoose.Text = "Применить";
                btnChooseAll.Visible = true;
            break;
                case "Применить":
            
                btnChooseAll.Visible = false;
                btnChoose.Text = "Начать трансформацию";
                cbStart.Checked=true;
            break;
                case "Начать трансформацию":
                cbStart.Checked = true;
                dpModel.Enabled = false;


                btnChoose.Text = "Сохранить модель";
                cbDone.Checked = true;
                cbErr.Checked = dataOut.Rows.Count == 0;
            break;
                case "Сохранить модель":
            if (cbErr.Checked)
            {
                parent.newmodel(transform.targetModel);
                this.Close();
            }
            else MessageBox.Show("При выводе модели произошли ошибки\nМодель сохранить нельзя","Ошибка");
            break;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            transform = null;
            txtRules.Text = "";
            btnChooseAll.Visible = false;
            btnChoose.Text = "Выбрать стартовый граф";
            cbBegin.Checked = false;
            cbDone.Checked = false;
            cbRules.Checked = false;
            cbStart.Checked = false;
            btnAddRule.Enabled = true;
            cbErr.Checked = false;
        }

        private void btnChooseAll_Click(object sender, EventArgs e)
        {
            btnChoose_Click(sender, e);
        }

    }
}
