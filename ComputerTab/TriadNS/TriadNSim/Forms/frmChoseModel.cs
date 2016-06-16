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
        public frmChoseModel(COWLOntologyManager ontologyManager)
        {
            InitializeComponent();
            lbModel.SelectedIndex = 0;
            foreach (IOWLClass cls in ontologyManager.GetNetworkElements(":Model"))
            {
                string sName = cls.Comment;
                if (sName.Length == 0)
                    sName = cls.Name;
                int ind=lbModel.Items.Add(sName);
                
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lbModel.SelectedIndex == 0)
                this.Tag = @"ComputerModel";
            else this.Tag = @"PetriNet";
        }

        private void frmChoseModel_Load(object sender, EventArgs e)
        {
            
        }

        private void lbModel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lbModel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnOK_Click(sender, new EventArgs());
            this.Close();
        }
    }
}
