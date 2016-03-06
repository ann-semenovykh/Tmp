using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModelControl
{
    [Serializable]
    public partial class ModelControl : TabControl
    {
        public TabPageCollection TabPages;
        public class PageCollection : TabControl.TabPageCollection
        {
            public PageCollection(TabControl owner)
                : base(owner)
            {

            }
            public override void Add (ModelPage page)
            {
                base.Add(page);
            }
            public override void Remove(ModelPage page)
            {
                base.Remove(page);
            }
        }
        public ModelControl()
        {
            InitializeComponent();
            TabPages = new PageCollection(this);
        }

        private void ModelControl_Load(object sender, EventArgs e)
        {

        }
    }
}
