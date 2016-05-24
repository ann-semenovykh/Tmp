using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using drawingPanel = DrawingPanel.DrawingPanel;
using System.Drawing;
using System.IO;
using System.Xml;
using DrawingPanel;
using TriadNSim.Forms;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;
using TriadCompiler;
using TriadCore;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;


namespace TriadNSim
{
    public class Model: Object
    {
        protected  TabPage tb;
        protected  ListView lv;
        protected  drawingPanel dp;
        public  ImageList img=new ImageList();
        protected  DrawingPanel.BaseObject[] m_oSelectedObjects;
        protected  Dictionary<ListViewItem, Bitmap> ItemImages = new Dictionary<ListViewItem, Bitmap>();
        protected  COWLOntologyManager ontologyManager;
        public  const string sOntologyPath = "Ontologies\\Petri.owl";
        protected  frmChangeRoutine m_frmChangeRoutine;
        private static Model instance = null;
        private string name=null;
        
        public Model(TabPage t,ListView l,drawingPanel d)
        {
            instance = this;
            tb = t;
            lv = l;
            dp = d;
            dp.DragDrop+=new DragEventHandler(dp_DragDrop);
            dp.DragEnter+=new DragEventHandler(dp_DragEnter);
            dp.objectSelected+=new ObjectSelected(dp_objectSelected);
            dp.MouseUp+=new MouseEventHandler(dp_MouseUp);
            lv.DragOver+=new DragEventHandler(lv_DragOver);
            lv.MouseUp+=new MouseEventHandler(lv_MouseUp);
            lv.ItemDrag+=new ItemDragEventHandler(lv_ItemDrag);
            dp.beforeAddLine+=new BeforeAddLine(dp_beforeAddLine);
            dp.onLineCPChanged+=new OnLineCPChanged(dp_onLineCPChanged);
            dp.MouseDoubleClick+=new MouseEventHandler(dp_MouseDoubleClick);

            dp.beforeAddDynamicOb += dp_beforeAddDynamicOb;
            LoadPage();
        }
        protected virtual void dp_beforeAddDynamicOb(object sender, BeforeAddDynamicObEventArgs e)
        {
        }
        public virtual void Define(int EndModelTime)
        {
            SimulationInfo simInfo = new SimulationInfo(dp.Shapes, EndModelTime);
            if (!ontologyManager.Complete(simInfo))
                Util.ShowErrorBox(ontologyManager.sCompleteError); 
        }
        public virtual void Reset()
        {
            foreach (BaseObject obj in dp.Shapes)
            {
                if (obj is NetworkObject)
                    (obj as NetworkObject).Routine = null;
                else if (obj is Link)
                {
                    Link link = obj as Link;
                    link.PolusFrom = null;
                    link.PolusTo = null;
                }
            }
        }
        public virtual void Calc()
        {
            if (dp.Shapes.Count == 0)
            {
                Util.ShowErrorBox("Сеть пуста");
                return;
            }
            frmCalculate frmCalc = new frmCalculate(dp);
            frmCalc.ShowDialog();
        }

        public virtual void Run(int EndModelTime,bool bSelectSimCondition = false)
        {
            SimulationInfo simInfo = new SimulationInfo(dp.Shapes, EndModelTime);

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
            catch (Exception ex)
            {
                Util.ShowErrorBox(ex.Message);
            }

            Cursor.Current = Cursors.Default;
        }
        public virtual void visualize(object sender, VisualizeArgs e)
        {
            
        }
        protected virtual void dp_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BaseObject selObj = dp.ShapeCollection.selectedObj;
            if (selObj is Link)
                EditLink(selObj as Link, true);
            if (selObj is DynamicObject)
                EditObject(selObj as DynamicObject);
        }
        public void EditObject(DynamicObject oMark)
        {
            //ModifyMarks frmMark = new ModifyMarks(oMark);
            //if (frmMark.ShowDialog() == DialogResult.OK)
            //    oMark.mult = frmMark.mult;
        }
        protected virtual void dp_onLineCPChanged(object sender, OnLineCPChangedEventArgs e)
        {
            Link link = e.line as Link;
            if (link != null)
            {
                if (e.fromChanged)
                    link.PolusFrom = null;
                else
                    link.PolusTo = null;
                if (!EditLink(link, false))
                    dp.Undo();
            }
        }
        protected virtual void dp_beforeAddLine(object sender, BeforeAddLineEventArgs e)
        {
            e.cancel = true;
            NetworkObject objFrom = e.fromCP.Owner as NetworkObject;
            NetworkObject objTo = e.toCP.Owner as NetworkObject;
            if (dp.ShapeCollection.GetLine(objFrom, objTo) == null)
            {
                Link oLink = new Link(dp, e.fromCP, e.toCP);
                if (EditLink(oLink, false))
                    dp.ShapeCollection.AddShape(oLink);
            }
        }
        protected virtual void lv_ItemDrag(object sender, ItemDragEventArgs e)
        {
            lv.DoDragDrop(e.Item, DragDropEffects.Move);
        }
        protected virtual void lv_MouseUp(object sender, MouseEventArgs e)
        {
            
        }
        private int modeltime;
        public int GetEndModelTime
        {
            get
            {
                return modeltime;
            }
            set
            {
                modeltime=value;
            }
        }
        protected virtual void cMenuItemsDelElement_Click(object sender, EventArgs e)
        {
            ListViewItem item = lv.SelectedItems[0];
            if (Util.ShowConformationBox("Удалить элемент '" + item.Text + "'?"))
            {
                ontologyManager.RemoveClass(ontologyManager.GetClass((item.Tag as object[])[0].ToString()));
                ontologyManager.SaveOntology(sOntologyPath);
                ItemImages.Remove(item);
                lv.Items.Remove(item);
            }
        }
        protected virtual void сMenuItemsRoutines_Click(object sender, EventArgs e)
        {
            ListViewItem item = lv.SelectedItems[0];
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
        protected virtual void ChangeElementImage(object sender, EventArgs e)
        {
            ListViewItem item = lv.SelectedItems[0];
            if (item != null)
            {
                Bitmap bmp = LoadImage("Изображение элемента '" + item.Text + "'");
                if (bmp != null)
                {
                    ItemImages[item] = bmp;
                    img.Images.Add(bmp);
                    item.ImageIndex = img.Images.Count - 1;
                }
            }
        }
        public  static Bitmap LoadImage(string sTitle)
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
        protected virtual void сMenuItemsAdd_Click(object sender, EventArgs e)
        {
            
        }
        protected virtual void lv_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
        }
        protected virtual void LoadPage()
        {
            m_frmChangeRoutine = new frmChangeRoutine(dp);
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
            Size s = new Size(60, 60);
            img.ImageSize = s;
            lv.LargeImageList = img;
        }

        protected  string m_sFileName = string.Empty;
        protected  string sUserIPFileName = "IP.dat";
        protected  string sSimCondFileName = "SimCond.dat";

        public  List<InfProcedure> userIProcedures = null;
        public  List<InfProcedure> standartIProcedures = null;
        public  List<SimCondition> simConditions = null;
        protected virtual void LoadSimConditions()
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
        protected virtual void LoadStandartIP()
        {
            standartIProcedures = new List<InfProcedure>();
            List<IProcedureType> iprocedures = SimConditionParser.StandartIP;
            foreach (var ip in iprocedures)
                standartIProcedures.Add(new InfProcedure(ip, true));
        }
        protected virtual void LoadUserIP()
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
        public virtual TabPage TabPage
        {
            get
            {return tb;}
            set
            {tb=value;}
        }

        protected virtual void LoadElements()
        {

            
        }

        protected virtual Dictionary<string, Bitmap> LoadImageList(string path)
        {
            Dictionary<string, Bitmap> res = new Dictionary<string, Bitmap>();
            if (File.Exists(path))
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreWhitespace = true;
                settings.IgnoreComments = true;
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                int nCount = doc.ChildNodes.Count;
                for (int i = 0; i < nCount; i++)
                {
                    XmlNode node = doc.ChildNodes[i];
                    if (node.Name == "elements")
                    {
                        foreach (XmlNode item in node.ChildNodes)
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
        protected virtual void dp_DragDrop(object sender, DragEventArgs e)
        {
            
        }
        protected virtual string GetUniqueShapeName(string sName)
        {
            int nIndex = 1;
            string sRes = sName.ToLower() + nIndex.ToString();
            Dictionary<string, bool> names = GetShapeNames();
            while (names.ContainsKey(sRes))
            {
                nIndex++;
                sRes = sName.ToLower() + nIndex.ToString();
            }
            return sName + nIndex.ToString();
        }
        protected virtual Dictionary<string, bool> GetShapeNames()
        {
            Dictionary<string, bool> ShapeNames = new Dictionary<string, bool>();
            foreach (DrawingPanel.BaseObject obj in dp.Shapes)
            {
                if (obj.Name != null && obj.Name.Length > 0)
                    ShapeNames[obj.Name.ToLower()] = true;
            }
            return ShapeNames;
        }
        protected virtual void dp_objectSelected(object sender, PropertyEventArgs e)
        {
            m_oSelectedObjects = e.ele;
        }
        protected virtual void dp_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
                e.Effect = DragDropEffects.Move;
        }
        protected virtual void miIP_Click(object sender, EventArgs e)
        {
            if (m_oSelectedObjects == null || m_oSelectedObjects.Length != 1)
                return;
            frmObjectIPs frmObjectIPs = new frmObjectIPs(m_oSelectedObjects[0] as NetworkObject);
            frmObjectIPs.ShowDialog();
        }
        protected virtual void miCharacteristics_Click(object sender, EventArgs e)
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
            }
        }
        public virtual NetworkObject GetSelectedObject()
        {
            if (m_oSelectedObjects == null || m_oSelectedObjects.Length != 1)
                return null;
            return m_oSelectedObjects[0] as NetworkObject;
        }
        public virtual void ResetRoutine(Object sender, EventArgs e)
        {
            NetworkObject obj = GetSelectedObject();
            if (obj != null && obj.Routine != null)
            {
                foreach (BaseObject baseobj in dp.Shapes)
                {
                    if (baseobj is Link)
                    {
                        Link link = baseobj as Link;
                        if (link.FromCP.Owner == obj)
                            link.PolusFrom = null;
                        if (link.ToCP.Owner == obj)
                            link.PolusTo = null;
                    }
                }
                obj.Routine = null;
            }
        }
        protected virtual void miUserObjectChangeRoutine_Click(object sender, EventArgs e)
        {
            
        }
        protected virtual void dp_MouseUp(object sender, MouseEventArgs e)
        {
          
        }
        public virtual void SelectRoutine(Object sender, EventArgs e)
        {
            NetworkObject obj = GetSelectedObject();
            if (obj != null)
            {
                string sType = ((ToolStripMenuItem)sender).Tag.ToString();
                IOWLClass cls = ontologyManager.GetClass(sType);
                if (!ontologyManager.CompleteNode(new SimulationInfo(dp.Shapes, 0), obj, ontologyManager.GetIndividuals(cls)[0]))
                {
                    Util.ShowErrorBox("Не удалось наложить рутину. Неподходящее кол-во дуг и/или типы соседей");
                }
            }
        }
        protected virtual void miLinkChange_Click(object sender, EventArgs e)
        {
            if (m_oSelectedObjects == null || m_oSelectedObjects.Length != 1)
                return;
            Link obj = m_oSelectedObjects[0] as Link;
            if (obj == null)
                return;
            EditLink(obj, true);
        }
        public virtual Boolean EditLink(Link oLink, Boolean bNeedShowDialog)
        {
            ModifyRelation frmRelation = new ModifyRelation(oLink);
            if (frmRelation.Success && !bNeedShowDialog || frmRelation.ShowDialog() == DialogResult.OK)
            {
                oLink.PolusFrom = frmRelation.From != null ? frmRelation.From : null;
                oLink.PolusTo = frmRelation.To != null ? frmRelation.To : null;
                return true;
            }
            return false;
        }
        protected virtual void miDelete_Click(object sender, EventArgs e)
        {
            dp.DeleteSelected();
        }
        protected virtual void miUserObjectImage_Click(object sender, EventArgs e)
        {
            if (m_oSelectedObjects == null || m_oSelectedObjects.Length != 1)
                return;
            NetworkObject obj = m_oSelectedObjects[0] as NetworkObject;
            //if (obj.Type != ENetworkObjectType.UserObject)
            //    return;
            obj.Load_IMG();
        }
        public string Name
        {
            get { return name; }
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
        public InfProcedure GetUserIP(string Name)
        {
            foreach (var proc in userIProcedures)
            {
                if (proc.Name == Name)
                    return proc;
            }
            return null;
        }
        public void SaveUserIP()
        {
            Stream StreamWrite = File.Open(sUserIPFileName, FileMode.Create, FileAccess.Write);
            BinaryFormatter BinaryWrite = new BinaryFormatter();
            BinaryWrite.Serialize(StreamWrite, userIProcedures);
            StreamWrite.Close();
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
        
        public void SaveSimConditions()
        {
            Stream StreamWrite = File.Open(sSimCondFileName, FileMode.Create, FileAccess.Write);
            BinaryFormatter BinaryWrite = new BinaryFormatter();
            BinaryWrite.Serialize(StreamWrite, simConditions);
            StreamWrite.Close();
        }
        public COWLOntologyManager OntologyManager
        {
            get
            {
                return ontologyManager;
            }
        }
        public static Model Instance
        {
            get
            {
                return instance;
            }
        }
        public drawingPanel Panel
        {
            get
            {
                return dp;
            }
        }
    }

}
