using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CartonTranceSystemClient.CustomApplication;
using log4net;

using System.Reflection;

namespace CartonTranceSystemClient
{
    public partial class FormMain : Form
    {
        BrainHeart brain;
        
        public FormMain()
        {
            InitializeComponent();
           
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            brain = new BrainHeart();
            brain.ConnectDevice();
        }
    }
}
