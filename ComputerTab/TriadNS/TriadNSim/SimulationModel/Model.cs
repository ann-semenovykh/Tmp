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


namespace TriadNSim
{
    class Model: Object
    {
        private TabPage tb;
        private ListView lv;
        private drawingPanel dp;
        public ImageList img=new ImageList();
        private DrawingPanel.BaseObject[] m_oSelectedObjects;
        private Dictionary<ListViewItem, Bitmap> ItemImages = new Dictionary<ListViewItem, Bitmap>();
        private COWLOntologyManager ontologyManager;
        public const string sOntologyPath = "Ontologies\\ComputerNetwork.owl";
        private frmChangeRoutine m_frmChangeRoutine;

        public Model(TabPage t,ListView l,drawingPanel d)
        {
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
            LoadPage();
        }
        public void Define(int EndModelTime)
        {
            SimulationInfo simInfo = new SimulationInfo(dp.Shapes, EndModelTime);
            if (!ontologyManager.Complete(simInfo))
                Util.ShowErrorBox(ontologyManager.sCompleteError); 
        }
        public void Reset()
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
        public void Calc()
        {
            if (dp.Shapes.Count == 0)
            {
                Util.ShowErrorBox("Сеть пуста");
                return;
            }
            frmCalculate frmCalc = new frmCalculate(dp);
            frmCalc.ShowDialog();
        }

        public void Run(int EndModelTime,bool bSelectSimCondition = false)
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
        public void visualize(object sender, VisualizeArgs e)
        {
            
        }
        private void dp_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BaseObject selObj = dp.ShapeCollection.selectedObj;
            if (selObj is Link)
                EditLink(selObj as Link, true);
        }
        private void dp_onLineCPChanged(object sender, OnLineCPChangedEventArgs e)
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
        private void dp_beforeAddLine(object sender, BeforeAddLineEventArgs e)
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
        private void lv_ItemDrag(object sender, ItemDragEventArgs e)
        {
            lv.DoDragDrop(e.Item, DragDropEffects.Move);
        }
        private void lv_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                if (lv.SelectedItems.Count > 0)
                {
                    ListViewItem item = lv.SelectedItems[0];
                    ModelObjectType type = ModelObjectType.Undefined;
                    object[] tag = null;
                    if (item.Tag != null)
                    {
                        tag = item.Tag as object[];
                        type = (ModelObjectType)tag[1];
                    }
                    if (type != ModelObjectType.UserObject)
                    {
                        menu.Items.Add("Поведения элемента", null, сMenuItemsRoutines_Click);
                        menu.Items.Add("Изменить изображение", null, ChangeElementImage);
                        if (type == ModelObjectType.Undefined && tag[0].ToString() != "Router")
                            menu.Items.Add("Удалить элемент", null, cMenuItemsDelElement_Click);
                    }
                }
                else
                {
                    menu.Items.Add("Добавить элемент", null, сMenuItemsAdd_Click);
                }
                if (menu.Items.Count > 0)
                    menu.Show(lv, e.Location, ToolStripDropDownDirection.AboveRight);
            }
        }
        private void cMenuItemsDelElement_Click(object sender, EventArgs e)
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
        private void сMenuItemsRoutines_Click(object sender, EventArgs e)
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
        private void ChangeElementImage(object sender, EventArgs e)
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

                img.Images.Add(frm.Bmp);
                int nImageIndex = img.Images.Count - 1;
                ListViewItem li = lv.Items.Add(frm.Name, nImageIndex);
                li.Tag = new object[2] { frm.Name, ModelObjectType.Undefined };
                ItemImages[li] = frm.Bmp;
            }
        }
        private void lv_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
        }
        private void LoadPage()
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

        private string m_sFileName = string.Empty;
        private string sUserIPFileName = "IP.dat";
        private string sSimCondFileName = "SimCond.dat";

        public List<InfProcedure> userIProcedures = null;
        public List<InfProcedure> standartIProcedures = null;
        public List<SimCondition> simConditions = null;
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
        public TabPage TabPage
        {
            get
            {return tb;}
            set
            {tb=value;}
        }

        private void LoadElements()
        {

            lv.Clear();
            img.Images.Clear();

            string[] standartItems = { "Рабочая станция", "Сервер", "Маршрутизатор", "Пользовательский" };
            //string[] standartItems = { "Workstation", "Server", "Router", "Custom" };
            string[] standartItemNames = { "Workstation", "Server", "Router", "ComputerNetworkNode" };
            ModelObjectType[] types = { ModelObjectType.Client, ModelObjectType.Server, ModelObjectType.Undefined, ModelObjectType.UserObject };
            Dictionary<string, ListViewItem> Items = new Dictionary<string, ListViewItem>();
            for (int i = 0; i < standartItems.Length; i++)
            {
                string sName = standartItems[i];
                ListViewItem item = lv.Items.Add(sName);
                item.Tag = new object[2] { standartItemNames[i], types[i] };
                Items[sName.ToLower()] = item;
            }

            foreach (IOWLClass cls in ontologyManager.GetComputerNetworkElements())
            {
                string sName = cls.Comment;
                if (sName.Length == 0)
                    sName = cls.Name;
                if (Items.ContainsKey(sName.ToLower()))
                    continue;
                ListViewItem item = lv.Items.Add(sName);
                item.Tag = new object[2] { cls.Name, ModelObjectType.Undefined };
                Items[sName.ToLower()] = item;
            }

            Dictionary<string, Bitmap> images = LoadImageList();
            foreach (KeyValuePair<string, Bitmap> pair in images)
            {
                if (!Items.ContainsKey(pair.Key))
                    continue;
                ItemImages[Items[pair.Key]] = pair.Value;
                img.Images.Add(pair.Value);
                Items[pair.Key].ImageIndex = img.Images.Count - 1;
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
        private void dp_DragDrop(object sender, DragEventArgs e)
        {
            ListViewItem li = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
            Point pt = dp.PointToClient(new Point(e.X, e.Y));
            object[] tag = li.Tag as object[];
            ModelObjectType type = (ModelObjectType)tag[1];

            float fZoom = dp.Zoom;
            int delta = 100;
            int X = (int)((pt.X / fZoom - dp.dx) - delta / 2);
            int Y = (int)((pt.Y / fZoom - dp.dx) - delta / 2);
            NetworkObject shape = new NetworkObject(dp);
            shape.Type = type;
            shape.Rect = new Rectangle(X, Y, delta, delta);
            shape.Name = GetUniqueShapeName(li.Text);
            if (type != ModelObjectType.UserObject)
                shape.SemanticType = ontologyManager.GetRoutineClass(ontologyManager.GetClass(tag[0] as string)).Name;
            if (ItemImages.ContainsKey(li))
            {
                shape.img = new Bitmap(ItemImages[li]);
                shape.showBorder = type == ModelObjectType.UserObject;
                shape.Trasparent = false;
            }
            else
            {//!!

                shape.showBorder = true;
            }
            dp.ShapeCollection.AddShape(shape);

            dp.Focus();
        }
        private string GetUniqueShapeName(string sName)
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
        private Dictionary<string, bool> GetShapeNames()
        {
            Dictionary<string, bool> ShapeNames = new Dictionary<string, bool>();
            foreach (DrawingPanel.BaseObject obj in dp.Shapes)
            {
                if (obj.Name != null && obj.Name.Length > 0)
                    ShapeNames[obj.Name.ToLower()] = true;
            }
            return ShapeNames;
        }
        private void dp_objectSelected(object sender, PropertyEventArgs e)
        {
            m_oSelectedObjects = e.ele;
        }
        private void dp_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
                e.Effect = DragDropEffects.Move;
        }
        private void miIP_Click(object sender, EventArgs e)
        {
            if (m_oSelectedObjects == null || m_oSelectedObjects.Length != 1)
                return;
            frmObjectIPs frmObjectIPs = new frmObjectIPs(m_oSelectedObjects[0] as NetworkObject);
            frmObjectIPs.ShowDialog();
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
            }
        }
        public NetworkObject GetSelectedObject()
        {
            if (m_oSelectedObjects == null || m_oSelectedObjects.Length != 1)
                return null;
            return m_oSelectedObjects[0] as NetworkObject;
        }
        public void ResetRoutine(Object sender, EventArgs e)
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
        private void miUserObjectChangeRoutine_Click(object sender, EventArgs e)
        {
            if (m_oSelectedObjects == null || m_oSelectedObjects.Length != 1)
                return;
            NetworkObject obj = m_oSelectedObjects[0] as NetworkObject;
            if (obj.Type != ModelObjectType.UserObject)
                return;
            Routine prevRout = obj.Routine;
            if (prevRout == null || obj.Routine.Type.Length > 0)
            {
                obj.Routine = new Routine();
                obj.Routine.Name = "R" + obj.Name;
                obj.Routine.Text = "routine " + obj.Routine.Name + "(InOut pol)\nendrout";
                obj.Routine.Poluses.Add(new Polus("pol"));
            }
            m_frmChangeRoutine.SetObject(obj);
            m_frmChangeRoutine.ShowDialog();
            if (m_frmChangeRoutine.Successed)
            {
                frmChangeRoutine.SaveLastCompiledRoutine(obj.Routine.Name + ".dll");
                obj.Routine.Type = string.Empty;
            }
            else
                obj.Routine = prevRout;
        }
        private void dp_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point pntShow = dp.PointToScreen(e.Location);
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
                            if (obj.Type == ModelObjectType.UserObject)
                                miRoutine.DropDownItems.Add("Пользовательская", null, miUserObjectChangeRoutine_Click);
                            bool bFind = false;
                            IOWLClass semanticType = ontologyManager.GetClass(obj.SemanticType);
                            if (semanticType != null)//
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
                                int iIndex = obj.Type == ModelObjectType.UserObject && obj.Routine != null ? 1 : 0;
                                ((ToolStripMenuItem)miRoutine.DropDownItems[iIndex]).Checked = true;
                            }
                        }
                        menu.Items.Add("Изменить изображение", null, miUserObjectImage_Click);
                    }
                    else
                        menu.Items.Add("Изменить", null, miLinkChange_Click);

                    menu.Items.Add(new ToolStripSeparator());
                    menu.Items.Add("Удалить", null, miDelete_Click);
                    menu.Show(tb, pntShow, ToolStripDropDownDirection.AboveRight);
                }
            }
        }
        public void SelectRoutine(Object sender, EventArgs e)
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
        private void miLinkChange_Click(object sender, EventArgs e)
        {
            if (m_oSelectedObjects == null || m_oSelectedObjects.Length != 1)
                return;
            Link obj = m_oSelectedObjects[0] as Link;
            if (obj == null)
                return;
            EditLink(obj, true);
        }
        public Boolean EditLink(Link oLink, Boolean bNeedShowDialog)
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
        private void miDelete_Click(object sender, EventArgs e)
        {
            dp.DeleteSelected();
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
    }

}
