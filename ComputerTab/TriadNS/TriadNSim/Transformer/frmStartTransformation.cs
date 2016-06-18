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
    public partial class frmStartTransformation : Form
    {
        public frmStartTransformation(frmSimulation par,ArrayList shape)
        {
            InitializeComponent();
            parent = par;
            foreach (BaseObject obj in shape)
                dpModel.Shapes.Add(obj);
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

                make_transformation();
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
        int relcount;
        void find_left_part(int i)
        {

            Visited.RemoveRange(0, Visited.Count);
            graph.RemoveRange(0, graph.Count);
            
            relcount = 0;
            foreach (BaseObject obj in transform.Rules[i].leftPart)
                if (obj is Line)
                    relcount++;
            if (relcount == 0)
            {
                add_entities(i);
                return;
            }
            foreach (BaseObject obj in transform.Rules[i].leftPart)
                if (obj is Line)
                {
                    ArrayList relations = new ArrayList();
                    find_relations(dpModel.Shapes,relations, (Line)obj);
                    foreach (Line rel in relations)
                        find_sub_graph(rel,i);
                }
        }

        void find_sub_graph(Line rel,int i)
        {
            graph.Add(rel);
            int count = 0;
            foreach (BaseObject ob in graph)
                if (ob is Line)
                    count++;
            if (count==relcount)
            {
                add_entities(i);
                graph.Remove(rel);
                return;
            }
            BaseObject obj = (BaseObject)rel.FromCP;
            if (!Visited.Contains(obj))
            {
                Visited.Add(obj);
                foreach (BaseObject ob in dpModel.Shapes)
                    if (ob is Line)
                    {
                        Line li = (Line)ob;
                        if (li.FromCP == obj && !graph.Contains(li))
                        foreach (BaseObject o in transform.Rules[i].leftPart)
                            if (o is Line)
                        {
                            Line lli = (Line)o;
                                if (lli.directed==li.directed && lli.FromCP.Name==obj.Tag)
                            find_sub_graph(li, i);
                        }

                    }
                foreach (BaseObject ob in dpModel.Shapes)
                    if (ob is Line)
                    {
                        Line li = (Line)ob;
                        if (li.ToCP == obj && !graph.Contains(li))
                            foreach (BaseObject o in transform.Rules[i].leftPart)
                                if (o is Line)
                                {
                                    Line lli = (Line)o;
                                    if (lli.directed == li.directed && lli.ToCP.Name == obj.Tag)
                                        find_sub_graph(li, i);
                                }

                    }  
            }
            obj = (BaseObject)rel.ToCP;
            if (!Visited.Contains(obj))
            {
                Visited.Add(obj);
                foreach (BaseObject ob in dpModel.Shapes)
                    if (ob is Line)
                    {
                        Line li = (Line)ob;
                        if (li.FromCP == obj && !graph.Contains(li))
                            foreach (BaseObject o in transform.Rules[i].leftPart)
                                if (o is Line)
                                {
                                    Line lli = (Line)o;
                                    if (lli.directed == li.directed && lli.FromCP.Name == obj.Tag)
                                        find_sub_graph(li, i);
                                }

                    }
                foreach (BaseObject ob in dpModel.Shapes)
                    if (ob is Line)
                    {
                        Line li = (Line)ob;
                        if (li.ToCP == obj && !graph.Contains(li))
                            foreach (BaseObject o in transform.Rules[i].leftPart)
                                if (o is Line)
                                {
                                    Line lli = (Line)o;
                                    if (lli.directed == li.directed && lli.ToCP.Name == obj.Tag)
                                        find_sub_graph(li, i);
                                }

                    }
            }
        }
        void find_isolated(ArrayList isolated,int i)
        {
            foreach (BaseObject obj in transform.Rules[i].leftPart)
                if (obj is NetworkObject)    
                {
                    int count=0;
                    foreach (BaseObject ob in transform.Rules[i].leftPart)
                        if (ob is Line)
                        {
                            Line li = (Line)ob;
                            if (li.ToCP.Owner == obj || li.FromCP.Owner == obj)
                            { count++; break; }
                        }
                    if (count == 0)
                        isolated.Add(obj);
                }
        }
        NetworkObject in_graph(ArrayList arr,NetworkObject obj)
        {
            foreach (BaseObject ob in arr)
                if (ob is NetworkObject)
                {
                    NetworkObject no=(NetworkObject)ob;
                    if (no.Tag==obj.Name) 
                    return obj;
                }
            return null;
        }
        void add_entities(int i)
        {
            NetworkObject o;
            ArrayList relations = new ArrayList();
            foreach (BaseObject ob in graph)
                if (ob is Line)
                    relations.Add(ob);
            foreach (Line ob in relations)
            {
                Line li = (Line)ob;
                NetworkObject obj =(NetworkObject)li.FromCP.Owner;
                foreach (BaseObject bo in transform.Rules[i].leftPart)
                    if (bo is NetworkObject)
                    {
                        NetworkObject no = (NetworkObject)bo;
                        if (no.Name == obj.Tag && !graph.Contains(obj))
                        {
                            graph.Add(obj);
                            break;
                        }
                    }
                obj = (NetworkObject)li.ToCP.Owner;
                foreach (BaseObject bo in transform.Rules[i].leftPart)
                    if (bo is NetworkObject)
                    {
                        NetworkObject no = (NetworkObject)bo;
                        if (no.Name == obj.Tag && !graph.Contains(obj))
                        {
                            graph.Add(obj);
                            break;
                        }
                    }
            }
            ArrayList isolated = new ArrayList();
            find_isolated(isolated, i);
            foreach (NetworkObject obj in isolated)
            {
                o = in_graph(dpModel.Shapes, obj);
                    if (o!=null)
                        graph.Add(o);
            }
        }
        void find_relations(ArrayList g,ArrayList arr,Line line)
        {
            foreach (BaseObject obj in g)
            if (obj is Line)
            {
                Line li=(Line)obj;
                if (li.directed == line.directed)//&&fromcp.name==fromcp.tag??//tocp
                    arr.Add(li);
            }
        }
        void replace_right_part(int i)
        {

        }

        ArrayList graph = new ArrayList();
        ArrayList Visited = new ArrayList();
        void make_transformation()
        {
            int ind = 0;

            do
            {
                find_left_part(ind);
                if (graph.Count!=0)
                {
                    replace_right_part(ind);
                    ind = 0;
                }
                else
                    if (ind < transform.Rules.Count)
                        ind++;
            } while (ind != transform.Rules.Count);

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

        private void frmStartTransformation_Load(object sender, EventArgs e)
        {

        }

    }
}
