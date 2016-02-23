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
    public partial class frmShowTree : Form
    {
        frmMainPetri parent;

        public frmShowTree(frmMainPetri par)
        {
            InitializeComponent();
            parent = par;
        }

        private void frmShowTree_Load(object sender, EventArgs e)
        {

        }

        private void frmShowTree_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.frmShowTreeClose();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            parent.TreeNodeClicked(e.Node);
        }
    }
}
