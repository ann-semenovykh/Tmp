using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using DrawingPanel;
using TriadCompiler;
using TriadNSim.Ontology;

//????????
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Threading;
using System.Xml;
using System.Diagnostics;
using System.Reflection;
using TriadCore;

namespace TriadNSim.Forms
{
    public partial class frmMainPetri : Form
    {
        private DrawingPanel.BaseObject[] m_oSelectedObjects;
        private frmChangeRoutine m_frmChangeRoutine;
        private string m_sFileName = string.Empty;
        private string sUserIPFileName = "IP.dat";
        private string sSimCondFileName = "SimCond.dat";
        private Dictionary<ListViewItem, Bitmap> ItemImages = new Dictionary<ListViewItem, Bitmap>();
        private COWLOntologyManager ontologyManager;
        public const string sOntologyPath = "Ontologies\\Petri.owl";
        public AppDomain domain = null;
        TreeView reachTree=new TreeView();
        frmShowTree tr ;

        public TreeNode curNode;
        public TreeNodes curstat;
        bool changed = false;
        /// <summary>
        /// Экземпляр формы
        /// </summary>
        private static frmMainPetri instance = null;
        public List<InfProcedure> userIProcedures = null;
        public List<InfProcedure> standartIProcedures = null;
        public List<SimCondition> simConditions = null;

        public frmMainPetri()
        {
            InitializeComponent();
            m_frmChangeRoutine = new frmChangeRoutine(drawingPanel1);
            ontologyManager = new COWLOntologyManager(sOntologyPath);
            m_frmChangeRoutine.OnNameChecked += delegate(object sender, CancelEventArgs args)
            {
                if (ontologyManager.GetClass(m_frmChangeRoutine.DesignTypeName) != null)
                    args.Cancel = true;
            };
            LoadUserIP();
            LoadStandartIP();
            LoadSimConditions();

            LoadElements();
            btnStop.Visible = false;
        }

        /// <summary>
        /// Экземпляр формы
        /// </summary>
        public static frmMainPetri Instance
        {
            get
            {
                if (instance == null)
                    instance = new frmMainPetri();
                return instance;
            }
        }

        public COWLOntologyManager OntologyManager
        {
            get
            {
                return ontologyManager;
            }
        }

        public DrawingPanel.DrawingPanel Panel
        {
            get
            {
                return drawingPanel1;
            }
        }

        public void SaveUserIP()
        {
            Stream StreamWrite = File.Open(sUserIPFileName, FileMode.Create, FileAccess.Write);
            BinaryFormatter BinaryWrite = new BinaryFormatter();
            BinaryWrite.Serialize(StreamWrite, userIProcedures);
            StreamWrite.Close();
        }

        private void LoadUserIP()
        {
            if (File.Exists(sUserIPFileName))
            {
                Stream StreamRead = File.Open(sUserIPFileName, FileMode.Open, FileAccess.Read);
                if (StreamRead.Length > 0)
                {
                    BinaryFormatter BinaryRead = new BinaryFormatter();
                    userIProcedures = (List<InfProcedure>)BinaryRead.Deserialize(StreamRead);
                }
                else
                    userIProcedures = new List<InfProcedure>(); 
                StreamRead.Close();
            }
            else
                userIProcedures = new List<InfProcedure>();   
        }

        public void SaveSimConditions()
        {
            Stream StreamWrite = File.Open(sSimCondFileName, FileMode.Create, FileAccess.Write);
            BinaryFormatter BinaryWrite = new BinaryFormatter();
            BinaryWrite.Serialize(StreamWrite, simConditions);
            StreamWrite.Close();
        }

        private void LoadSimConditions()
        {
            if (File.Exists(sSimCondFileName))
            {
                Stream StreamRead = File.Open(sSimCondFileName, FileMode.Open, FileAccess.Read);
                if (StreamRead.Length > 0)
                {
                    BinaryFormatter BinaryRead = new BinaryFormatter();
                    simConditions = (List<SimCondition>)BinaryRead.Deserialize(StreamRead);
                }
                else
                    simConditions = new List<SimCondition>(); 
                StreamRead.Close();
            }
            else
                simConditions = new List<SimCondition>();   
        }

        private void LoadStandartIP()
        {
            standartIProcedures = new List<InfProcedure>();
            List<IProcedureType> iprocedures = SimConditionParser.StandartIP;
            foreach (var ip in iprocedures)
                standartIProcedures.Add(new InfProcedure(ip, true));
        }

        public InfProcedure GetUserIP(string Name)
        {
            foreach (var proc in userIProcedures)
            {
                if (proc.Name == Name)
                    return proc;
            }
            return null;
        }

        public bool DeleteUserIP(string Name)
        {
            foreach (var ip in userIProcedures)
            {
                if (ip.Name == Name)
                {
                    userIProcedures.Remove(ip);
                    SaveUserIP();
                    return true;
                }
            }
            return false;
        }

        public SimCondition GetSimCondition(string Name)
        {
            foreach (var simCond in simConditions)
            {
                if (simCond.Name == Name)
                    return simCond;
            }
            return null;
        }

        public bool DeleteSimCondition(string Name)
        {
            foreach (var simCond in simConditions)
            {
                if (simCond.Name == Name)
                {
                    simConditions.Remove(simCond);
                    SaveSimConditions();
                    return true;
                }
            }
            return false;
        }

        public void Open()
        {
            string sNewFileName = drawingPanel1.Loader();
            if (sNewFileName != string.Empty)
            {
                this.m_sFileName = sNewFileName;
                this.Text = Util.ApplicationName + " [" + System.IO.Path.GetFileName(this.m_sFileName) + "]";
            }
        }

        public int GetEndModelTime()
        {
            int iResult;
            try
            {
                iResult = Int32.Parse(tstModelTime.Text);
            }
            catch
            {
                tstModelTime.Text = "100";
                iResult = 100;
            }
            return iResult;
        }

        public void Save(string fileName)
        {
            string sNewFileName = drawingPanel1.Saver(fileName);
            if (sNewFileName != string.Empty)
            {
                this.m_sFileName = sNewFileName;
                this.Text = this.m_sFileName;
            }
        }

        public void UpdateZoom()
        {
            try
            {
                string strZoom = toolStripcmbZoom.Text.Trim();
                if (strZoom.EndsWith("%")) strZoom = strZoom.Substring(0, strZoom.Length - 1);
                int value = Int32.Parse(strZoom);
                drawingPanel1.Zoom = value / 100.0f;
            }
            catch
            {
                toolStripcmbZoom.Text = Convert.ToString(drawingPanel1.Zoom * 100) + "%";
            }
        }

        private void miOpen_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void miSave_Click(object sender, EventArgs e)
        {
           Save(m_sFileName);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            toolStripcmbZoom.Text = Convert.ToString(drawingPanel1.Zoom * 100) + "%";
            drawingPanel1.CurrentTool = DrawingPanel.ToolType.ttSelect;
            toolStripbtnSelect.Checked = true;
            //listView1.Items[0].Tag =new object[2] { "Workstation", ENetworkObjectType.Client };
            //listView1.Items[1].Tag = new object[2] { "Server", ENetworkObjectType.Server };
            //listView1.Items[2].Tag = new object[2] { "Router", ENetworkObjectType.Undefined };

            //listView1.Items[3].Tag = new object[2] { "UserObj", ENetworkObjectType.UserObject };
        }

        private void toolStripcmbZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateZoom();
        }

        private void toolStripcmbZoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                UpdateZoom();
        }

        private void toolStripbtnLink_Click(object sender, EventArgs e)
        {
            drawingPanel1.CurrentTool = DrawingPanel.ToolType.ttLine;
            toolStripbtnSelect.Checked = toolStripButtonMark.Checked = false;
            toolStripbtnLink.Checked = true;
        }

        private void toolStripbtnSelect_Click(object sender, EventArgs e)
        {
            drawingPanel1.CurrentTool = DrawingPanel.ToolType.ttSelect;
            toolStripbtnSelect.Checked = true;
            toolStripbtnLink.Checked=toolStripButtonMark.Checked = false;
        }

        private void miRun_Click(object sender, EventArgs e)
        {
            SimulationInfo simInfo = new SimulationInfo(drawingPanel1.Shapes, GetEndModelTime());
            Run(simInfo);
        }

        private void toolStripbtnOpen_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void toolStripbtnSave_Click(object sender, EventArgs e)
        {
            Save(m_sFileName);
        }

        private void toolStripbtnRun_Click(object sender, EventArgs e)
        {
            List<int> marks=new List<int>();
            NetworkObject obj ;
            foreach (BaseObject ob in drawingPanel1.Shapes)
                if (ob is NetworkObject)
                {
                    obj = (NetworkObject)ob;
                    if (obj.Type == ENetPetriObjectType.Place)
                        marks.Add(obj.Mark.mult);
                }
            TreeNodes tmp = new TreeNodes(marks);
            reachTree.Nodes.Add(tmp.GetNode());
            reachTree.SelectedNode = reachTree.Nodes[0];
            curstat = tmp;

            SimulationInfo simInfo = new SimulationInfo(drawingPanel1.Shapes, GetEndModelTime());
            Run(simInfo,false);
        }
        private BaseObject FindNode(Node node)
        {
            BaseObject obj = null;
            foreach (BaseObject ob in drawingPanel1.Shapes)
                if (ob.Name == node.Name.BaseName) { obj = ob; break; }
            return obj;
        }
        private void DrawMarks()
        {
            PetriMark pm;
            foreach (BaseObject ob in drawingPanel1.Shapes)
                if (ob.bIsMark)
                { pm = (PetriMark)ob; pm.Draw(drawingPanel1.CreateGraphics(), drawingPanel1.dx, drawingPanel1.dy, drawingPanel1.Zoom); }
        }
        double curtime;
        public void visualize(object sender, VisualizeArgs e)
        {
            NetworkObject curnode = (NetworkObject)FindNode(e.CurNode);
            
            //NetworkObject node = (NetworkObject)FindNode(e.CurNode);     //NonSync
            //if ((node.SemanticType == "PlaceRoutine") && (node.Mark.mult != e.Num))
            //{
            //    node.Mark.mult = e.Num;
            //    node.Mark.MultChange();
            //    drawingPanel1.redraw(true);
            //    Thread.Sleep(500);
            //}

            bool child = ((curnode.Type == ENetPetriObjectType.Place) && (curnode.Mark.mult != e.Num));
            bool pause = e.EndModeling;    //Slow
            if ((curnode.Type == ENetPetriObjectType.Place) && !pause)
            {
                if (e.Nodes.ContainsKey(e.CurNode))
                    e.Nodes[e.CurNode] = e.Num;
                else
                    e.Nodes.Add(e.CurNode, e.Num);

            }
            else
            {

                pause = true;
                foreach (var key in e.Nodes)
                {
                    NetworkObject node = (NetworkObject)FindNode(key.Key);
                    if (node.Mark.mult != key.Value)
                    {
                        pause &= node.Mark.mult != key.Value;
                        node.Mark.mult = key.Value;
                    }

                }


                e.Nodes.Clear();

                if (pause)
                {
                    //DrawMarks();
                    drawingPanel1.redraw(true);
                    //Thread.Sleep(300);
                }
            }


            if (child)
            {
                bool ready = false;
                NetworkObject obj;
                int i = 0;

                foreach (BaseObject ob in drawingPanel1.Shapes)
                    if (ob is NetworkObject)
                    {
                        obj = (NetworkObject)ob;
                        if (obj.Type == ENetPetriObjectType.Place && curstat.treeNodes[i] != obj.Mark.mult)
                        {
                            curstat.treeNodes[i] = obj.Mark.mult;
                            ready = true;
                        }
                        if (obj.Type == ENetPetriObjectType.Place) i++;
                    }
                if (ready)
                {
                    if (manual)
                    e.EndModeling = true;
                    
                    int[] marks = new int[curstat.treeNodes.Count];
                    TreeNode children = new TreeNode();
                    curstat.treeNodes.CopyTo(marks);
                    TreeNodes tmp = new TreeNodes(marks.ToList<int>());
                    TreeNode tt = new TreeNode(tmp.GetNode());
                    reachTree.SelectedNode.Nodes.Add(tt);
                    reachTree.SelectedNode = tt;
                }
            }
        }
        private void SyncMarks(NetworkObject obj)
        {
            if (obj.Type == ENetPetriObjectType.Place)
            {
                obj.Routine.ParameterValues[0] =obj.Mark.mult as Object;
            }
        }
        private void Run(SimulationInfo simInfo, bool bSelectSimCondition = false)
        {
            //m_oSimulation.Start(true);

            if (simInfo.Nodes.Count == 0)
            {
                Util.ShowErrorBox("Сеть пуста");
                return;
            }
            bool bFind = false;
            foreach (NetworkObject obj in simInfo.Nodes)
            {
                if (obj.Routine == null)
                {
                    bFind = true;
                    break;
                }
                else SyncMarks(obj);
            }
            
            if (bFind)
            {
                if (Util.ShowConformationBox("У некоторых элементов неопределенное поведение. Доопределить автоматически?"))
                {
                    if (!ontologyManager.Complete(simInfo))
                    {
                        Util.ShowErrorBox(ontologyManager.sCompleteError);
                        return;
                    }
                }
                else
                    return;
            }

            if (bSelectSimCondition && simConditions.Count > 0)
            {
                frmSimulate frmSim = new frmSimulate(simInfo);
                if (frmSim.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
            }

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                TriadCore.Graph.Visualize += visualize;
                Simulation.Instance.Begin(simInfo);
            }
            catch(Exception ex)
            {
                Util.ShowErrorBox(ex.Message);
            }

            Cursor.Current = Cursors.Default;
        }

        private void tsbCalcStaticProp_Click(object sender, EventArgs e)
        {
            if (drawingPanel1.Shapes.Count == 0)
            {
                Util.ShowErrorBox("Сеть пуста");
                return;
            }
            frmCalculate frmCalc = new frmCalculate(drawingPanel1);
            frmCalc.ShowDialog();
        }

        private void drawingPanel1_objectSelected(object sender, PropertyEventArgs e)
        {
            
            m_oSelectedObjects = e.ele;
        }
        int x1, y1;
        private void drawingPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && !manual)
            {
                Point pntShow = drawingPanel1.PointToScreen(e.Location);
                if (m_oSelectedObjects != null && m_oSelectedObjects.Length == 1)
                {
                    ContextMenuStrip menu = new ContextMenuStrip();
                    NetworkObject obj = m_oSelectedObjects[0] as NetworkObject;
                    if (obj != null)
                    {
                        ToolStripMenuItem miRoutine = (ToolStripMenuItem)menu.Items.Add("Рутина", null);
                        if (obj.Routine != null)
                        {
                            menu.Items.Add("Информационные процедуры", null, miIP_Click);
                            menu.Items.Add("Параметры", null, miCharacteristics_Click);
                        }
                        {
                            miRoutine.DropDownItems.Add("Нет", null, ResetRoutine);
                            //if (obj.Type == ENetworkObjectType.UserObject)
                             miRoutine.DropDownItems.Add("Задать рутину", null, miUserObjectChangeRoutine_Click);
                            bool bFind = false;
                            IOWLClass semanticType = ontologyManager.GetClass(obj.SemanticType);
                            if (semanticType!=null)
                            foreach (var indiv in ontologyManager.GetIndividuals(semanticType))
                            {
                                IOWLClass cls = ontologyManager.GetIndividualClass(indiv);
                                string sName = cls.Comment;
                                if (sName.Length == 0)
                                    sName = cls.Name;
                                ToolStripMenuItem mi = (ToolStripMenuItem)miRoutine.DropDownItems.Add(sName, null, SelectRoutine);
                                mi.Tag = cls.Name;
                                if (obj.Routine != null && !bFind && obj.Routine.Type == mi.Tag.ToString())
                                {
                                    mi.Checked = true;
                                    bFind = true;
                                }
                            }
                            if (!bFind)
                            {
                                // int iIndex = obj.Type == ENetworkObjectType.UserObject && obj.Routine != null ? 1 : 0;
                                //((ToolStripMenuItem)miRoutine.DropDownItems[iIndex]).Checked = true;
                            }
                        }
                        menu.Items.Add("Изменить изображение", null, miUserObjectImage_Click);
                    }
                    else
                        menu.Items.Add("Изменить", null, miLinkChange_Click);

                    menu.Items.Add(new ToolStripSeparator());
                    menu.Items.Add("Удалить", null, miDelete_Click);
                    menu.Show(this, pntShow, ToolStripDropDownDirection.AboveRight);
                }
                
            }
            if (e.Button == MouseButtons.Left)
            {
                x1 = e.X;
                y1 = e.Y;
                if (manual)
                {
                    NetworkObject ob = null;
                    ob = GetSelectedObject();
                    if (m_oSelectedObjects != null) ob = (NetworkObject)m_oSelectedObjects[0];

                    if (ob != null && ob.Type == ENetPetriObjectType.Transition)
                    { changed = false; RunManual(ob); }
                }

            }
        }


        public NetworkObject GetSelectedObject()
        {
            if (m_oSelectedObjects == null || m_oSelectedObjects.Length != 1)
                return null;
            return m_oSelectedObjects[0] as NetworkObject;
        }

        public void SelectRoutine(Object sender, EventArgs e)
        {
            NetworkObject obj = GetSelectedObject();
            if (obj != null)
            {
                string sType = ((ToolStripMenuItem)sender).Tag.ToString();
                IOWLClass cls = ontologyManager.GetClass(sType);
                if (!ontologyManager.CompleteNode(new SimulationInfo(drawingPanel1.Shapes, GetEndModelTime()), obj, ontologyManager.GetIndividuals(cls)[0]))
                {
                    Util.ShowErrorBox("Не удалось наложить рутину. Неподходящее кол-во дуг и/или типы соседей");
                }
            }
        }

        public void ResetRoutine(Object sender, EventArgs e)
        {
            NetworkObject obj = GetSelectedObject();
            if (obj != null && obj.Routine != null)
            {
                foreach (BaseObject baseobj in drawingPanel1.Shapes)
                {
                    if (baseobj is PetriLink)
                    {
                        PetriLink link = baseobj as PetriLink;
                        if (link.FromCP.Owner == obj)
                            link.PolusFrom = null;
                        if (link.ToCP.Owner == obj)
                            link.PolusTo = null;
                    }
                }
                obj.Routine = null;
            }
        }

        private void NewFormRoutine(frmChangeRoutine m_frmChangeRoutine)
        {
            //m_frmChangeRoutine.OnNameChecked += delegate(object sender, CancelEventArgs args)
            //{
            //    if (ontologyManager.GetClass(m_frmChangeRoutine.DesignTypeName) != null)
            //        args.Cancel = true;
            //};
        }
        string rout_place="routine_place.txt";
        string rout_tr="routine_tr.txt";

        private void miUserObjectChangeRoutine_Click(object sender, EventArgs e)
        {
            if (m_oSelectedObjects == null || m_oSelectedObjects.Length != 1)
                return;
            NetworkObject obj = m_oSelectedObjects[0] as NetworkObject;
            //if (obj.Type != ENetworkObjectType.UserObject)
            //    return;
            Routine prevRout = obj.Routine;
            if (prevRout == null || obj.Routine.Type.Length > 0)
            {
                string path;
                if (obj.Type == ENetPetriObjectType.Place) path = rout_place;
                else path = rout_tr;
                obj.Routine = new Routine();
                obj.Routine.Name = "R" + obj.Name;
                //obj.Routine.Text = "routine " + obj.Routine.Name+File.OpenText(path).ReadToEnd() ;

                obj.Routine.Text = "routine " + obj.Routine.Name + "(InOut pol)\nendrout";
                obj.Routine.Poluses.Add(new Polus("pol"));
            }
            NewFormRoutine(m_frmChangeRoutine);
            m_frmChangeRoutine.SetObject(obj);
            m_frmChangeRoutine.ShowDialog();
            if (m_frmChangeRoutine.Successed)
            {
                frmChangeRoutine.SaveLastCompiledRoutine(obj.Routine.Name + ".dll");
                obj.Routine.Type = string.Empty;
                obj.Routine.User = true;
            }
            else
                obj.Routine = prevRout;
        }

        public static Assembly GenerateAssemblyFromFile(string sFile, string[] references = null)
        {
            if (File.Exists(sFile))
            {
                CompilerParameters compilerParameters = new CompilerParameters();
                compilerParameters.CompilerOptions = "/t:library";
                compilerParameters.GenerateInMemory = true;
                //compilerParameters.GenerateInMemory = false;
                //compilerParameters.OutputAssembly = "SimCondition.dll";
                compilerParameters.ReferencedAssemblies.Add("system.dll");
                compilerParameters.ReferencedAssemblies.Add("TriadCore.dll");
                compilerParameters.ReferencedAssemblies.Add("TriadNSim.exe");
                if (references != null)
                {
                    foreach (string reference in references)
                        compilerParameters.ReferencedAssemblies.Add(reference.ToLower());
                }
                return new CSharpCodeProvider().CompileAssemblyFromFile(compilerParameters, sFile).CompiledAssembly;
            }
            return null;
        }

        private void miHelp_Click(object sender, EventArgs e)
        {

        }

        private void miSaveAs_Click(object sender, EventArgs e)
        {
            Save(string.Empty);
        }

        private void toolStripbtnNew_Click(object sender, EventArgs e)
        {
            drawingPanel1.Clear();
        }

        private void miPrintPreview_Click(object sender, EventArgs e)
        {
            drawingPanel1.PrintPreview(1);
        }

        private void miUserObjectImage_Click(object sender, EventArgs e)
        {
            if (m_oSelectedObjects == null || m_oSelectedObjects.Length != 1)
                return;
            NetworkObject obj = m_oSelectedObjects[0] as NetworkObject;
            //if (obj.Type != ENetworkObjectType.UserObject)
            //    return;
            obj.Load_IMG();
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            base.DoDragDrop(e.Item,DragDropEffects.Move);
        }

        private void drawingPanel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
                e.Effect = DragDropEffects.Move;
        }

        private void listView1_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
        }

        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (listView1.GetItemAt(e.X, e.Y) != null)
                Cursor = Cursors.SizeAll;
            else
                Cursor = Cursors.Default;
        }

        public void GetLinks(PetriLink oLink, Polus from, Polus to)
        {
            foreach (BaseObject obj in drawingPanel1.ShapeCollection.ShapeList)
            {
                if (obj is PetriLink)
                {
                    PetriLink line = obj as PetriLink;
                    if (line.FromCP.Owner == oLink.FromCP.Owner && line.ToCP.Owner == oLink.ToCP.Owner)
                    //|| line.FromCP.Owner == obj2 && line.ToCP.Owner == obj1)
                    {
                        line.PolusFrom = from;
                        line.PolusTo = to;
                    }
                }
            }
        }
        public void EditMark(PetriMark oMark)
        {
            //ModifyMarks frmMark = new ModifyMarks(oMark);
            //if (frmMark.ShowDialog() == DialogResult.OK)
            //    oMark.mult = frmMark.mult;
        }
        public Boolean EditLink(PetriLink oLink, Boolean bNeedShowDialog)
        {
            ModifyRelation frmRelation = new ModifyRelation(oLink);
            if (frmRelation.Success && !bNeedShowDialog || frmRelation.ShowDialog() == DialogResult.OK)
            {
                oLink.PolusFrom = frmRelation.From != null ? frmRelation.From : null;
                oLink.PolusTo = frmRelation.To != null ? frmRelation.To : null;
                GetLinks(oLink, oLink.PolusFrom, oLink.PolusTo);
                //oLink.mult = frmRelation.mult;
                return true;
            }
            return false;
        }

        public PetriLink AddLink(CConnectionPoint From, CConnectionPoint To)
        {
            PetriLink r = new PetriLink(drawingPanel1, From, To,x,y,x1,y1);
            drawingPanel1.ShapeCollection.AddShape(r);
            return r;
        }

        private Dictionary<string, bool> GetShapeNames()
        {
            Dictionary<string, bool> ShapeNames = new Dictionary<string, bool>();
            foreach (BaseObject obj in drawingPanel1.Shapes)
            {
                if (obj.Name != null && obj.Name.Length > 0)
                    ShapeNames[obj.Name.ToLower()] = true;
            }
            return ShapeNames;
        }

        private string GetUniqueShapeName(string sName)
        {
            int nIndex = 1;
            string sRes = sName.ToLower() + nIndex.ToString();
            Dictionary<string, bool> names = GetShapeNames();
            while(names.ContainsKey(sRes))
            {
                nIndex++;
                sRes = sName.ToLower() + nIndex.ToString();
            }
            return sName + nIndex.ToString();
        }

        private void drawingPanel1_DragDrop(object sender, DragEventArgs e)
        {
            ListViewItem li = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
            Point pt = drawingPanel1.PointToClient(new Point(e.X, e.Y));
            object[] tag = li.Tag as object[];
            ENetPetriObjectType type = (ENetPetriObjectType)tag[1];

            float fZoom = drawingPanel1.Zoom;
            int delta = 100;
            int X = (int)((pt.X / fZoom - drawingPanel1.dx) - delta / 2);
            int Y = (int)((pt.Y / fZoom - drawingPanel1.dx) - delta / 2);
            NetworkObject shape = new NetworkObject(drawingPanel1);
            shape.Type = type;
            shape.Rect = new Rectangle(X, Y, delta, delta);
            shape.Name = GetUniqueShapeName(li.Text);
            {
                shape.SemanticType = ontologyManager.GetRoutineClass(ontologyManager.GetClass(tag[0] as string)).Name;//!!
            }
            if (ItemImages.ContainsKey(li))
            {
                shape.img = new Bitmap(ItemImages[li]);
               // shape.showBorder = type == ENetworkObjectType.UserObject;
                shape.Trasparent = false;
            }
            else
                shape.showBorder = false;
            drawingPanel1.ShapeCollection.AddShape(shape);
            if (shape.Type == ENetPetriObjectType.Place)
            {
                PetriMark m = new PetriMark(drawingPanel1, shape.ConnectionPoint);
                shape.Mark = m;
                m.mult = 0;

                drawingPanel1.ShapeCollection.AddShape(m);
                
            }

            drawingPanel1.Focus();
        }

        private void listView1_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            drawingPanel1.DeleteSelected();
        }

        private void miLinkChange_Click(object sender, EventArgs e)
        {
            if (m_oSelectedObjects == null || m_oSelectedObjects.Length != 1)
                return;
            PetriLink obj = m_oSelectedObjects[0] as PetriLink;
            if (obj == null)
                return;
            EditLink(obj, true);
        }

        private void miCharacteristics_Click(object sender, EventArgs e)
        {
            if (m_oSelectedObjects == null || m_oSelectedObjects.Length != 1)
                return;
            NetworkObject obj = m_oSelectedObjects[0] as NetworkObject;
            if (obj.Routine == null || obj.Routine.Parameters.Count == 0)
                return;

            frmEditParam param = new frmEditParam(obj.Routine.Parameters, obj.Routine.ParameterValues);
            param.Descriptions = obj.Routine.ParamDescription.ToArray();
            if (param.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                obj.Routine.ParameterValues = param.Values;
                obj.Routine.ParamDescription = param.Descriptions.ToList();
                if (obj.Type == ENetPetriObjectType.Place)
                {
                    obj.Mark.mult = Convert.ToInt32(param.Values[0]);
                    drawingPanel1.redraw(true);
                }
            }
        }

        private void miIP_Click(object sender, EventArgs e)
        {
            if (m_oSelectedObjects == null || m_oSelectedObjects.Length != 1)
                return;
            frmObjectIPs frmObjectIPs = new frmObjectIPs(m_oSelectedObjects[0] as NetworkObject);
            frmObjectIPs.ShowDialog();
        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            new frmIProcedures().ShowDialog();
        }

        private void drawingPanel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BaseObject selObj = drawingPanel1.ShapeCollection.selectedObj;
            if (selObj is PetriLink)
                EditLink(selObj as PetriLink, true);
            if (selObj is PetriMark) 
                EditMark(selObj as PetriMark);
        }

        private void drawingPanel1_beforeAddMark(object sender, BeforeAddMarkEventArgs e)
        {
            e.cancel = true;
            NetworkObject objFrom = e.CP.Owner as NetworkObject;
            if (objFrom.Type == ENetPetriObjectType.Place)
                if (drawingPanel1.ShapeCollection.GetMark(objFrom)==null)
            {
                //PetriMark m = new PetriMark(drawingPanel1, e.CP);
                //drawingPanel1.ShapeCollection.AddShape(m);
                //objFrom.Mark = m;
            }else
                {
                    PetriMark m = drawingPanel1.ShapeCollection.GetMark(objFrom);
                    m.IncMult();
                }

        }
        private void drawingPanel1_beforeAddLine(object sender, BeforeAddLineEventArgs e)
        {
            e.cancel = true;
            NetworkObject objFrom = e.fromCP.Owner as NetworkObject;
            NetworkObject objTo = e.toCP.Owner as NetworkObject;
            if (objFrom.Type != objTo.Type)//соединяются только различные вершины (позиции и переходы)
                if (drawingPanel1.ShapeCollection.GetLine(objFrom, objTo) == null) //связь туда и обратно
                {
                    
                    //PetriLine oLine = new PetriLine(drawingPanel1, e.fromCP, e.toCP, x, y, x1, y1);
                    PetriLink oLink = new PetriLink(drawingPanel1, e.fromCP, e.toCP, x, y, x1, y1);
                    if (EditLink(oLink, false))
                    {
                       // drawingPanel1.ShapeCollection.AddShape(oLine);
                        drawingPanel1.ShapeCollection.AddShape(oLink);
                    }
                }
                else
                {
                    //PetriLink oLink = new PetriLink(drawingPanel1, e.fromCP, e.toCP, x, y, x1, y1);
                    PetriLine curLine=drawingPanel1.ShapeCollection.GetLine(objFrom, objTo);
                    curLine.IncMult();
                     //drawingPanel1.ShapeCollection.AddShape(oLink);
                    
                }
        }

        private void drawingPanel1_onLineCPChanged(object sender, OnLineCPChangedEventArgs e)
        {
            PetriLink link = e.line as PetriLink;
            if (link != null)
            {
                if (e.fromChanged)
                    link.PolusFrom = null;
                else
                    link.PolusTo = null;
                if (!EditLink(link, false))
                    drawingPanel1.Undo();
            }
        }

        public bool ContainsElement(string sName)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if (String.Compare(sName, item.Text, true) == 0)
                    return true;
            }
            return false;
        }

        private void сMenuItemsAdd_Click(object sender, EventArgs e)
        {
            frmAddElement frm = new frmAddElement();
            frm.Bmp = global::TriadNSim.Properties.Resources.question;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                IOWLClass superClass = ontologyManager.GetClass(frm.ParentName);
                IOWLClass cls = ontologyManager.AddClass(frm.Name);
                ontologyManager.AddSubClass(cls, superClass);
                IOWLClass routClass = ontologyManager.AddRoutine(ontologyManager.GetRoutineClass(superClass), cls, frm.Name);
                ontologyManager.SaveOntology(sOntologyPath);

                imageList2.Images.Add(frm.Bmp);
                int nImageIndex = imageList2.Images.Count - 1;
                ListViewItem li = listView1.Items.Add(frm.Name, nImageIndex);
               // li.Tag = new object[2] { frm.Name, ENetworkObjectType.Undefined };
                ItemImages[li] = frm.Bmp;
            }
        }

        public static Bitmap LoadImage(string sTitle)
        {
            Bitmap bmp = null;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = sTitle;
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.PNG;*.GIF)|*.BMP;*.JPG;*.PNG;*.GIF|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    bmp = new Bitmap(dlg.FileName);
                }
                catch (Exception ex)
                {
                    bmp = null;
                }
            }
            return bmp;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SaveImageList();
            base.OnFormClosing(e);
        }
        
        private void SaveImageList()
        {
            XmlWriter writer = XmlWriter.Create("elements.xml");
            writer.WriteStartElement("elements");
            foreach (KeyValuePair<ListViewItem, Bitmap> pair in ItemImages)
            {
                writer.WriteStartElement("item");
                writer.WriteAttributeString("name", pair.Key.Text);
                MemoryStream ms = new MemoryStream();
                pair.Value.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                writer.WriteBase64(ms.ToArray(), 0, (int)ms.Length);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();
        }


        private void LoadElements()
        {
            
            listView1.Clear();
            imageList2.Images.Clear();

            string[] standartItems = { "Позиция", "Переход" }; //{ "Рабочая станция", "Сервер", "Маршрутизатор", "Пользовательский" };
            //string[] standartItems = { "Workstation", "Server", "Router", "Custom" };
            string[] standartItemNames = { "Place", "Transition" };
            ENetPetriObjectType[] types = { ENetPetriObjectType.Place, ENetPetriObjectType.Transition };
            Dictionary<string, ListViewItem> Items = new Dictionary<string, ListViewItem>();
            for (int i = 0; i < standartItems.Length; i++)
            {
                string sName = standartItems[i];
                ListViewItem item = listView1.Items.Add(sName);
                item.Tag = new object[2] { standartItemNames[i], types[i] };
                Items[sName.ToLower()] = item;
            }

            foreach (IOWLClass cls in ontologyManager.GetPetriNetElements())
            {
                string sName = cls.Comment;
                if (sName.Length == 0)
                    sName = cls.Name;
                if (Items.ContainsKey(sName.ToLower()))
                    continue;
                ListViewItem item = listView1.Items.Add(sName);
                //item.Tag = new object[2] { cls.Name, ENetworkObjectType.Undefined };
                Items[sName.ToLower()] = item;
            }

            Dictionary<string, Bitmap> images = LoadImageList();
            foreach (KeyValuePair<string, Bitmap> pair in images)
            {
                if (!Items.ContainsKey(pair.Key))
                    continue;
                ItemImages[Items[pair.Key]] = pair.Value;
                imageList2.Images.Add(pair.Value);
                Items[pair.Key].ImageIndex = imageList2.Images.Count - 1;
            }
        }

        private Dictionary<string, Bitmap> LoadImageList()
        {
            Dictionary<string, Bitmap> res = new Dictionary<string, Bitmap>();
            if (File.Exists("elements.xml"))
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreWhitespace = true;
                settings.IgnoreComments = true;
                XmlDocument doc = new XmlDocument();
                doc.Load("elements.xml");
                int nCount = doc.ChildNodes.Count;
                for (int i = 0; i < nCount; i++)
                {
                    XmlNode node = doc.ChildNodes[i];
                    if (node.Name == "elements")
                    {
                        foreach(XmlNode item in node.ChildNodes)
                        {
                            string sName = item.Attributes["name"].Value;
                            string sBase64 = item.InnerXml;
                            if (sBase64.Length > 0)
                            {
                                byte[] buffer = Convert.FromBase64String(sBase64);
                                MemoryStream ms = new MemoryStream();
                                ms.Write(buffer, 0, buffer.Length);
                                res[sName.ToLower()] = new Bitmap(ms);
                            }
                        }
                        break;
                    }
                }
            }
            return res;
        }

        private void сMenuItemsRoutines_Click(object sender, EventArgs e)
        {
            ListViewItem item = listView1.SelectedItems[0];
            string sName = (string)(item.Tag as object[])[0];
            IOWLClass cls = ontologyManager.GetClass(sName);
            if (cls == null)
            {
                Util.ShowErrorBox("Элемент '" + sName + "' не найден");
                return;
            }
            frmRoutines frm = new frmRoutines(cls);
            frm.ShowDialog();
        }
        
        private void ChangeElementImage(object sender, EventArgs e)
        {
            ListViewItem item = listView1.SelectedItems[0];
            if (item != null)
            {
                Bitmap bmp = LoadImage("Изображение элемента '" + item.Text + "'");
                if (bmp != null)
                {
                    ItemImages[item] = bmp;
                    imageList2.Images.Add(bmp);
                    item.ImageIndex = imageList2.Images.Count - 1;
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            foreach (BaseObject obj in drawingPanel1.Shapes)
            {
                if (obj is NetworkObject)
                    (obj as NetworkObject).Routine = null;
                else if (obj is PetriLink)
                {
                    PetriLink link = obj as PetriLink;
                    link.PolusFrom = null;
                    link.PolusTo = null;
                }
            }
        }

        private void drawingPanel1_objectDeleted(object sender, ObjectEventArgs e)
        {
            
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            frmIConditions frm = new frmIConditions();
            frm.ShowDialog();
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                if (listView1.SelectedItems.Count > 0)
                {
                    ListViewItem item = listView1.SelectedItems[0];
                    ENetPetriObjectType type = ENetPetriObjectType.Undefined;
                    object[] tag = null;
                    if (item.Tag != null)
                    {
                        tag = item.Tag as object[];
                        type = (ENetPetriObjectType)tag[1];
                    }
                   // if (type != ENetPetriObjectType.UserObject)
                    {
                       // menu.Items.Add("Поведения элемента", null, сMenuItemsRoutines_Click);
                        menu.Items.Add("Изменить изображение", null, ChangeElementImage);
                        //if (type == ENetworkObjectType.Undefined && tag[0].ToString() != "Router")
                        //    menu.Items.Add("Удалить элемент", null, cMenuItemsDelElement_Click);
                    }
                }
                else
                {
                    menu.Items.Add("Добавить элемент", null, сMenuItemsAdd_Click);
                }
                if (menu.Items.Count > 0)
                    menu.Show(listView1, e.Location, ToolStripDropDownDirection.AboveRight);
            }
        }


        private void cMenuItemsDelElement_Click(object sender, EventArgs e)
        {
            ListViewItem item = listView1.SelectedItems[0];
            if (Util.ShowConformationBox("Удалить элемент '" + item.Text + "'?"))
            {
                ontologyManager.RemoveClass(ontologyManager.GetClass((item.Tag as object[])[0].ToString()));
                ontologyManager.SaveOntology(sOntologyPath);
                ItemImages.Remove(item);
                listView1.Items.Remove(item);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            SimulationInfo simInfo = new SimulationInfo(drawingPanel1.Shapes, GetEndModelTime());
            Run(simInfo,true);
        }

        private void toolStripbtnRun_DropDownOpening(object sender, EventArgs e)
        {
            toolStripMenuItem1.Enabled = simConditions.Count > 0;
        }

        private void btnDefine_Click(object sender, EventArgs e)
        {
            SimulationInfo simInfo = new SimulationInfo(drawingPanel1.Shapes, GetEndModelTime());
            if (!ontologyManager.Complete(simInfo))
                Util.ShowErrorBox(ontologyManager.sCompleteError);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveImageList();
        }
        int x,y;
        private void drawingPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (e.Button==MouseButtons.Left)
            {
                x1=e.X;
                y1=e.Y;
            }

        }

        private void drawingPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                x = e.X;
                y = e.Y;
            }
        }

        private void toolStripButtonMark_Click(object sender, EventArgs e)
        {
            drawingPanel1.CurrentTool = DrawingPanel.ToolType.ttMark;
            toolStripbtnLink.Checked = toolStripbtnSelect.Checked = false;
            toolStripButtonMark.Checked = true;
        }
        
        
        private void btnShowReachTree_Click(object sender, EventArgs e)
        {
            List<NetworkObject> place = new List<NetworkObject>();
            List<NetworkObject> transition = new List<NetworkObject>();
            List<string> transitions = new List<string>();
            List<int> mark=new List<int>();
            foreach (BaseObject obj in drawingPanel1.Shapes)
            {
                if (obj is NetworkObject)
                    if ((obj as NetworkObject).Type == ENetPetriObjectType.Place)
                    {
                        place.Add((obj as NetworkObject));
                        mark.Add((obj as NetworkObject).Mark.mult);
                    }
                    else
                    {
                        transition.Add((obj as NetworkObject));
                        transitions.Add((obj as NetworkObject).Name);
                    }

            }
            
            int [,] input=new int[place.Count,transition.Count];
            int [,] output = new int[place.Count, transition.Count];
            
            for (int i=0;i<place.Count;i++)
                for (int j=0;j<transition.Count;j++)
                {
                    foreach (BaseObject obj in drawingPanel1.Shapes)
                        if (obj is PetriLine)
                        {
                            PetriLine link = obj as PetriLine;
                            if ((link.FromCP.Owner as NetworkObject) == place[i] && (link.ToCP.Owner as NetworkObject) == transition[j])
                                input[i, j] = link.mult;
                            if ((link.ToCP.Owner as NetworkObject) == place[i] && (link.FromCP.Owner as NetworkObject) == transition[j])
                                output[i, j] = link.mult;
                         }
                }
            tr = new frmShowTree(this);
            tr.PopulateTree(mark, input, output,place.Count,transition.Count,transitions);
            tr.treeView1.ExpandAll();
            tr.ShowDialog();
            

            //if (reachTree == null)
            //{
            //    MessageBox.Show("Необходимо произвести моделирование", "Внимание");
            //}
            //else
            //{
            //    tr = new frmShowTree(this);
            //    btnShowReachTree.Checked = true;
            //    tr.treeView1 = reachTree;
            //    tr.treeView1.CollapseAll();
            //    tr.Show();
            //}
        }
        private bool manual;
        private void RunManual(BaseObject ob)
        {
            List<int> marks = new List<int>();
            NetworkObject obj;
            foreach (BaseObject obb in drawingPanel1.Shapes)
                if (obb is NetworkObject)
                {
                    obj = (NetworkObject)obb;
                    if (obj.Type == ENetPetriObjectType.Place)
                        marks.Add(obj.Mark.mult);
                }
            TreeNodes tmpp = new TreeNodes(marks);
            reachTree.Nodes.Add(tmpp.GetNode());
            reachTree.SelectedNode = reachTree.Nodes[0];
            curstat = tmpp;


            //ожидать ответа пользователя
            ArrayList Sh = new ArrayList();

            PetriLine tmp;
            //найти все дуги вершины и позиции этих дуг
            foreach (BaseObject objj in drawingPanel1.Shapes)
                if (objj.bIsLine)
                {
                    tmp = (PetriLine)objj;
                    if (tmp.FromCP.Owner == ob)
                    { Sh.Add(tmp); Sh.Add(tmp.ToCP.Owner); }
                    else if (tmp.ToCP.Owner==ob)
                    { Sh.Add(tmp); Sh.Add(tmp.FromCP.Owner); }
                }
            Sh.Add(ob);
            SimulationInfo simInfo = new SimulationInfo(Sh, 1000); ///time???
            
            Run(simInfo);
        }
        private void StopManual()
        {
            manual = false;
            foreach (ToolStripItem it in MainToolBar.Items)
                it.Enabled = true;
            ручноеМоделированиеToolStripMenuItem.Enabled = true;
            btnStop.Visible = false;
        }

        private void ручноеМоделированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SimulationInfo simInfo = new SimulationInfo(drawingPanel1.Shapes, GetEndModelTime());
            if (simInfo.Nodes.Count == 0)
            {
                Util.ShowErrorBox("Сеть пуста");
                return;
            }
            bool bFind = false;
            foreach (NetworkObject obj in simInfo.Nodes)
            {
                if (obj.Routine == null)
                {
                    bFind = true;
                    break;
                }
                else SyncMarks(obj);
            }

            if (bFind)
            {
                if (Util.ShowConformationBox("У некоторых элементов неопределенное поведение. Доопределить автоматически?"))
                {
                    if (!ontologyManager.Complete(simInfo))
                    {
                        Util.ShowErrorBox(ontologyManager.sCompleteError);
                        return;
                    }
                }
                else
                    return;
            }
            manual = true;
            foreach (ToolStripItem it in MainToolBar.Items)
                if (it.Name != "toolStripbtnRun" && it.Name != "btnStop" && it.Name != "tstModelTime" && it.Name != "toolStripcmbZoom" && it.Name!="btnUndo")
                it.Enabled = false;
            ручноеМоделированиеToolStripMenuItem.Enabled = false;
            btnStop.Visible = true;
        }

        private void toolStripcmbZoom_Click(object sender, EventArgs e)
        {

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopManual();
        }
        public void frmShowTreeClose()
        {
           // btnShowReachTree.Checked = false;
        }

        private void btnShowReachTree_CheckedChanged(object sender, EventArgs e)
        {
            //if (!btnShowReachTree.Checked)
            //    tr.Close();
        }

        public void TreeNodeClicked(TreeNode node)
        {
            //reachTree.SelectedNode = reachTree.Nodes[ tr.treeView1.Nodes.IndexOf(node)];
            //List<int> tmp = new List<int>();
            //string val = reachTree.SelectedNode.Text;
            //string c="";
            //int i=1;
            //int n;

            //while (val[i]!=']')
            //{
            //    while (val[i] != ',')
            //    { c += val[i]; i++; }
            //    i++;
            //    n = Convert.ToInt16(c);
            //    tmp.Add(n);
            //}



        }

    }
}
