using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TriadNSim.Forms
{
    public partial class frmChoseModel : Form
    {
        public frmChoseModel()
        {
            InitializeComponent();
            lbModel.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lbModel.SelectedIndex == 0)
                this.Tag = @"ComputerModel";
            else this.Tag = @"PetriNet";
        }
    }
}
