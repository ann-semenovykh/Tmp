using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DrawingPanel;

namespace TriadNSim.Forms
{
    public partial class ModifyMarks : Form
    {
        public int mult;
        public ModifyMarks(PetriMark mark)
        {
            InitializeComponent();
            txtMult.Text = mark.mult.ToString();
            mult = mark.mult;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ModifyMarks_Load(object sender, EventArgs e)
        {

        }

        private void txtMult_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
            mult = Convert.ToInt32(txtMult.Text);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
