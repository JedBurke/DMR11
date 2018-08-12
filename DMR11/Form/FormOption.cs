using ModernFolderBrowserDialog;
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
            EnableProxyCheckBox.Checked = settings.ProxyEnable;
            ProxyAddress.Text = settings.ProxyHost;
            ProxyPort.Value = settings.ProxyPort;

            txtSaveDestination.Text = settings.DefaultSaveDestination;
        }

        public void ApplySettings()
        {
            var proxyChanged = (settings.ProxyEnable != EnableProxyCheckBox.Checked);

            settings.ProxyEnable = EnableProxyCheckBox.Checked;
            settings.ProxyHost = ProxyAddress.Text;
            settings.ProxyPort = ProxyPort.Value;

            settings.DefaultSaveDestination = txtSaveDestination.Text;

            if (proxyChanged)
            {
                DMR11.Core.Net.ProxyServer.Instance.UseProxyServer = settings.ProxyEnable;
                DMR11.Core.Net.ProxyServer.Instance.SetProxyServer(settings.ProxyHost, Convert.ToInt32(settings.ProxyPort), settings.ProxyUserName, settings.ProxyPassword);

                System.Threading.Thread th = new System.Threading.Thread((() =>
                {
                    Console.WriteLine(DMR11.Core.Net.ProxyServer.Instance.GetProxyIpAddress());
                }));

                th.Start();
            }
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
            using (FolderBrowser browserDialog = new FolderBrowser())
            {
                if (browserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtSaveDestination.Text = browserDialog.SelectedPath;
                }
            }
        }
    }
}
