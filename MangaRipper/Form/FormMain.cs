using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Deployment.Application;
using System.Collections.Specialized;
using MangaRipper.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MangaRipper
{
    public partial class FormMain : Form
    {

        BindingList<IChapter> DownloadQueue;

        protected const string FILENAME_ICHAPTER_COLLECTION = "IChapterCollection.bin";

        private CancellationTokenSource _cts;

        private string SaveDestination
        {
            get
            {
                if (rdSeriesDestination.Checked)
                {
                    return lbSeriesDestination.Text;
                }
                else
                    return lbDefaultDestination.Text;
            }
            set
            {
                if (rdSeriesDestination.Checked)
                    lbSeriesDestination.Text = value;

                else
                    lbDefaultDestination.Text = value;
            }
        }

        public FormMain()
        {
            InitializeComponent();

            //var items = cbTitleUrl.Items;
            //object[] sorted = new object[items.Count];

            //List<object> itemsToBeSorted = new List<object>();
            //itemsToBeSorted.Sort();

            //cbTitleUrl.BeginUpdate();

            //cbTitleUrl.Items.AddRange(itemsToBeSorted.ToArray());

            //cbTitleUrl.EndUpdate();

        }

        private void btnGetChapter_Click(object sender, EventArgs e)
        {
            try
            {
                var titleUrl = new UriValidated(cbTitleUrl.Text);
                ITitle title = TitleFactory.CreateTitle(titleUrl);
                title.Proxy = Option.GetProxy();
                btnGetChapter.Enabled = false;
                var task = title.PopulateChapterAsync(new MangaRipper.Core.Progress<int>(progress => txtPercent.Text = progress + "%"));
                task.ContinueWith(t =>
                {
                    btnGetChapter.Enabled = true;
                    dgvChapter.DataSource = title.Chapters;



                    PrepareSeriesDirectory();

                    if (t.Exception != null && t.Exception.InnerException != null)
                    {
                        txtMessage.Text = t.Exception.InnerException.Message;
                    }

                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var items = new List<IChapter>();
            foreach (DataGridViewRow row in dgvChapter.Rows)
            {
                if (row.Selected == true)
                {
                    items.Add((IChapter)row.DataBoundItem);
                }
            }

            items.Reverse();
            foreach (IChapter item in items)
            {
                if (DownloadQueue.IndexOf(item) < 0)
                {
                    DownloadQueue.Add(item);
                }
            }
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            var items = new List<IChapter>();
            foreach (DataGridViewRow row in dgvChapter.Rows)
            {
                items.Add((IChapter)row.DataBoundItem);
            }
            items.Reverse();
            foreach (IChapter item in items)
            {
                if (DownloadQueue.IndexOf(item) < 0)
                {
                    DownloadQueue.Add(item);
                }
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            int limit = 0,
                chaptersAdded = 0;

            if (Helper.KeyState.IsShiftKeyPressed())
                limit = 5;

            for (int i = dgvChapter.RowCount; i-- > 0; )
            {
                var item = (IChapter)dgvChapter.Rows[i].DataBoundItem;

                // Todo: Check the default AND the series directory for the chapters.
                string destinationDirectory = lbDefaultDestination.Text; //txtSaveTo.Text;

                // Todo: Check if the series destination exists, create the directory if it doesn't.

                string testPath = Path.Combine(destinationDirectory, item.Name);
                bool chapterExists = Directory.Exists(testPath);

                if (chapterExists)
                {
                    // Check if the directory has any files (pages).
                    int pageCount = Directory.GetFiles(testPath).Length;

                    // If the page count is zero, treat it as the user hasn't downloaded
                    // that chapter yet.
                    if (pageCount == 0)
                        chapterExists = false;

                }
                else
                {
                    testPath = Path.Combine(lbSeriesDestination.Text, item.Name);
                    chapterExists = Directory.Exists(testPath);
                    string seriesDestination = lbSeriesDestination.Text;
                    bool seriesDestinationExists = Directory.Exists(seriesDestination);

                    if (seriesDestinationExists)
                    {
                        if (!chapterExists)
                        {
                            foreach (var sub in Directory.GetDirectories(seriesDestination))
                            {
                                testPath = Path.Combine(sub, item.Name);
                                chapterExists = Directory.Exists(testPath);

                                Console.WriteLine("Path: {0} | Exists: {1}", testPath, chapterExists);
                            }
                        }


                        if (chapterExists)
                        {
                            // Check if the directory has any files (pages).
                            int pageCount = Directory.GetFiles(testPath).Length;

                            // If the page count is zero, treat it as the user hasn't downloaded
                            // that chapter yet.
                            if (pageCount == 0)
                                chapterExists = false;

                        }

                    }
                }

                if (!chapterExists)
                {
                    if (DownloadQueue.IndexOf(item) < 0)
                    {
                        DownloadQueue.Add(item);

                        if (limit > 0 && (++chaptersAdded == limit))
                            break;
                    }

                }
            }


        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dgvQueueChapter.SelectedRows)
            {
                IChapter chapter = (IChapter)item.DataBoundItem;
                if (chapter.IsBusy == false)
                {
                    DownloadQueue.Remove(chapter);
                }
            }
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            var removeItems = DownloadQueue.Where(r => r.IsBusy == false).ToList();

            foreach (var item in removeItems)
            {
                DownloadQueue.Remove(item);
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            _cts = new CancellationTokenSource();
            DownloadChapter();
        }

        private void DownloadChapter(int millisecond)
        {
            timer1.Interval = millisecond;
            timer1.Enabled = true;
        }

        private void DownloadChapter()
        {
            if (DownloadQueue.Count > 0 && _cts.IsCancellationRequested == false)
            {
                int current = DownloadQueue.Where(c => c.IsBusy == true).Count();
                int max = Convert.ToInt32(nudThread.Value);
                int remain = max - current;
                var chapters = DownloadQueue.Where(c => c.IsBusy == false).Take(remain);

                foreach (var chapter in chapters)
                {
                    chapter.Proxy = Option.GetProxy();
                    btnDownload.Enabled = false;
                    var task = chapter.DownloadImageAsync(SaveDestination, _cts.Token, new MangaRipper.Core.Progress<ChapterProgress>(c =>
                        {
                            foreach (DataGridViewRow item in dgvQueueChapter.Rows)
                            {
                                if (c.Chapter == item.DataBoundItem)
                                {
                                    item.Cells[ColChapterStatus.Name].Value = c.Percent + "%";
                                    break;
                                }
                            }
                        }));

                    task.ContinueWith(t =>
                    {
                        switch (t.Status)
                        {
                            case TaskStatus.Canceled:
                                btnDownload.Enabled = true;
                                break;
                            case TaskStatus.Faulted:
                                txtMessage.Text = t.Exception.InnerException.Message;
                                DownloadChapter(1000);
                                break;
                            case TaskStatus.RanToCompletion:
                                DownloadQueue.Remove(chapter);
                                DownloadChapter();
                                break;
                            default:
                                break;
                        }
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
            else
            {
                btnDownload.Enabled = true;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _cts.Cancel();
        }

        private void btnChangeSaveTo_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = SaveDestination;

            DialogResult dr = folderBrowserDialog1.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                SaveDestination = folderBrowserDialog1.SelectedPath;
            }

        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveDestination))
                Process.Start(SaveDestination);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Size = MangaRipper.Properties.Settings.Default.Size;
            this.Location = MangaRipper.Properties.Settings.Default.Location;
            this.WindowState = MangaRipper.Properties.Settings.Default.WindowState;

            dgvQueueChapter.AutoGenerateColumns = false;
            dgvChapter.AutoGenerateColumns = false;

            this.Text = String.Format("{0} {1}", Application.ProductName, AppInfo.DeploymentVersion);

            //foreach (string[] item in TitleFactory.GetSupportedSites())
            //{
            //    dgvSupportedSites.Rows.Add(item);
            //}

            if (String.IsNullOrEmpty(lbDefaultDestination.Text))
            {
                lbDefaultDestination.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            DownloadQueue = Common.LoadIChapterCollection(FILENAME_ICHAPTER_COLLECTION);
            dgvQueueChapter.DataSource = DownloadQueue;

            LoadBookmark();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            var about = new AboutBox();
            about.ShowDialog(this);
        }

        private void btnHowToUse_Click(object sender, EventArgs e)
        {
            Process.Start("http://mangaripper.codeplex.com/documentation");
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                MangaRipper.Properties.Settings.Default.Size = this.Size;
                MangaRipper.Properties.Settings.Default.Location = this.Location;
                MangaRipper.Properties.Settings.Default.WindowState = this.WindowState;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                MangaRipper.Properties.Settings.Default.WindowState = this.WindowState;
            }

            Properties.Settings.Default.Save();
            Common.SaveIChapterCollection(DownloadQueue, FILENAME_ICHAPTER_COLLECTION);
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            FormOption form = new FormOption();
            form.ShowDialog(this);
        }

        private void LoadBookmark()
        {
            cbTitleUrl.Items.Clear();
            StringCollection sc = MangaRipper.Properties.Settings.Default.Bookmark;

            if (sc != null)
            {
                foreach (string item in sc)
                {
                    cbTitleUrl.Items.Add(item);
                }
            }
        }

        private void btnAddBookmark_Click(object sender, EventArgs e)
        {
            StringCollection sc = MangaRipper.Properties.Settings.Default.Bookmark;
            if (sc == null)
            {
                sc = new StringCollection();
            }
            if (sc.Contains(cbTitleUrl.Text) == false)
            {
                sc.Add(cbTitleUrl.Text);
                MangaRipper.Properties.Settings.Default.Bookmark = sc;
                LoadBookmark();
            }
        }

        private void btnRemoveBookmark_Click(object sender, EventArgs e)
        {
            StringCollection sc = MangaRipper.Properties.Settings.Default.Bookmark;
            if (sc != null)
            {
                sc.Remove(cbTitleUrl.Text);
                MangaRipper.Properties.Settings.Default.Bookmark = sc;

                LoadBookmark();
            }
        }

        private void btnAddPrefixCounter_Click(object sender, EventArgs e)
        {
            var chapters = new List<IChapter>();
            foreach (DataGridViewRow row in dgvChapter.Rows)
            {
                IChapter chapter = row.DataBoundItem as IChapter;
                chapters.Add(chapter);
            }
            chapters = Common.CloneIChapterCollection(chapters);

            chapters.Reverse();
            chapters.ForEach(r => r.Name = String.Format("[{0:000}] - {1}", chapters.IndexOf(r) + 1, r.Name));
            chapters.Reverse();

            dgvChapter.DataSource = chapters;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            DownloadChapter();
        }

        private void btnPasteUrl_Click(object sender, EventArgs e)
        {
            bool clipboardCopied = false;

            try
            {
                if (Clipboard.ContainsText())
                {
                    string pastedUrl = Clipboard.GetData(DataFormats.StringFormat).ToString();

                    if (!string.IsNullOrWhiteSpace(pastedUrl))
                    {
                        pastedUrl = pastedUrl.Trim();
                        cbTitleUrl.Text = pastedUrl;
                        clipboardCopied = true;
                    }

                }
            }
            catch (Exception)
            {
                string error = String.Format("{0} - Clipboard operation failed, please try again.", DateTime.Now.ToLongTimeString());
                txtMessage.Text = error;
            }


            if (clipboardCopied)
            {
                btnGetChapter_Click(null, EventArgs.Empty);
            }

        }

        private void lbDefaultDestination_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                rdDefaultDestination.Checked = true;
        }

        private void lbSeriesDestination_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                rdSeriesDestination.Checked = true;
        }

        private void PrepareSeriesDirectory()
        {
            // Todo: Set series-specific directory path to default.
            if (dgvChapter.RowCount == 0)
                return;

            string
                defaultSeriesDestination = Properties.Settings.Default.DefaultSaveDestination,
                series = string.Empty,
                path = string.Empty;

            if (!string.IsNullOrWhiteSpace(cbTitleUrl.Text))
            {
                Uri seriesUri = null;

                if (Uri.TryCreate(cbTitleUrl.Text, UriKind.Absolute, out seriesUri))
                    series = seriesUri.ToString();

                else
                    series = cbTitleUrl.SelectedItem.ToString();
            }
            else
            {
                series = cbTitleUrl.Text;
            }


            if (string.IsNullOrWhiteSpace(series))
            {
                // Todo: Set series-specific directory path to default.
                return;
            }

            if (string.IsNullOrEmpty(defaultSeriesDestination))
                defaultSeriesDestination = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            series = series.TrimEnd('/');
            series = series.Substring(series.LastIndexOf('/') + 1);

            var item = (IChapter)dgvChapter.Rows[0].DataBoundItem;
            series = item.Name.Substring(0, item.Name.LastIndexOf(" ")).Trim();

            // Todo: Replace invalid characters.
            path = Path.Combine(defaultSeriesDestination, series);

            lbSeriesDestination.Text = path;

            if (Directory.Exists(path))
                rdSeriesDestination.Checked = true;

        }

    }

}

