using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CartonTranceSystemClient.CustomApplication;

using Loger;

namespace CartonTranceSystemClient
{
    public partial class FormMain : Form
    {
        BrainHeart brain;
        public FormMain()
        {
            InitializeComponent();
            LogManager aa = new LogManager();
            aa.WriteInfo("hello");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            brain = new BrainHeart();
            brain.ConnectDevice();
        }
    }
}
