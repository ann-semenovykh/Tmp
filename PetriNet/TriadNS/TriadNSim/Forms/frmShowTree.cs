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
        TreeNode globalroot;
        int rows;
        int columns;
        public frmShowTree(frmMainPetri par)
        {
            InitializeComponent();
            parent = par;
        }

        private void populatenode(ref TreeNode parent,int[] mark,int[,] input, int[,] output,List<string> transitions)
        {
            bool doubl = 
            globalroot.Nodes.Find("(" + String.Join(", ", mark).Replace("-1","w") + ")",true).Count()>1;
            if (doubl) return;                              // вершина дублирующая

            List<int> ok = new List<int>();
                                                           //нет hазрешенных переходов
            for (int i = 0; i < columns; i++)
            {
                int j;
                for (j = 0; j < rows; j++)
                    if (mark[j] >= input[j, i]||mark[j]==-1)
                        continue;
                    else break;
                if (j == rows) ok.Add(i);               //находим переходы
            }
            if (ok.Count==0) return;                    //нет возможных переходов для запуска
            else
                for (int i=0;i<ok.Count;i++)
                {
                    int[] tmp=new int[mark.Count()];
                    mark.CopyTo(tmp,0);
                    for (int j = 0; j < rows; j++)
                        if (tmp[j]!=-1)
                        tmp[j] += output[j, ok[i]] - input[j, ok[i]];

                        //пересчитываем маркировку
                    
                    checkparents(parent, ref tmp); 
                    TreeNode node = parent.Nodes.Add("(" + String.Join(", ", tmp).Replace("-1", "w") + ")", transitions[ok[i]] + ": " + "(" + String.Join(", ", tmp).Replace("-1", "w") + ")");

                    node.Tag = tmp;
                    populatenode(ref node, tmp, input, output, transitions);
                }

        }
        private void checkparents(TreeNode parent,ref int[] tmp)
        {
            while (parent!=null)
            {
                bool ok = true;
                int[] arr = parent.Tag as int[];
                for (int i = 0; i < arr.Count()&&ok; i++)
                    if (arr[i]!=-1)
                    ok &= arr[i] <= tmp[i];
                if (ok)
                {
                    for (int i = 0; i < arr.Count(); i++)
                        if (arr[i] < tmp[i] && arr[i] != -1)
                            tmp[i] = -1;
                   break;
                }else
              parent = parent.Parent;
            }
        }
        public void PopulateTree(List<int> mark, int[,] input, int[,] output,int n,int m,List<string> transitions)
        {
            rows = n;
            columns = m;
            TreeNode root=new TreeNode("("+ String.Join(", ", mark)+")");
            root.Name = "(" + String.Join(", ", mark) + ")";
            root.Tag = mark.ToArray();
            globalroot = root;
            populatenode(ref root,mark.ToArray(),input,output,transitions);
            treeView1.Nodes.Add(root);
            analyse();


        }
        void analyse()
        {
            bool safety=true;
            bool save=true;
            bool constr=true;
            TreeNode node=treeView1.Nodes[0];
            int sum=(globalroot.Tag as int[]).Sum();
            while ((safety||save||constr)&&node!=null)
            {
                int[] tmp = node.Tag as int[];
                save &= tmp.Sum() == sum && !tmp.Contains(-1);

                if (safety)
                    for (int i = 0; i < tmp.Count(); i++)
                    {
                        safety &= (tmp[i] == 0 || tmp[i] == 1);
                        constr &= tmp[i] != -1;
                    }
                if (node.NextNode == null&&node.Nodes.Count>0)
                    node = node.Nodes[0];
                else
                    node = node.NextNode;
            }
            cbLimit.Checked = constr;
            cbReserv.Checked = save;
            cbSafety.Checked = safety;
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
