using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SatTracker
{
    public partial class fmClusterSettings : Form
    {
        public List<String> Nodes = new List<String>();

        public fmClusterSettings()
        {
            InitializeComponent();
        }

        private void buttonAddNode_Click(object sender, EventArgs e)
        {
            NodesList.Items.Add(textBoxAddNode.Text, true);
        }

        private void fmClusterSettings_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in NodesList.CheckedItems)
                Nodes.Add((string)item);
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
