using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TriadNSim;
using System.Windows.Forms;
using System.Drawing;
using drawingPanel = DrawingPanel.DrawingPanel;
using TriadNSim.Forms;
using graphicalEditor=GraphicalEditor.GraphicalEditor;

namespace ComputerModel
{
    public class  ComputerModel: Model
    {
        public ComputerModel(TabPage tb,graphicalEditor editor):base (tb,editor)
        {

        }
        protected override void miUserObjectChangeRoutine_Click(object sender, EventArgs e)
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
        protected override void dp_MouseUp(object sender, MouseEventArgs e)
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
        protected override void lv_MouseUp(object sender, MouseEventArgs e)
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
                li.Tag = new object[2] { frm.Name, ModelObjectType.Undefined };
                ItemImages[li] = frm.Bmp;
            }
        }
        protected override void dp_DragDrop(object sender, DragEventArgs e)
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
        protected override void LoadElements()
        {

            lv.Clear();
            img.Images.Clear();

            string[] standartItems = { "Рабочая станция", "Сервер", "Маршрутизатор", "Пользовательский" };
            //string[] standartItems = { "Workstation", "Server", "Router", "Custom" };
            string[] standartItemNames = { "Workstation", "Server", "Router", "ComputerNetworkNode" };
            EComputerObjectType[] types = { EComputerObjectType.Undefined, EComputerObjectType.Undefined, EComputerObjectType.Undefined, EComputerObjectType.UserObject };
            Dictionary<string, ListViewItem> Items = new Dictionary<string, ListViewItem>();
            for (int i = 0; i < standartItems.Length; i++)
            {
                string sName = standartItems[i];
                ListViewItem item = lv.Items.Add(sName);
                item.Tag = new object[2] { standartItemNames[i], types[i] };
                Items[sName.ToLower()] = item;
            }

            foreach (IOWLClass cls in ontologyManager.GetNetworkElements(":ComputerNetNode"))
            {
                string sName = cls.Comment;
                if (sName.Length == 0)
                    sName = cls.Name;
                if (Items.ContainsKey(sName.ToLower()))
                    continue;
                ListViewItem item = lv.Items.Add(sName);
                item.Tag = new object[2] { cls.Name, EComputerObjectType.Undefined };
                Items[sName.ToLower()] = item;
            }

            Dictionary<string, Bitmap> images = LoadImageList("comp_elements.xml");
            foreach (KeyValuePair<string, Bitmap> pair in images)
            {
                if (!Items.ContainsKey(pair.Key))
                    continue;
                ItemImages[Items[pair.Key]] = pair.Value;
                img.Images.Add(pair.Value);
                Items[pair.Key].ImageIndex = img.Images.Count - 1;
            }
        }
    }
}
