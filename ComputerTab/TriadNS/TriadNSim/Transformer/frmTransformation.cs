using DrawingPanel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TriadNSim.Transformer;

namespace TriadNSim.Forms
{
    public partial class frmTransformation : Form
    {
        private ListView list;
        private frmRules parent;
        private string edit;
        public frmTransformation(ListView lv,frmRules par,string e)
        {
            InitializeComponent();
            parent = par;
            edit = e;
            list = lv;
            list.Parent = splitContainer2.Panel2;
            list.DragOver += new DragEventHandler(lv_DragOver);
            list.MouseUp += new MouseEventHandler(lv_MouseUp);
            list.ItemDrag += new ItemDragEventHandler(lv_ItemDrag);
            leftPart.DragDrop += new DragEventHandler(dp_DragDrop);
            leftPart.DragEnter += new DragEventHandler(dp_DragEnter);
            leftPart.objectSelected += new ObjectSelected(dp_objectSelected);
            leftPart.MouseUp += new MouseEventHandler(dp_MouseUp);
            rightPart.DragDrop += new DragEventHandler(dp_DragDrop);
            rightPart.DragEnter += new DragEventHandler(dp_DragEnter);
            rightPart.objectSelected += new ObjectSelected(dp_objectSelected);
            rightPart.MouseUp += new MouseEventHandler(dp_MouseUp);
            leftPart.MouseDown += new MouseEventHandler(dp_MouseDown);
            rightPart.MouseDown += new MouseEventHandler(dp_MouseDown);
            leftPart.beforeAddLine += new BeforeAddLine(dp_beforeAddLine);
            rightPart.beforeAddLine += new BeforeAddLine(dp_beforeAddLine);
            leftPart.MouseDoubleClick += new MouseEventHandler(dp_MouseDoubleClick);
            rightPart.MouseDoubleClick += new MouseEventHandler(dp_MouseDoubleClick);
            leftPart.MouseClick += new MouseEventHandler(dp_MouseClick);
            rightPart.MouseClick += new MouseEventHandler(dp_MouseClick);
        }

        private void dp_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                x1 = e.X;
                y1 = e.Y;
            }
        }
        int x, y, y1, x1;
        private void dp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                x = e.X;
                y = e.Y;
            } 
        }

        private void dp_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //DrawingPanel.DrawingPanel dp = (DrawingPanel.DrawingPanel)sender;
            //BaseObject selObj = dp.ShapeCollection.selectedObj;
            //if (selObj is Link)
            //    EditLink(selObj as Link, true);
        }

        private void dp_beforeAddLine(object sender, BeforeAddLineEventArgs e)
        {
            DrawingPanel.DrawingPanel dp = (DrawingPanel.DrawingPanel)sender;
            e.cancel = true;
            NetworkObject objFrom = e.fromCP.Owner as NetworkObject;
            NetworkObject objTo = e.toCP.Owner as NetworkObject;
            if (dp.ShapeCollection.GetLine(objFrom, objTo) == null)
            {
                Link oLink = toolStripButton1.Checked ? new Link(dp, e.fromCP, e.toCP) : new Link(dp, e.fromCP, e.toCP,x, y, x1, y1);
                dp.ShapeCollection.AddShape(oLink);
            }
        }


        private void dp_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void dp_objectSelected(object sender, PropertyEventArgs e)
        {
        }

        private void dp_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
                e.Effect = DragDropEffects.Move;
        }

        private void dp_DragDrop(object sender, DragEventArgs e)
        {
            DrawingPanel.DrawingPanel dp = (DrawingPanel.DrawingPanel)sender;
            ListViewItem li = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
            Point pt = dp.PointToClient(new Point(e.X, e.Y));
            object[] tag = li.Tag as object[];

            float fZoom = dp.Zoom;
            int delta = 100;
            int X = (int)((pt.X / fZoom - dp.dx) - delta / 2);
            int Y = (int)((pt.Y / fZoom - dp.dx) - delta / 2);
            NetworkObject shape = new NetworkObject(dp);
            shape.Type = ModelObjectType.Entity;
            shape.Rect = new Rectangle(X, Y, delta, delta);
            shape.Name = li.Text;
            if (parent.transform.ItemImages.ContainsKey(li))
            {
                shape.img = new Bitmap(parent.transform.ItemImages[li]);
                shape.Trasparent = false;
            }
            else
                shape.showBorder = false;
            dp.ShapeCollection.AddShape(shape);

            dp.Focus();
        }

        private void lv_ItemDrag(object sender, ItemDragEventArgs e)
        {
            list.DoDragDrop(e.Item, DragDropEffects.Move);
        }
        protected void ChangeElementImage(object sender, EventArgs e)
        {
            ListViewItem item = list.SelectedItems[0];
            if (item != null)
            {
                Bitmap bmp = Model.LoadImage("Изображение элемента '" + item.Text + "'");
                if (bmp != null)
                {
                    parent.transform.ItemImages[item] = bmp;
                    parent.img.Images.Add(bmp);
                    item.ImageIndex = parent.img.Images.Count - 1;
                }
            }
        }
        protected void сMenuItemsAdd_Click(object sender, EventArgs e)
        {
            frmAddElement frm = new frmAddElement(false);
            frm.cmbParent.Visible = frm.label2.Visible=false;
            frm.Bmp = global::TriadNSim.Properties.Resources.question;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                parent.img.Images.Add(frm.Bmp);
                int nImageIndex = parent.img.Images.Count - 1;
                ListViewItem li = list.Items.Add(frm.Name, nImageIndex);
                parent.transform.ItemImages[li] = frm.Bmp;
            }
        }
        private void lv_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                if (list.SelectedItems.Count > 0)
                {
                    ListViewItem item = list.SelectedItems[0];

                    menu.Items.Add("Изменить изображение", null, ChangeElementImage);

                }
                else
                {
                    menu.Items.Add("Добавить элемент", null, сMenuItemsAdd_Click);
                }
                if (menu.Items.Count > 0)
                    menu.Show(list, e.Location, ToolStripDropDownDirection.AboveRight);
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

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton1_ButtonClick(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void frmTransformation_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text=="")
            {
                MessageBox.Show("Введите имя правила");
                return;
            }
            if (leftPart.Shapes.Count==0)
            {
                MessageBox.Show("Не определена правая часть правила");
                return;
            }
            if (parent.lstRules.Items.ContainsKey(txtName.Text))
            {
                MessageBox.Show("Правило с таким именем уже существует");
                return;
            }
            TransformationRule item = new TransformationRule();
            item.Name = txtName.Text;
            copy_shapes(leftPart.Shapes,item.leftPart);
            copy_shapes(rightPart.Shapes, item.rightPart);
            parent.transform.Rules.Add(item);
            parent.lstRules.Items.Add(item.Name);
            parent.lstRules.Items[parent.lstRules.Items.Count - 1].Selected = true;
            this.Close();
        }
        private void copy_shapes(ArrayList dp,ArrayList rule)
        {
            foreach (BaseObject obj in dp)
                rule.Add(obj);
        }

        private void leftPart_Load(object sender, EventArgs e)
        {

        }

        private void frmTransformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            //list.Parent = null;
          //  list = null;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
                leftPart.CurrentTool = DrawingPanel.ToolType.ttLine;
                rightPart.CurrentTool = DrawingPanel.ToolType.ttLine;
            
            toolStripbtnSelect.Checked = false;
            toolStripbtnLink.Checked = false;
            toolStripButton1.Checked = true;
        }

        private void toolStripbtnLink_Click(object sender, EventArgs e)
        {
            leftPart.CurrentTool = DrawingPanel.ToolType.ttLine;
            rightPart.CurrentTool = DrawingPanel.ToolType.ttLine;

            toolStripbtnSelect.Checked = false;
            toolStripbtnLink.Checked = true;
            toolStripButton1.Checked = false;
        }

        private void toolStripbtnSelect_Click(object sender, EventArgs e)
        {
            leftPart.CurrentTool = DrawingPanel.ToolType.ttSelect;
            rightPart.CurrentTool = DrawingPanel.ToolType.ttSelect;

            toolStripbtnSelect.Checked = true;
            toolStripbtnLink.Checked = false;
            toolStripButton1.Checked = false;
        }

    }
}
