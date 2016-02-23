using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TriadCompiler;
using DrawingPanel;

namespace TriadNSim.Forms
{
    public partial class frmIProcedures : Form
    {
        public frmIProcedures()
        {
            InitializeComponent();
        }

        private void frmIProcedures_Load(object sender, EventArgs e)
        {
            foreach (var ip in frmMainPetri.Instance.standartIProcedures)
                AddIp(ip);
            foreach (var ip in frmMainPetri.Instance.userIProcedures)
                AddIp(ip);
        }

        private void btnAddIP_Click(object sender, EventArgs e)
        {
            frmEditIP oFrmEditIp = new frmEditIP();
            oFrmEditIp.Code = "infprocedure IP\ninitial\nendi\nhandling\nendh\nprocessing\nendp\nendinf";
            oFrmEditIp.ShowDialog();
            if (oFrmEditIp.Successed)
            {
                AddIp(oFrmEditIp.IProcedure);
                frmMainPetri.Instance.userIProcedures.Add(oFrmEditIp.IProcedure);
                frmMainPetri.Instance.SaveUserIP();
            }
        }

        private void AddIp(InfProcedure proc)
        {
            ListViewItem item = new ListViewItem();
            item.Text = proc.Name;
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, proc.Description));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, proc.Params.Count.ToString()));
            if (proc.IsStandart)
                listViewStandartIP.Items.Add(item);
            else
                listViewUserIP.Items.Add(item);
        }

        private void btnDeleteIP_Click(object sender, EventArgs e)
        {
            int nIndex = listViewUserIP.SelectedItems[0].Index;
            frmMainPetri.Instance.DeleteUserIP(listViewUserIP.SelectedItems[0].Text);
            listViewUserIP.Items.RemoveAt(nIndex);
        }

        private void btnEditIP_Click(object sender, EventArgs e)
        {
            frmEditIP oFrmEditIp = new frmEditIP();
            int nIndex = listViewUserIP.SelectedItems[0].Index;
            string sName = listViewUserIP.SelectedItems[0].Text;
            InfProcedure EditProc = frmMainPetri.Instance.GetUserIP(sName);
            oFrmEditIp.SetIP(EditProc);
            oFrmEditIp.ShowDialog();
            if (oFrmEditIp.Successed)
            {
                InfProcedure proc = oFrmEditIp.IProcedure;
                EditProc.Name = listViewUserIP.Items[nIndex].Text = proc.Name;
                EditProc.Description = listViewUserIP.Items[nIndex].SubItems[1].Text = proc.Description;
                listViewUserIP.Items[nIndex].SubItems[2].Text = proc.Params.Count.ToString();
                EditProc.Code = proc.Code;
                EditProc.ReturnCode = proc.ReturnCode;
                EditProc.Params = proc.Params;
                frmMainPetri.Instance.SaveUserIP();
            }
        }

        private void listViewUserIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDeleteIP.Enabled = btnEditIP.Enabled = listViewUserIP.SelectedItems.Count != 0;
        }
    }
}
