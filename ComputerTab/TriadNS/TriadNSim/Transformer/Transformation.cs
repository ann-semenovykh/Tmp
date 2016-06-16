
using PetriNetModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace TriadNSim.Transformer
{
    [Serializable]
    public class Transformation
    {
        public string sourceModel, targetModel;
        public List<TransformationRule> Rules=new List<TransformationRule>();
        public Dictionary<ListViewItem, Bitmap> ItemImages = new Dictionary<ListViewItem, Bitmap>();
        public Transformation(ImageList img,COWLOntologyManager ontologyManager,ListView lstItem)
        {
            lstItem.Dock = DockStyle.Fill;
            lstItem.Clear();
            img.Images.Clear();
            LoadElements("petri_elements.xml",":PetriNetNode",img,ontologyManager,lstItem);
            LoadElements("comp_elements.xml", ":ComputerNetworkNode",img,ontologyManager,lstItem);
            Size s = new Size(60, 60);
            img.ImageSize = s;
            lstItem.LargeImageList = img;
        }
        public Transformation()
        {

        }
        protected void LoadElements(string path,string node,ImageList img,COWLOntologyManager ontologyManager,ListView lstItem)
        {
            Dictionary<string, ListViewItem> Items = new Dictionary<string, ListViewItem>();
            
            foreach (IOWLClass cls in ontologyManager.GetNetworkElements(node))
            {
                string sName = cls.Comment;
                if (sName.Length == 0)
                    sName = cls.Name;
                if (Items.ContainsKey(sName.ToLower()))
                    continue;
                ListViewItem item = lstItem.Items.Add(sName);
                Items[sName.ToLower()] = item;
            }

            Dictionary<string, Bitmap> images = LoadImageList(path);
            foreach (KeyValuePair<string, Bitmap> pair in images)
            {
                if (!Items.ContainsKey(pair.Key))
                    continue;
                ItemImages[Items[pair.Key]] = pair.Value;
                img.Images.Add(pair.Value);
                Items[pair.Key].ImageIndex = img.Images.Count - 1;
            }
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
    }
}
