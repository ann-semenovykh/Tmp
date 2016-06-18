using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TriadNSim;
using System.Windows.Forms;
using drawingPanel=DrawingPanel.DrawingPanel;
using System.Drawing;
using TriadNSim.Forms;
using System.ComponentModel;
using DrawingPanel;
using TriadNSim.SimulationModel.PetriNetModel;

namespace PetriNetModel
{
    [Serializable]
    public class PetriNetModel: Model
    {

        ToolStripButton add_mark;
        ToolStripButton tree;
        public PetriNetModel(TabPage t,GraphicalEditor.GraphicalEditor editor)
            : base(t,editor)
        {
            dp.MouseDown += new MouseEventHandler(dp_MouseDown);
            dp.MouseClick += new MouseEventHandler(dp_MouseClick);

            editor.toolStripContainer1.TopToolStripPanelVisible = true;
            add_mark= new ToolStripButton("Добавить метку", new Bitmap("img\\mark.png"));
            tree = new ToolStripButton("Дерево достижимости", new Bitmap("img\\tree.png"));
            add_mark.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tree.DisplayStyle = ToolStripItemDisplayStyle.Image;
            editor.toolStrip1.Items.Add(add_mark);
            editor.toolStrip1.Items.Add(new ToolStripSeparator());
            editor.toolStrip1.Items.Add(tree);
            add_mark.Click += new EventHandler(ButtonMark_Click);
        }
        private void ButtonMark_Click(object sender, EventArgs e)
        {
            dp.CurrentTool = DrawingPanel.ToolType.ttDynamicOb;
            add_mark.Checked = !add_mark.Checked;
        }
        void dp_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                x1 = e.X;
                y1 = e.Y;
            }
        }

        void dp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                x = e.X;
                y = e.Y;
            } 

        }
        protected override void LoadElements()
        {
            lv.Clear();
            img.Images.Clear();

            string[] standartItems = { "Позиция", "Переход" };
            string[] standartItemNames = { "Place", "Transition" };
            ENetPetriObjectType[] types = { ENetPetriObjectType.Place, ENetPetriObjectType.Transition };
            Dictionary<string, ListViewItem> Items = new Dictionary<string, ListViewItem>();
            for (int i = 0; i < standartItems.Length; i++)
            {
                string sName = standartItems[i];
                ListViewItem item = lv.Items.Add(sName);
                item.Tag = new object[2] { standartItemNames[i], types[i] };
                Items[sName.ToLower()] = item;
            }

            foreach (IOWLClass cls in ontologyManager.GetNetworkElements(":PetriNetNode"))
            {
                string sName = cls.Comment;
                if (sName.Length == 0)
                    sName = cls.Name;
                if (Items.ContainsKey(sName.ToLower()))
                    continue;
                ListViewItem item = lv.Items.Add(sName);
                Items[sName.ToLower()] = item;
            }

            Dictionary<string, Bitmap> images = LoadImageList("petri_elements.xml");
            foreach (KeyValuePair<string, Bitmap> pair in images)
            {
                if (!Items.ContainsKey(pair.Key))
                    continue;
                ItemImages[Items[pair.Key]] = pair.Value;
                img.Images.Add(pair.Value);
                Items[pair.Key].ImageIndex = img.Images.Count - 1;
            }
        }
        protected override void lv_MouseUp(object sender, MouseEventArgs e)
        {
        if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                if (lv.SelectedItems.Count > 0)
                {
                    ListViewItem item = lv.SelectedItems[0];
                    ENetPetriObjectType type = ENetPetriObjectType.Undefined;
                    object[] tag = null;
                    if (item.Tag != null)
                    {
                        tag = item.Tag as object[];
                        type = (ENetPetriObjectType)tag[1];
                    }
                   
                        menu.Items.Add("Изменить изображение", null, ChangeElementImage);
                    
                }
                else
                {
                    menu.Items.Add("Добавить элемент", null, сMenuItemsAdd_Click);
                }
                if (menu.Items.Count > 0)
                    menu.Show(lv, e.Location, ToolStripDropDownDirection.AboveRight);
            }
}
        protected override void сMenuItemsAdd_Click(object sender, EventArgs e)
        {
            frmAddElement frm = new frmAddElement(true);
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
                ItemImages[li] = frm.Bmp;
            }
        }
        protected override void dp_DragDrop(object sender, DragEventArgs e)
        {
        ListViewItem li = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
            Point pt = dp.PointToClient(new Point(e.X, e.Y));
            object[] tag = li.Tag as object[];
            ENetPetriObjectType type = (ENetPetriObjectType)tag[1];

            float fZoom = dp.Zoom;
            int delta = 100;
            int X = (int)((pt.X / fZoom - dp.dx) - delta / 2);
            int Y = (int)((pt.Y / fZoom - dp.dx) - delta / 2);
            NetworkObject shape = new NetworkObject(dp);
            shape.Tag = li.Text;
            shape.Type = ModelObjectType.Entity;
            shape.Rect = new Rectangle(X, Y, delta, delta);
            shape.Name = GetUniqueShapeName(li.Text);
            {
                shape.SemanticType = ontologyManager.GetRoutineClass(ontologyManager.GetClass(tag[0] as string)).Name;//!!
            }
            if (ItemImages.ContainsKey(li))
            {
                shape.img = new Bitmap(ItemImages[li]);
                shape.Trasparent = false;
            }
            else
                shape.showBorder = false;
            dp.ShapeCollection.AddShape(shape);
            if (type == ENetPetriObjectType.Place)
            {
                PetriMark m = new PetriMark(dp, shape.ConnectionPoint);
                shape.DynamicObj = m;
                m.mult = 0;

                dp.ShapeCollection.AddShape(m);
                
            }
            
            dp.Focus();
    }
        int x, y, x1, y1;
        protected override void dp_beforeAddDynamicOb(object sender, BeforeAddDynamicObEventArgs e)
        {
            e.cancel = true;
            NetworkObject objFrom = e.CP.Owner as NetworkObject;
            if (objFrom.SemanticType == "PlaceRoutine")
                if (dp.ShapeCollection.GetDynamicOb(objFrom) == null)
                {
                    //PetriMark m = new PetriMark(drawingPanel1, e.CP);
                    //drawingPanel1.ShapeCollection.AddShape(m);
                    //objFrom.Mark = m;
                }
                else
                {
                    PetriMark m = (PetriMark)dp.ShapeCollection.GetDynamicOb(objFrom);
                    m.IncMult();
                }

        }
        public void GetLinks(Link oLink, Polus from, Polus to)
        {
            foreach (BaseObject obj in dp.ShapeCollection.ShapeList)
            {
                if (obj is Link)
                {
                    Link line = obj as Link;
                    if (line.FromCP.Owner == oLink.FromCP.Owner && line.ToCP.Owner == oLink.ToCP.Owner)
                    //|| line.FromCP.Owner == obj2 && line.ToCP.Owner == obj1)
                    {
                        line.PolusFrom = from;
                        line.PolusTo = to;
                    }
                }
            }
        }
        public override bool EditLink(Link oLink, bool bNeedShowDialog)
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
        protected override void dp_beforeAddLine(object sender, BeforeAddLineEventArgs e)
        {
            e.cancel = true;
            NetworkObject objFrom = e.fromCP.Owner as NetworkObject;
            NetworkObject objTo = e.toCP.Owner as NetworkObject;
            if (objFrom.SemanticType != objTo.SemanticType)//соединяются только различные вершины (позиции и переходы)
                if (dp.ShapeCollection.GetLine(objFrom, objTo) == null) //связь туда и обратно
                {

                    //PetriLine oLine = new PetriLine(drawingPanel1, e.fromCP, e.toCP, x, y, x1, y1);
                    Link oLink = new Link(dp, e.fromCP, e.toCP, x, y, x1, y1);
                    if (EditLink(oLink, false))
                    {
                        // drawingPanel1.ShapeCollection.AddShape(oLine);
                        dp.ShapeCollection.AddShape(oLink);
                    }
                }
                else
                {
                    //PetriLink oLink = new PetriLink(drawingPanel1, e.fromCP, e.toCP, x, y, x1, y1);
                    Line curLine = dp.ShapeCollection.GetLine(objFrom, objTo);
                    curLine.IncMult();
                    //drawingPanel1.ShapeCollection.AddShape(oLink);

                }
        }
        string rout_place = "routine_place.txt";
        string rout_tr = "routine_tr.txt";
        protected override void miUserObjectChangeRoutine_Click(object sender, EventArgs e)
        {
            if (m_oSelectedObjects == null || m_oSelectedObjects.Length != 1)
                return;
            NetworkObject obj = m_oSelectedObjects[0] as NetworkObject;
            Routine prevRout = obj.Routine;
            if (prevRout == null || obj.Routine.Type.Length > 0)
            {
                string path;
                if (obj.SemanticType == "PlaceRoutine") path = rout_place;   ///???
                else path = rout_tr;
                obj.Routine = new Routine();
                obj.Routine.Name = "R" + obj.Name;

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
        private void NewFormRoutine(frmChangeRoutine m_frmChangeRoutine)
        {
            m_frmChangeRoutine.OnNameChecked += delegate(object sender, CancelEventArgs args)
            {
                if (ontologyManager.GetClass(m_frmChangeRoutine.DesignTypeName) != null)
                    args.Cancel = true;
            };
        }
        protected override void dp_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right )//&& !manual)
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
                            miRoutine.DropDownItems.Add("Задать рутину", null, miUserObjectChangeRoutine_Click);
                            bool bFind = false;
                            IOWLClass semanticType = ontologyManager.GetClass(obj.SemanticType);
                            if (semanticType != null)
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
                                
                            }
                        }
                        menu.Items.Add("Изменить изображение", null, miUserObjectImage_Click);
                    }
                    else
                        menu.Items.Add("Изменить", null, miLinkChange_Click);

                    menu.Items.Add(new ToolStripSeparator());
                    menu.Items.Add("Удалить", null, miDelete_Click);
                    menu.Show(dp, pntShow, ToolStripDropDownDirection.AboveRight);
                }

            }
            if (e.Button == MouseButtons.Left)
            {
                x1 = e.X;
                y1 = e.Y;
                //if (manual)
                //{
                //    NetworkObject ob = null;
                //    ob = GetSelectedObject();
                //    if (m_oSelectedObjects != null) ob = (NetworkObject)m_oSelectedObjects[0];

                //    if (ob != null && ob.Type == ENetPetriObjectType.Transition)
                //    { changed = false; RunManual(ob); }
                //}

            }
        }
        private void DrawMarks()
        {
            PetriMark pm;
            foreach (BaseObject ob in dp.Shapes)
                if (ob.bIsDynamicOb)
                { pm = (PetriMark)ob; pm.Draw(dp.CreateGraphics(), dp.dx, dp.dy, dp.Zoom); }
        }
    }
}
