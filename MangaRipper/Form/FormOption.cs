using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMR11
{
    public partial class FormOption : Form
    {
        public FormOption()
        {
            InitializeComponent();
        }

        private void btnSaveBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtSaveDestination.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
