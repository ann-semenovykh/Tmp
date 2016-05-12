using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TriadNSim;
using System.Windows.Forms;
using drawingPanel=DrawingPanel.DrawingPanel;
using System.Drawing;

namespace PetriNetModel
{
    public class PetriNetModel: Model
    {
        public PetriNetModel(TabPage t, ListView l, drawingPanel d)
            : base(t,l,d)
        { }
        protected override void LoadElements()
        {
            lv.Clear();
            img.Images.Clear();

            string[] standartItems = { "Позиция", "Переход" }; //{ "Рабочая станция", "Сервер", "Маршрутизатор", "Пользовательский" };
            //string[] standartItems = { "Workstation", "Server", "Router", "Custom" };
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
                //item.Tag = new object[2] { cls.Name, ENetworkObjectType.Undefined };
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
    }
}
