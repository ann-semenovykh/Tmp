﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using TriadNSim.Forms;

namespace TriadNSim
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmSimulation());
            //Application.Run(new frmEditParam(10));
        }
    }
}
