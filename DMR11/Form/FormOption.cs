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
        public FormOption(IMainFormSettings settings)
        {
            InitializeComponent();

            this.settings = settings;

            this.Load += FormOption_Load;
            
            this.FormClosing += FormOption_FormClosing;
        }
        
        IMainFormSettings settings;

        public void LoadSettings()
        {
            txtSaveDestination.Text = settings.DefaultSaveDestination;
        }

        public void ApplySettings()
        {           

            settings.DefaultSaveDestination = txtSaveDestination.Text;
        }

        void FormOption_Load(object sender, EventArgs e)
        {
            FormMain.SetFormButtonStyle(new[] { this.Controls, this.grpSaveDestination.Controls });
            LoadSettings();
        }
        
        void FormOption_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                ApplySettings();
            }
        }

        private void btnSaveBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog browseDialog = new FolderBrowserDialog())
            {
                if (browseDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtSaveDestination.Text = browseDialog.SelectedPath;
                }
            }
        }
    }
}
