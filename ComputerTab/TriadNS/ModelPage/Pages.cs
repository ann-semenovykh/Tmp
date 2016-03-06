using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModelPage
{
    public partial class Pages : TabPage
    {
        private ListView lv;
        private DrawingPanel.DrawingPanel dp;
        public Pages():base()
        {
            InitializeComponent();
            dp = new DrawingPanel.DrawingPanel();
            lv = new ListView();
            lv.Dock = DockStyle.Bottom;
            dp.Dock = DockStyle.Fill;
        }

        public ListView listView
        {
            get
        {
            return lv;            
        }
            set
            {
                lv = value;
            }
        
        }

        public DrawingPanel.DrawingPanel drawingPanel
        {
            get
            { return dp; }
            set
            { dp = value; }
        }
        private void Pages_Load(object sender, EventArgs e)
        {

        }
    }
}
